using UnityEngine;
using System.Collections;

public class SpritesTwinkle : MonoBehaviour {
	public string tag;
	public bool twinkle=false;
	int twinkleCount=0;
	const int maxTwinkleCount=5;
	bool showed=false;
	public SpriteRenderer[] sprites;
	void Awake(){
		getSprites();
		
	}
	// Use this for initialization
	void Start () {
	
	}
	public void getSprites(){
		sprites=GetComponentsInChildren<SpriteRenderer>();
	}
	void Update(){
		if(twinkle){
			if(twinkleCount>=maxTwinkleCount){
				twinkleCount=0;
				setTagGroupVisible(showed);
				showed=!showed;
			}
			twinkleCount++;
		}
	}
	void setVisible(SpriteRenderer spr,bool visible){
		Color c=spr.color;
		if(visible){
			c.a=1;
		}else{
			c.a=0;
		}
		spr.color=c;
	}
	void setTagGroupVisible(bool visible){
		foreach(SpriteRenderer spr in sprites){
			if(spr.tag==tag){
				setVisible(spr,visible);
			}
		}
	}
//	IEnumerator twinkleStep(){
//		while(true){
//			setTagGroupVisible(showed);
//			showed=!showed;
//			yield return new WaitForSeconds(1.2f);
//		}
//	}
	public void start(){
		twinkle=true;
	}
	public void stop(){
		twinkle=false;
		twinkleCount=maxTwinkleCount;
		setTagGroupVisible(true);
	}
}
