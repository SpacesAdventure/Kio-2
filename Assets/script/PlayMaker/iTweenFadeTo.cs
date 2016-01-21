using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions{
	[ActionCategory("iTween")]
	[Tooltip("Fade sprites to ")]
	public class iTweenFadeTo : FsmStateAction {
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
		public override void OnEnter ()
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
			iTween.FadeTo(go,hash);
			Finish();
		}
	}
}