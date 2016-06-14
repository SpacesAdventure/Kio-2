using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

public class CameraFollowerLimit : MonoBehaviour {
	Camera camera;
	public GameObject player;
	public float maxX;
	public float minX;
	public float maxY;
	public float minY;
	void Awake(){

		camera = Camera.main;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(player==null){
			FsmVariables vs=PlayMakerGlobals.Instance.Variables;
			FsmGameObject fsgo=vs.GetFsmGameObject("player");
			if(fsgo!=null)
				player=fsgo.Value;
		}
		Vector3 pposition=player.transform.position;
		pposition.z=-10;
		if(pposition.x>maxX)
			pposition.x=maxX;
		if(pposition.x<minX)
		pposition.x=minX;
		if(pposition.y>maxY)
			pposition.y=maxY;
		if(pposition.y<minY)
			pposition.y=minY;
		camera.transform.position=pposition;
	}
}
