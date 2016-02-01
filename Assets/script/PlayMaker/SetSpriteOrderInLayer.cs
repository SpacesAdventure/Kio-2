using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Sprite")]
	[Tooltip("Set the sorting order in a SpriteRendered. Optionally set all SpriteRendered found on the gameobject Target.")]
	public class SetSpriteOrderInLayer : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		public FsmInt order;


		public bool setAllSpritesInChildren;

		public override void Reset()
		{
			gameObject = null;
			order = null;
			setAllSpritesInChildren = false;
		}

		public override string ErrorCheck ()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);

			if (go!=null && ! setAllSpritesInChildren && go.GetComponent<SpriteRenderer>() == null ){
				return "Missing SpriteRenderer on GameObject";
			}

			return "";
		}
		public override void OnEnter()
		{
			DoSetLayerName();
			Finish();
		}

		void DoSetLayerName()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;


			if (setAllSpritesInChildren)
			{
				SpriteRenderer[] sprites = go.GetComponentsInChildren<SpriteRenderer> ();
				foreach (SpriteRenderer _sprite in sprites) {
					_sprite.sortingOrder = order.Value;
				}
			}else{
				if (go.GetComponent<SpriteRenderer>() != null)go.GetComponent<SpriteRenderer>().sortingOrder  = order.Value;
			}
		}
	}
}

