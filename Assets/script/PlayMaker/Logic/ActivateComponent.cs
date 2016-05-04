using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory(ActionCategory.Logic)]
public class ActivateComponent : FsmStateAction
{
		[RequiredField]
		public Component component;


		public override void Reset ()
		{
			base.Reset ();
			component=null;

		}
	// Code that runs on entering the state.
	public override void OnEnter()
	{

		Finish();
	}


}

}
