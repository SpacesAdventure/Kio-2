using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Vector2")]

	public class Vector2MultiplyFloat : FsmStateAction {

		[RequiredField]
		[Tooltip("The first vector")]
		public FsmVector2 vector1;
		[RequiredField]
		[Tooltip("The float")]
		public FsmFloat number;
		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector2 result when it applies.")]
		public FsmVector2 storeVector2Result;

		public override void Reset ()
		{
			vector1=null;
			number=null;
			storeVector2Result=null;
		}
		public override void OnEnter ()
		{
			storeVector2Result.Value=vector1.Value*number.Value;
			Finish();
		}
	}
}