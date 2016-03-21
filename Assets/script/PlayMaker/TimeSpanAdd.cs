using UnityEngine;
using System.Collections;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Gets time span between two time")]
	public class TimeSpanAdd : FsmStateAction
	{

		[Tooltip("Store System DateTime as a string.")]
		public FsmString timeSpan1;
		public FsmString timeSpan2;
		[UIHint(UIHint.Variable)]
		public FsmString timeSpan;

		[Tooltip("Optional format string. E.g., MM/dd/yyyy HH:mm")]
		public FsmString format;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			//			storeString = null;
			format = "HH:mm";
			timeSpan1=null;
			timeSpan2=null;
			timeSpan=null;
		}

		public override void OnEnter()
		{
			getSpan();

			if (!everyFrame)
			{
				Finish();
			}
		}

		void getSpan(){
			TimeSpan ts1=TimeSpan.Parse(timeSpan1.Value);
			TimeSpan ts2=TimeSpan.Parse(timeSpan2.Value);
			TimeSpan ts=ts1+ts2;
			string r=ts.ToString();
			timeSpan.Value=r;

		}
		public override void OnUpdate()
		{
			//			storeString.Value = DateTime.Now.ToString(format.Value);
		}
	}
}