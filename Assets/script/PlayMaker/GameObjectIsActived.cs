using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if a GameObject Variable has a null value. E.g., If the FindGameObject action failed to find an object.")]
	public class GameObjectIsActived : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The GameObject variable to test.")]
		public FsmGameObject gameObject;

		[Tooltip("Event to send if the GamObject is null.")]
		public FsmEvent isActived;

		[Tooltip("Event to send if the GamObject is NOT null.")]
		public FsmEvent isNotActived;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a bool variable.")]
		public FsmBool storeResult;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			isActived = null;
			isNotActived = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoIsGameObjectNull();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoIsGameObjectNull();
		}

		void DoIsGameObjectNull()
		{
			if(gameObject.Value==null){
				Fsm.Event(isNotActived);
				if (storeResult != null)
				{
					storeResult.Value = false;
				}
				return;
			}
			var goIsNull = gameObject.Value.activeInHierarchy;

			if (storeResult != null)
			{
				storeResult.Value = goIsNull;
			}

			Fsm.Event(goIsNull ? isActived : isNotActived);
		}
	}
}