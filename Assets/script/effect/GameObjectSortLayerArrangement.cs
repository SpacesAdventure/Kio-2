using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectSortLayerArrangement : MonoBehaviour {
	public static GameObjectSortLayerArrangement instance;
	public List<GameObject> inViewObjects=new List<GameObject>();
	void Awake(){
		instance=this;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(inViewObjects.Count>0){
			inViewObjects.Sort(delegate(GameObject a,GameObject b) {
				return a.transform.position.y.CompareTo(b.transform.position.y)*-1;
		});
		}
		for(int i=0;i<inViewObjects.Count;i++){
			SortLayerMarker slm=inViewObjects[i].GetComponent<SortLayerMarker>();
			if(slm){
				slm.setSortedLayer("g"+i.ToString());
			}
		}
	}
	public void add(GameObject go){
		if(inViewObjects.Contains(go))
			return;
		inViewObjects.Add(go);
	}
	public void remove(GameObject go){
		inViewObjects.Remove(go);
	}
}
