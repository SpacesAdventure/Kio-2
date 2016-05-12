using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectSortLayerArrangement : MonoBehaviour {
	public static GameObjectSortLayerArrangement instance;
	public List<GameObject> inViewObjects=new List<GameObject>();
	public float distance=0.01f;
	void Awake(){
		instance=this;
	}
	// Use this for initialization
	void Start () {
	
//		if(inViewObjects.Count>0){
//			inViewObjects.Sort(delegate(GameObject a,GameObject b) {
//				return a.transform.position.y.CompareTo(b.transform.position.y)*-1;
//			});
//
//		}
	}
	
	// Update is called once per frame
	void Update () {
		if(inViewObjects.Count>0){
			inViewObjects.Sort(delegate(GameObject a,GameObject b) {
				return a.transform.position.y.CompareTo(b.transform.position.y)*-1;
		});
		}
		int j=0;
		for(int i=0;i<inViewObjects.Count;i++){
			GameObject go=inViewObjects[i];
			SortLayerMarker slm=go.GetComponent<SortLayerMarker>();
			if(slm){
				
				if(i>0){
					GameObject gop=inViewObjects[i-1];
					if(gop!=null){
						float yp=gop.transform.position.y;
						float y=go.transform.position.y;
						if(Mathf.Abs(yp-y)>=distance){
							j++;
						}
					}
				}
				slm.setSortedLayer("g"+j.ToString());
			}
		}
//		print(j);
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
