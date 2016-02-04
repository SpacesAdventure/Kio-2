using UnityEngine;
using System.Collections;

public class ParticleRearranger : MonoBehaviour {
	ParticleSystem particle;
	public string sortLayer="Default";
	public int sortOrder=0;
	void Start(){
		particle = GetComponent<ParticleSystem> ();
		particle.GetComponent<Renderer>().sortingLayerName =sortLayer;
		particle.GetComponent<Renderer>().sortingOrder=sortOrder;

	}
	// Use this for initialization

}
