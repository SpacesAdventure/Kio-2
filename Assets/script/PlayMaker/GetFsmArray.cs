using UnityEngine;
using System.Collections;
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Get the value of a Array Variable from another FSM.")]
	public class GetFsmArray : FsmStateAction
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
		public FsmArray storeValue;
		public bool everyFrame;

		GameObject goLastFrame;
		PlayMakerFSM fsm;

		public override void Reset()
		{
			gameObject = null;
			fsmName = "";
			storeValue = null;
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
			if (storeValue == null) return;

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

			storeValue.Values = fsmString.Values;
		}

	}
}