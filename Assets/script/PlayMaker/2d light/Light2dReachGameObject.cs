using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory("2d light")]
public class Light2dReachGameObject : FsmStateAction
{
		[RequiredField]
		[CheckForComponent(typeof(DynamicLight))]
		public FsmOwnerDefault gameobject;
		[RequiredField]
		[UIHint(UIHint.Tag)]
		[Tooltip("Filter by Tag.")]
		public FsmString reachTag;
		[RequiredField]
		[Tooltip("Event to send if a light is detected.")]
		public FsmEvent sendEvent;
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
		public FsmGameObject storeGameobject;

		DynamicLight light2d;
	// Code that runs on entering the state.
		public override void Reset ()
		{
			base.Reset ();
			gameobject=null;
			reachTag = new FsmString(){UseVariable=true};
			sendEvent = null;
			storeGameobject=null;

		}
	public override void OnEnter()
	{
			light2d=gameobject.GameObject.Value.GetComponent<DynamicLight>();
			light2d.OnReachedGameObjects+=reachGameobjects;
//		Finish();
	}

	// Code that runs when exiting the state.
	public override void OnExit()
	{
		
	}
		void reachGameobjects(GameObject[] objs){
			if(objs.Length>0){
				GameObject go=null;
				if(reachTag.Value.Length>0){
					foreach(GameObject goo in objs){
						if(goo.CompareTag(reachTag.Value)){
							go=goo;
							break;
						}
					}
				}
				if(storeGameobject!=null&&go!=null){
					storeGameobject.Value=go;
					Fsm.Event(sendEvent);
				}

			}
		}


}

}
