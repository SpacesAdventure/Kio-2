using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory(ActionCategory.Logic)]
public class ActivateComponent : FsmStateAction
{
		[RequiredField]
		public Component component;
		public FsmBool active;

		public override void Reset ()
		{
			base.Reset ();
			component=null;
			active=null;
		}
	// Code that runs on entering the state.
	public override void OnEnter()
	{
			(component as Behaviour).enabled=active.Value;
		Finish();
	}


}

}
