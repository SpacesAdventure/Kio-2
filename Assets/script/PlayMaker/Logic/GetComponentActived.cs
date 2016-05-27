using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory(ActionCategory.Logic)]
public class GetComponentActived : FsmStateAction
{
		[RequiredField]
		public Component component;
		[UIHint(UIHint.Variable)]
		public FsmBool storeValue;
		public override void Reset ()
		{
			base.Reset ();
			component=null;
			storeValue=null;
		}
	// Code that runs on entering the state.
	public override void OnEnter()
	{
			storeValue.Value=(component as Behaviour).enabled;
		Finish();
	}


}

}
