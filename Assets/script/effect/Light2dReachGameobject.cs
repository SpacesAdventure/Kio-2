using UnityEngine;
using System.Collections;

public class Light2dReachGameobject : MonoBehaviour {
	DynamicLight light2d;
	// Use this for initialization
	void Start () {
		light2d=GetComponent<DynamicLight>();
		light2d.OnReachedGameObjects+=reachGameobjects;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void reachGameobjects(GameObject[] objs){
		print("er "+objs.Length.ToString());
		if(objs.Length>0){
			print("get");
//			GameObject go=objs[0];
//			if(reachTag.Value.Length>0){
//				foreach(GameObject goo in objs){
//					if(goo.CompareTag(reachTag.Value)){
//						go=goo;
//						break;
//					}
//				}
//			}
//			if(storeGameobject!=null){
//				storeGameobject.Value=go;
//			}
//			Fsm.Event(sendEvent);
		}
	}
}
