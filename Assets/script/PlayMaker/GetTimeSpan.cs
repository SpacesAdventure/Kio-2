using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Gets time span between two time")]
	public class GetTimeSpan : FsmStateAction
	{
		
		[Tooltip("Store System DateTime as a string.")]
		public FsmString startTime;
		public FsmString currentTime;
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
			startTime=null;
			currentTime=null;
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
			DateTime st=DateTime.Parse(startTime.Value);
			DateTime ct=DateTime.Parse(currentTime.Value);
			TimeSpan ts=ct-st;
			string r=ts.ToString();
			timeSpan.Value=r;

		}
		public override void OnUpdate()
		{
//			storeString.Value = DateTime.Now.ToString(format.Value);
		}
	}
}