using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Physics 2d")]
	[Tooltip("Controls a collider isTrigger Flag")]
	public class Collider2dSetIsTrigger : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Collider2D))]
		[Tooltip("The GameObject with collider2d to control")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Is the collider a trigger or not")]
		public FsmBool isTrigger;

		[Tooltip("Reset the collider trigger flag on exit")]
		public FsmBool resetOnExit;

		private Collider2D _collider;
		bool _originalValue;

		public override void Reset()
		{
			gameObject = null;
			isTrigger = false;
			resetOnExit = false;
		}

		public override void OnEnter()
		{
			DoSetIsTrigger();

			Finish();
		}

		void DoSetIsTrigger()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;

			_collider = go.GetComponent<Collider2D>();

			if (_collider == null) return;

			if (resetOnExit.Value)
			{
				_originalValue = _collider.isTrigger;
			}

			_collider.isTrigger = isTrigger.Value;
		}

		public override void OnExit()
		{
			if (_collider==null)
			{
				return;
			}

			if (resetOnExit.Value)
			{
				_collider.isTrigger = _originalValue;
			}
		}
	}
}
