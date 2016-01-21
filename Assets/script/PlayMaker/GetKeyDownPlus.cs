using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends an Event when a Key is pressed.")]
	public class GetKeyDownPlus : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString keyCode;
//		public KeyCode key;
		public FsmEvent sendEvent;
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;

		public override void Reset()
		{
			sendEvent = null;
			keyCode=null;
			storeResult = null;
		}

		public override void OnUpdate()
		{
			
			string key=keyCode.Value;

			bool keyDown = Input.GetKeyDown(key);

			if (keyDown)
				Fsm.Event(sendEvent);

			storeResult.Value = keyDown;

		}
	}
}
