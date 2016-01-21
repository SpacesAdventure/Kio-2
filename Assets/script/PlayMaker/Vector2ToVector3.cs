using UnityEngine;
using System.Collections;
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Vector2")]
	[Tooltip("Convert vector2 to vector3.")]
	public class Vector2ToVector3 : FsmStateAction {

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector2 target")]
		public FsmVector2 vector;
		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector3 result when it applies.")]
		public FsmVector3 result;
		public override void Reset ()
		{
			vector=null;
			result=null;

		}
		public override void OnEnter ()
		{
			Vector3 v=Vector3.zero;
			Vector2 v2=vector.Value;
			v.x=v2.x;
			v.y=v2.y;
			result.Value=v;
			Finish();
		}
	}
}
