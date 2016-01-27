using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

public class CameraFollower : MonoBehaviour {
	
	Camera camera;
	public float distance=0.4f;
	public float distancey=0.55f;
	public GameObject player;
	void Awake(){
		
		camera = Camera.main;
	}
	// Update is called once per frame
	void Update () {
		if(player==null){
			FsmVariables vs=PlayMakerGlobals.Instance.Variables;
			FsmGameObject fsgo=vs.GetFsmGameObject("player");
			if(fsgo!=null)
			player=fsgo.Value;
		}
		
//		float side = camera.aspect * camera.orthographicSize;
		float sidex=1.3f;
		float sidey=0.9f;
//		print (side);
		if (player != null) {
			Vector3 pp=player.transform.position;
			Vector3 cp=camera.transform.position;
			if(pp.x<-sidex+distance){
				cp.x=pp.x+sidex-distance;
			}
			if(pp.x>sidex-distance){
				cp.x=pp.x-sidex+distance;
			}
			if(Mathf.Abs(cp.x)<0.005f){
				cp.x=0;
			}
			if(pp.y<-sidey+distancey){
				cp.y=pp.y+sidey-distancey;
			}
			if(pp.y>sidey-distancey){
				cp.y=pp.y-sidey+distancey;
			}
			if(Mathf.Abs(cp.y)<0.005f){
				cp.y=0;
			}
			camera.transform.position=cp;
		}
	}
}
