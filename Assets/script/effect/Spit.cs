using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker.Actions;

public class Spit : MonoBehaviour {
	public PlayMakerFSM fsm;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void shoot(){
		fsm.Fsm.Event("down");
	}
}
