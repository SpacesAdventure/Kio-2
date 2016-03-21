using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace HutongGames.PlayMaker.Actions
{
	class EasySaveArrayTools{
		public static void SaveArray(FsmArray array,string filename,ES2Settings setting){
			VariableType t= array.ElementType;
			object[] os;
			switch(t){
			case VariableType.Bool:{
					os=array.Values;
					bool[] bs=new bool[os.Length];
					for(int i=0;i<bs.Length;i++){
						bs[i]=(bool)os[i];
					}
					ES2.Save(bs, filename, setting);
					break;
				}
			case VariableType.Int:{
					os=array.Values;
					int[] ins=new int[os.Length];
					for(int i=0;i<ins.Length;i++){
						ins[i]=(int)os[i];
					}
					ES2.Save(ins, filename, setting);
					break;
				}
			case VariableType.Float:{
					os=array.Values;
					float[] fs=new float[os.Length];
					for(int i=0;i<fs.Length;i++){
						fs[i]=(float)os[i];
					}
					ES2.Save(fs, filename, setting);
					break;
				}
			case VariableType.String:{
					os=array.Values;
					string[] ss=new string[os.Length];
					for(int i=0;i<ss.Length;i++){
						ss[i]=(string)os[i];
					}
					ES2.Save(ss, filename, setting);
					break;
				}
			default:{
					break;
				}
			}
		}
		public static FsmArray LoadArray(ref FsmArray array,string filename,ES2Settings setting){
			
//			FsmArray array=new FsmArray();
			VariableType t=array.ElementType;
			switch(t){
			case VariableType.Bool:{
					array.boolValues=ES2.LoadArray<bool>(filename, setting);
					break;
				}
			case VariableType.Int:{
					int[] ints=ES2.LoadArray<int>(filename, setting);
					array.intValues=ints;
					break;
				}
			case VariableType.Float:{
					array.floatValues=ES2.LoadArray<float>(filename, setting);
					break;
				}
			case VariableType.String:{
					array.stringValues=ES2.LoadArray<string>(filename, setting);
					break;
				}
			default:{
					break;
				}
			}

			return array;
		}
	}
	[ActionCategory("Easy Save 2")]
	public class SaveArray : ES2SaveAction {

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The data we want to save.")]
		public FsmArray saveValue;
		public override void Reset()
		{
			saveValue = null;
			base.Reset (); // Ensure that base.Reset() is called when done.
		}

		public override void OnEnter()
		{
			try
			{
				
				EasySaveArrayTools.SaveArray(saveValue,filename.Value,GetSettings(new ES2Settings()));
//				ES2.Save(objs, filename.Value, GetSettings(new ES2Settings()));
				base.OnEnter(); // Ensure that base.OnEnter() is called when done.
			}
			catch(System.Exception e)
			{
				if(ifError != null)
				{
					LogError(e.Message);
					Fsm.Event(ifError);
				}
			}
			base.OnEnter(); // Ensure that base.OnEnter() is called when done.
		}
	}
	[ActionCategory("Easy Save 2")]
	public class LoadArray:ES2LoadAction{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The data we we want to load.")]
		public FsmArray loadValue;
		public override void Reset()
		{
			loadValue = null;
			base.Reset (); // Ensure that base.Reset() is called when done.
		}

		public override void OnEnter()
		{
			try
			{
				EasySaveArrayTools.LoadArray(ref loadValue,filename.Value,GetSettings(new ES2Settings()));
//				loadValue.CopyValues(fa);
//				loadValue.Values = ES2.LoadArray(filename.Value, GetSettings(new ES2Settings()));
				base.OnEnter(); // Ensure that base.OnEnter() is called when done.
			}
			catch(System.Exception e)
			{
				if(ifError != null)
				{
					LogError(e.Message);
					Fsm.Event(ifError);
				}
			}
		}
	}
	[ActionCategory("Easy Save 2")]
	public class SaveAllPlus : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name or absolute path of the file where our data will be stored. If the file doesn't exist, it will be created.")]
		public FsmString filename = "defaultFile.txt";
		[Tooltip("Whether to encrypt the data or not. If set to true, you must set an encryption password.")]
		public FsmBool encrypt = ES2GlobalSettings.defaultEncrypt;
		[Tooltip("The password to use for encryption if it is enabled.")]
		public FsmString encryptionPassword = ES2GlobalSettings.defaultEncryptionPassword;
		[Tooltip("Save the local variables accessible in this FSM?")]
		public FsmBool saveFsmVariables = true;
		[Tooltip("Save the global variables accessible in all FSMs?")]
		public FsmBool saveGlobalVariables = true;
		[Tooltip("This event is called if an error occurs.")]
		public FsmEvent ifError = null;
		[Tooltip("Set PlayerPrefs to default save location.")]
		public FsmBool usePlayerPrefs = true;

		public override void Reset()
		{
			filename.Value = "defaultFile.txt";
			encrypt.Value = ES2GlobalSettings.defaultEncrypt;
			encryptionPassword.Value = ES2GlobalSettings.defaultEncryptionPassword;
		}

		public override void OnEnter()
		{
			try
			{
				ES2Settings settings = new ES2Settings();
				settings.encrypt = encrypt.Value;
				settings.encryptionPassword = encryptionPassword.Value;
				if(usePlayerPrefs.Value)
					settings.saveLocation = ES2Settings.SaveLocation.PlayerPrefs;

				// Get FSMVariables objects required based on whether the user wants to save
				// local variables, global variables or both.
				FsmVariables[] fsmVariables;
				if(saveFsmVariables.Value && saveGlobalVariables.Value)
					fsmVariables = new FsmVariables[]{Fsm.Variables, FsmVariables.GlobalVariables};
				else if(saveFsmVariables.Value && !saveGlobalVariables.Value)
					fsmVariables = new FsmVariables[]{Fsm.Variables};
				else if(!saveFsmVariables.Value && saveGlobalVariables.Value)
					fsmVariables = new FsmVariables[]{FsmVariables.GlobalVariables};
				else
					fsmVariables = new FsmVariables[0];

				foreach(FsmVariables fsmVariable in fsmVariables)
				{
					// Variables are stored in seperate arrays based on their types.
					// Save each item in each array seperately.
					foreach(FsmBool fsmVar in fsmVariable.BoolVariables)
					{
						settings.tag = fsmVar.Name;
						ES2.Save(fsmVar.Value, filename.Value, settings);
					}
					foreach(FsmArray fsmVar in fsmVariable.ArrayVariables){
						settings.tag=fsmVar.Name;
						EasySaveArrayTools.SaveArray(fsmVar,filename.Value,settings);
					}
					foreach(FsmFloat fsmVar in fsmVariable.FloatVariables)
					{
						settings.tag = fsmVar.Name;
						ES2.Save(fsmVar.Value, filename.Value, settings);
					}

					foreach(FsmInt fsmVar in fsmVariable.IntVariables)
					{
						settings.tag = fsmVar.Name;
						ES2.Save(fsmVar.Value, filename.Value, settings);
					}

					foreach(FsmString fsmVar in fsmVariable.StringVariables)
					{
						settings.tag = fsmVar.Name;
						ES2.Save(fsmVar.Value, filename.Value, settings);
					}

					foreach(FsmVector2 fsmVar in fsmVariable.Vector2Variables)
					{
						settings.tag = fsmVar.Name;
						ES2.Save(fsmVar.Value, filename.Value, settings);
					}

					foreach(FsmVector3 fsmVar in fsmVariable.Vector3Variables)
					{
						settings.tag = fsmVar.Name;
						ES2.Save(fsmVar.Value, filename.Value, settings);
					}

					foreach(FsmRect fsmVar in fsmVariable.RectVariables)
					{
						settings.tag = fsmVar.Name;
						ES2.Save(fsmVar.Value, filename.Value, settings);
					}

					foreach(FsmQuaternion fsmVar in fsmVariable.QuaternionVariables)
					{
						settings.tag = fsmVar.Name;
						ES2.Save(fsmVar.Value, filename.Value, settings);
					}

					foreach(FsmColor fsmVar in fsmVariable.ColorVariables)
					{
						settings.tag = fsmVar.Name;
						ES2.Save(fsmVar.Value, filename.Value, settings);
					}

					foreach(FsmMaterial fsmVar in fsmVariable.MaterialVariables)
					{
						settings.tag = fsmVar.Name;
						ES2.Save(fsmVar.Value, filename.Value, settings);
					}

					foreach(FsmTexture fsmVar in fsmVariable.TextureVariables)
					{
						settings.tag = fsmVar.Name;
						ES2.Save(fsmVar.Value as Texture2D, filename.Value, settings);
					}

					foreach(FsmObject fsmVar in fsmVariable.ObjectVariables)
					{
						settings.tag = fsmVar.Name;
						ES2.Save(fsmVar.Value, filename.Value, settings);
					}
				}

				Log("Saved to "+filename.Value);
				Finish();
			}
			catch(Exception e)
			{
				if(ifError != null)
					Fsm.Event(ifError);
				else
					LogError (e.Message);
			}
		}
	}
	[ActionCategory("Easy Save 2")]
	public class LoadAllPlus : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name or absolute path of the file where our data will be stored. If the file doesn't exist, it will be created.")]
		public FsmString filename = "defaultFile.txt";
		[Tooltip("Whether to encrypt the data or not. If set to true, you must set an encryption password.")]
		public FsmBool encrypt = ES2GlobalSettings.defaultEncrypt;
		[Tooltip("The password to use for encryption if it is enabled.")]
		public FsmString encryptionPassword = ES2GlobalSettings.defaultEncryptionPassword;
		[Tooltip("Load the local variables accessible in this FSM?")]
		public FsmBool loadFsmVariables = true;
		[Tooltip("Load the global variables accessible in all FSMs?")]
		public FsmBool loadGlobalVariables = true;
		[Tooltip("This event is called if an error occurs.")]
		public FsmEvent ifError = null;
		[Tooltip("Set PlayerPrefs to default save location.")]
		public FsmBool usePlayerPrefs = true;

		public override void Reset()
		{
			filename.Value = "defaultFile.txt";
			encrypt.Value = ES2GlobalSettings.defaultEncrypt;
			encryptionPassword.Value = ES2GlobalSettings.defaultEncryptionPassword;
		}

		public override void OnEnter()
		{
			try
			{
				if(!ES2.Exists(filename.Value))
				{
					Finish();
					return;
				}

				ES2Settings settings = new ES2Settings(filename.Value);
				settings.encrypt = encrypt.Value;
				settings.encryptionPassword = encryptionPassword.Value;
				if(usePlayerPrefs.Value)
					settings.saveLocation = ES2Settings.SaveLocation.PlayerPrefs;

				ES2Data es2Data = ES2.LoadAll(filename.Value, settings);

				if(es2Data.loadedData.Count < 1)
				{
					Finish();
					return;
				}

				// Get FSMVariables objects required based on whether the user wants to save
				// local variables, global variables or both.
				FsmVariables[] fsmVariables;
				if(loadFsmVariables.Value && loadGlobalVariables.Value)
					fsmVariables = new FsmVariables[]{Fsm.Variables, FsmVariables.GlobalVariables};
				else if(loadFsmVariables.Value && !loadGlobalVariables.Value)
					fsmVariables = new FsmVariables[]{Fsm.Variables};
				else if(!loadFsmVariables.Value && loadGlobalVariables.Value)
					fsmVariables = new FsmVariables[]{FsmVariables.GlobalVariables};
				else
					fsmVariables = new FsmVariables[0];

				foreach(KeyValuePair<string, object> entry in es2Data.loadedData)
				{
					bool arrayMark=false;
					ES2Type es2Type = ES2TypeManager.GetES2Type(entry.Value.GetType());						
					if(es2Type == null){
						var obj=entry.Value;
						string fn=obj.GetType().FullName;
						if(obj.GetType()!=typeof(object[]))
							continue;
						arrayMark=true;
					}

					foreach(FsmVariables variable in fsmVariables)
					{	
						if(arrayMark){
							FsmArray thisVar=variable.FindFsmArray(entry.Key);
							if(thisVar!=null){
								thisVar.Values=(object[])entry.Value;
							}
							continue;
						}

						if(es2Type.type == typeof(bool))
						{
							FsmBool thisVar = variable.FindFsmBool(entry.Key);
							if(thisVar != null)
								thisVar.Value = es2Data.Load<bool>(entry.Key);
						}
						else if(es2Type.type == typeof(float))
						{
							FsmFloat thisVar = variable.FindFsmFloat(entry.Key);
							if(thisVar != null)
								thisVar.Value = es2Data.Load<float>(entry.Key);
						}
						else if(es2Type.type == typeof(int))
						{
							FsmInt thisVar = variable.FindFsmInt(entry.Key);
							if(thisVar != null)
								thisVar.Value =es2Data.Load<int>(entry.Key);
						}
						else if(es2Type.type == typeof(string))
						{
							FsmString thisVar = variable.FindFsmString(entry.Key);
							if(thisVar != null)
								thisVar.Value = es2Data.Load<string>(entry.Key);
						}
						else if(es2Type.type == typeof(Vector2))
						{
							FsmVector2 thisVar = variable.FindFsmVector2(entry.Key);
							if(thisVar != null)
								thisVar.Value =es2Data.Load<Vector2>(entry.Key);
						}
						else if(es2Type.type == typeof(Vector3))
						{
							FsmVector3 thisVar = variable.FindFsmVector3(entry.Key);
							if(thisVar != null)
								thisVar.Value =es2Data.Load<Vector3>(entry.Key);
						}
						else if(es2Type.type == typeof(Rect))
						{
							FsmRect thisVar = variable.FindFsmRect(entry.Key);
							if(thisVar != null)
								thisVar.Value = es2Data.Load<Rect>(entry.Key);
						}
						else if(es2Type.type == typeof(Quaternion))
						{
							FsmQuaternion thisVar = variable.FindFsmQuaternion(entry.Key);
							if(thisVar != null)
								thisVar.Value =es2Data.Load<Quaternion>(entry.Key);
						}
						else if(es2Type.type == typeof(Color))
						{
							FsmColor thisVar = variable.FindFsmColor(entry.Key);
							if(thisVar != null)
								thisVar.Value = es2Data.Load<Color>(entry.Key);
						}
						else if(es2Type.type == typeof(Material))
						{
							FsmMaterial thisVar = variable.FindFsmMaterial(entry.Key);
							if(thisVar != null)
								thisVar.Value = es2Data.Load<Material>(entry.Key);
						}
						else if(es2Type.type == typeof(Texture2D))
						{
							FsmTexture thisVar = variable.FindFsmTexture(entry.Key);
							if(thisVar != null)
								thisVar.Value = es2Data.Load<Texture2D>(entry.Key);
						}
						else
						{
							FsmObject thisVar = variable.FindFsmObject(entry.Key);
							if(thisVar != null)
								thisVar.Value = entry.Value as UnityEngine.Object;
						}
					}
				}

				Log("Loaded from "+filename.Value);
				Finish();
			}
			catch(System.Exception e)
			{
				if(ifError != null)
				{
					LogError(e.Message);
					Fsm.Event(ifError);
				}
			}
		}
	}
}