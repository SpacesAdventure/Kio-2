using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SortLayerMarker : MonoBehaviour {
	public GameObject[] sortObjects;

	// Use this for initialization
	void Start () {
		getSprites();
	}
	public void getSprites(){
		List<GameObject> list=loopAllChildrenByTag(transform,"layer");
		SpriteRenderer spr=GetComponent<SpriteRenderer>();
		if(spr){
			if(spr.tag==("layer"))
				list.Add(gameObject);
		}
		sortObjects=list.ToArray();
	}

	List<GameObject> loopAllChildrenByTag(Transform child,string tag){
		List<GameObject> list=new List<GameObject>();
		foreach(Transform c in child){
			if(c.gameObject.CompareTag(tag)){
				list.Add(c.gameObject);
			}
			if(c.childCount>0){
				List<GameObject> subL=loopAllChildrenByTag(c,tag);
				foreach(GameObject subGo in subL.ToArray()){
					list.Add(subGo);
				}
			}
		}
		return list;
	}
	void Update(){
		Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		bool isVisible = (Camera.main.orthographic || pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f);
		addSelf(isVisible);
	}
	void OnBecameVisible(){


	}
	void OnBecameInvisible(){


	}
	void OnDestroy(){
		addSelf(false);
	}
	void addSelf(bool add){
		if(GameObjectSortLayerArrangement.instance==null)
			return;
		if(add){
			GameObjectSortLayerArrangement.instance.add(gameObject);
		}else{
			GameObjectSortLayerArrangement.instance.remove(gameObject);
		}
	}
	public void setSortedLayer(string layer){
		foreach(GameObject go in sortObjects){
			if(go==null)
				break;
			SpriteRenderer spr=go.GetComponent<SpriteRenderer>();
			if(spr){
				spr.sortingLayerName=layer;
			}
			ParticleSystem ps=go.GetComponent<ParticleSystem>();
			if(ps){
				ps.GetComponent<Renderer>().sortingLayerName=layer;
			}
		}
	}
}
