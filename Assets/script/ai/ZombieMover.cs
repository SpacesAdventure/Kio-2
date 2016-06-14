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
	bool canSearch=true;
	public float repathRate = 0.5F;
	protected float lastRepath = -9999;
	// Use this for initialization
	void Start () {
		seeker=GetComponent<Seeker>();
		StartCoroutine(keepSeeking());
		seeker.pathCallback += OnPathComplete;

	}
	IEnumerator keepSeeking(){
		while(true){
			float v=TrySearchPath();
			yield return new WaitForSeconds(v);

		}
	}
	protected virtual void OnEnable () {
		lastRepath = -9999;
		canSearch = true;
//		seeker.pathCallback += OnPathComplete;
	}
	void searchPath(){
		lastRepath = Time.time;
		//This is where we should search to

		canSearch = false;
		seeker.StartPath(transform.position,player.transform.position);
	}
	float TrySearchPath () {
		if(player==null){
			FsmVariables fvs= PlayMakerGlobals.Instance.Variables;
			FsmGameObject fmgo=fvs.FindFsmGameObject("player");
			if(fmgo!=null){
				player=fmgo.Value;
			}
//			return;
		}
		if (Time.time - lastRepath >= repathRate && canSearch && player != null) {
			searchPath ();
			return repathRate;
		} else {
			//StartCoroutine (WaitForRepath ());
			float v = repathRate - (Time.time-lastRepath);
			return v < 0 ? 0 : v;
		}
	}
	// Update is called once per frame
	void Update () {
//		if(player==null){
//			FsmVariables fvs= PlayMakerGlobals.Instance.Variables;
//			FsmGameObject fmgo=fvs.FindFsmGameObject("player");
//			if(fmgo!=null){
//				player=fmgo.Value;
//			}
//			return;
//		}
//		if(canSearch){
//			canSearch=false;
//			seeker.StartPath(transform.position,player.transform.position,OnPathComplete);
//		}


	}
	public void OnPathComplete (Path p) {
		path=p;
		points=path.vectorPath.ToArray();
		if(points.Length<=3){
			nextDestination= points[points.Length-1];
		}else{
			nextDestination=points[1];
		}
		canSearch=true;
	}
	public Vector3 getClosesPoint(){
		
		return nextDestination;
	}
	public int getPointsSize(){
		return points.Length;
	}
}
