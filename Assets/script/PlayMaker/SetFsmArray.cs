using UnityEngine;
using System.Collections;
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Set the value of a Array Variable from another FSM.")]
	public class SetFsmArray : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;
		[RequiredField]
		[UIHint(UIHint.FsmString)]
		public FsmString variableName;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmArray Value;
		public bool everyFrame;

		GameObject goLastFrame;
		PlayMakerFSM fsm;

		public override void Reset()
		{
			gameObject = null;
			fsmName = "";
			Value = null;
		}

		public override void OnEnter()
		{
			DoGetFsmString();

			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoGetFsmString();
		}

		void DoGetFsmString()
		{
			if (Value == null) return;

			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;

			// only get the fsm component if go has changed

			if (go != goLastFrame)
			{
				goLastFrame = go;
				fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
			}

			if (fsm == null) return;

			FsmArray fsmString = fsm.FsmVariables.GetFsmArray(variableName.Value);

			if (fsmString == null) return;

			fsmString.Values=Value.Values;
		}

	}
}