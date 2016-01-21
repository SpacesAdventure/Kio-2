using UnityEngine;
using System.Collections;
namespace HutongGames.PlayMaker.Actions{
	[ActionCategory("Physics 2d")]
	[Tooltip("Sets linear drag of this object  NOTE: Game object must have a rigidbody 2D.")]
	public class SetLinearDrag2d : FsmStateAction {
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		public FsmOwnerDefault gameObject;
		[RequiredField]
		public FsmFloat linearDrag;
		public override void Reset()
		{
			gameObject = null;
			linearDrag = 1f;
		}
		public override void OnEnter()
		{
			doSetLinearDrag();
			Finish();
		}
		void doSetLinearDrag(){
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			if (go.GetComponent<Rigidbody2D>() == null) return;
			go.GetComponent<Rigidbody2D>().drag=linearDrag.Value;
		}
	}
}
