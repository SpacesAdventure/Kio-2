using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Pathfinding;

public class ZombieMover : MonoBehaviour {
	GameObject player;
	public Vector3[] points;
	Vector3 nextDestination;
	Path path;
	Seeker seeker;
	// Use this for initialization
	void Start () {
		seeker=GetComponent<Seeker>();
	}
	
	// Update is called once per frame
	void Update () {
		if(player==null){
			FsmVariables fvs= PlayMakerGlobals.Instance.Variables;
			FsmGameObject fmgo=fvs.FindFsmGameObject("player");
			if(fmgo!=null){
				player=fmgo.Value;
			}
			return;
		}
		seeker.StartPath(transform.position,player.transform.position,OnPathComplete);
		points=path.vectorPath.ToArray();
		nextDestination=points[1];
	}
	public void OnPathComplete (Path p) {
		path=p;
	}
	public Vector3 getClosesPoint(){
		return nextDestination;
	}
	public int getPointsSize(){
		return points.Length;
	}
}
