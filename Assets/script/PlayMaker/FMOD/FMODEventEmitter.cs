using UnityEngine;
using System;
using FMODUnity;
namespace HutongGames.PlayMaker.Actions
{

[ActionCategory("FMOD")]
[Tooltip("play FMOD event")]
public class FMODPlayEventEmitter : FsmStateAction
{

		[RequiredField]
		[CheckForComponent(typeof(FMODUnity.StudioEventEmitter))]
		public FsmOwnerDefault gameObject;
		public StudioEventEmitter emitter;

		public override void Reset ()
		{
			base.Reset ();

			gameObject=null;
		
		}
	// Code that runs on entering the state.
	public override void OnEnter()
	{
			if(emitter!=null){
//				emitter.Play ();

			}	
			
		Finish();
	}

	// Code that runs when exiting the state.
	public override void OnExit()
	{
			
	}


}
	[ActionCategory("FMOD")]
	[Tooltip("play FMOD event")]
	public class FMODSetEventEmitterParameter : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(FMODUnity.StudioEventEmitter))]
		public FsmOwnerDefault gameObject;
		public StudioEventEmitter emitter;
		public FsmString parameterName;
		public FsmFloat paramaterValue;

		public override void Reset ()
		{
			base.Reset ();
			parameterName=null;
			paramaterValue=null;
			gameObject=null;

		}
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			if(emitter!=null){
				ParamRef[] ps=emitter.Params;
				ParamRef pr=null;
				foreach(var prin in ps){
					if(prin.Name==parameterName.Value){
						pr=prin;
						break;
					}
				}
				if(pr==null&&ps.Length>0){
					pr=ps[0];
				}
				if(pr!=null)
				pr.Value=paramaterValue.Value;
			}	

			Finish();
		}

		// Code that runs when exiting the state.
		public override void OnExit()
		{

		}


	}


}
