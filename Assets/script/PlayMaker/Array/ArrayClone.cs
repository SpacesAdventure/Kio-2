using UnityEngine;
using System.Collections;
namespace HutongGames.PlayMaker.Actions
{

[ActionCategory(ActionCategory.Array)]
public class ArrayClone : FsmStateAction
{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmArray source;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmArray value;
		public override void Reset ()
		{
			base.Reset ();
			source=null;
			value=null;
		}

	// Code that runs on entering the state.
	public override void OnEnter()
	{
			if(source.Length>0)
			value.Values=source.Values;
		Finish();
	}

	// Code that runs when exiting the state.
	public override void OnExit()
	{
		
	}


}

}
