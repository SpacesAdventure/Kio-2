using UnityEngine;
using System.Collections;

public class RendererRearranger : MonoBehaviour {
	MeshRenderer mrenderer;
	public string sortLayer="Default";
	public int sortOrder=0;
	void Awake(){
		mrenderer=GetComponent<MeshRenderer>();
	}
	// Use this for initialization
	void Start () {
		mrenderer.sortingLayerName=sortLayer;
		mrenderer.sortingOrder=sortOrder;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
