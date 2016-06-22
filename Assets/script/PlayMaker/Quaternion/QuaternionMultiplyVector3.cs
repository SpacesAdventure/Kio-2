using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory(ActionCategory.Quaternion)]
public class QuaternionMultiplyVector3 : FsmStateAction
{
		[RequiredField]
		public FsmQuaternion quaternion;
		public FsmVector3 vector3;
		[UIHint(UIHint.Variable)]
		public FsmVector3 result;

		public override void Reset ()
		{

			quaternion=null;
			vector3=null;
			result=null;
		}
	// Code that runs on entering the state.
	public override void OnEnter()
	{
			Quaternion q= quaternion.Value;
			Vector3 v=vector3.Value;
			Vector3 r=q*v;
			result.Value=r;
		Finish();
	}


}

}
