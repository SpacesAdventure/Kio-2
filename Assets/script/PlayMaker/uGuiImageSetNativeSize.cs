using UnityEngine;
using System.Collections;
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the native size of a UGui Image component.")]
	public class uGuiImageSetNativeSize : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Image))]
		[Tooltip("The GameObject with the Image ui component.")]
		public FsmOwnerDefault gameObject;

//		[RequiredField]
//		[Tooltip("The source sprite of the UGui Image component.")]
//		[ObjectType(typeof(UnityEngine.Sprite))]
//		public FsmObject sprite;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		UnityEngine.UI.Image _image;
//		UnityEngine.Sprite _originalSprite;


		public override void Reset()
		{
			gameObject = null;
			resetOnExit = false;
		}

		public override void OnEnter()
		{
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_image = _go.GetComponent<UnityEngine.UI.Image>();
			}

			DoSetImageSourceValue();

			Finish();
		}

		void DoSetImageSourceValue()
		{
			if (_image==null)
			{
				return;
			}

			if (resetOnExit.Value)
			{
//				_originalSprite = _image.sprite;
			}

			_image.SetNativeSize();
		}

		public override void OnExit()
		{
			if (_image==null)
			{
				return;
			}

			if (resetOnExit.Value)
			{
//				_image.sprite = _originalSprite;
			}
		}

	}
}