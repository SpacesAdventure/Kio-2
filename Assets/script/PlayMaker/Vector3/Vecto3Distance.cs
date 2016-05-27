using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("get the distance between two vector3.")]
public class Vecto3Distance : FsmStateAction
{
		[RequiredField]
		public FsmVector3 vector1;
		public FsmVector3 vector2;
		[UIHint(UIHint.Variable)]
		public FsmFloat result;
		public override void Reset ()
		{
			vector1=null;
			vector2=null;
			result=null;
		}
	// Code that runs on entering the state.
	public override void OnEnter()
	{
			result.Value=Vector3.Distance(vector1.Value,vector2.Value);
		Finish();
	}


}

}
