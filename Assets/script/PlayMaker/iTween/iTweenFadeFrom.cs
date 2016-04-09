using UnityEngine;
using System.Collections;
namespace HutongGames.PlayMaker.Actions
{

[ActionCategory(ActionCategory.iTween)]
public class iTweenFadeFrom : FsmStateAction
{
		[Tooltip("The gameObject")]
		public FsmOwnerDefault gameObject;

		public FsmFloat time;
		[HasFloatSlider(0, 1)]
		public FsmFloat alpha;

		public iTween.EaseType easeType = iTween.EaseType.linear;

		Hashtable hash;	
		GameObject go;
		public override void Reset ()
		{
			time=1f;
			alpha=1f;
		}
	// Code that runs on entering the state.
	public override void OnEnter()
	{
			hash=new Hashtable();
			go=Fsm.GetOwnerDefaultTarget(gameObject);
			if(go==null){
				Finish();
				return;
			}
			hash.Add("alpha",alpha.Value);
			hash.Add("easetype", easeType);
			hash.Add("time",time.Value);
			iTween.FadeFrom(go,hash);
		Finish();
	}


}

}
