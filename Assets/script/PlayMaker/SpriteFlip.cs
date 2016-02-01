using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("sprite")]
	[Tooltip("Flip a single Sprite on any GameObject. Object must have a Sprite Renderer.")]
	public class FlipSprite : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(SpriteRenderer))]
		public FsmOwnerDefault gameObject;

		public FsmBool resetOnExit;

		private SpriteRenderer spriteRenderer;

		public FsmBool x;
		public FsmBool y;
		bool _x;
		bool _y;

		public override void Reset()
		{
			gameObject = null;
			resetOnExit = false;
			x=null;
			y=null;

		}

		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;

			spriteRenderer = go.GetComponent<SpriteRenderer>();

			if (spriteRenderer == null)
			{
				LogError("SwapSingleSprite: Missing SpriteRenderer!");
				return;
			}
			_x=spriteRenderer.flipX;
			_y=spriteRenderer.flipY;


			filp();

			Finish();
		}

		public override void OnExit()
		{
			if (resetOnExit.Value)
			{
				spriteRenderer.flipX=_x;
				spriteRenderer.flipY=_y;
			}
		}
		void filp()
		{
			spriteRenderer.flipX=x.Value;
			spriteRenderer.flipY=y.Value;
		}



	}
}
