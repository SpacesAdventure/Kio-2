using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using HutongGames.PlayMaker.Actions;
using UnityEngine.UI;

	
public class TypeWriter : MonoBehaviour
	{
	MatchCollection mc;
	string[] typingText;
	Text label;
	public PlayMakerFSM fsm;
	float defaultDelay=0.04f;
	void Awake(){
		label=GetComponent<Text>();
	}
	void Start(){
		
	}
	public void typeText(string text,float delay)
		{
		if(label==null){
			sendFinishEvent();
			return;
		}
		label.text="";
		if(delay>0)
			defaultDelay=delay;
		
		string t=text;
//		t="asdb\\nasdg\\1.5hjkh\\nhjkhg";
		Regex reg=new Regex(@"\\n|\\\d{1,5}\.\d{0,2}|\\\d{1,5}");
		mc=reg.Matches(t);
		typingText=reg.Split(t);
		StopAllCoroutines();
		StartCoroutine(typing());
		}
	IEnumerator typing(){
		int i=0;
		foreach(string text in typingText){
			char[] chars=text.ToCharArray();
			if(chars.Length<=0)
				continue;
			foreach(char c in chars){
				label.text+=c;
				yield return new WaitForSeconds(defaultDelay);
			}
			if(i>=mc.Count)
				continue;
			string r=mc[i].Value;
			if(r=="\\n"){
				label.text+="\r\n";
//				yield return new WaitForSeconds(defaultDelay);
			}else{
				r=r.Replace("\\","");
				float ti=float.Parse(r);
				if(ti>0){
					yield return new WaitForSeconds(ti);
				}
			}
			i++;
		}
		yield return null;
		sendFinishEvent();
	}
	void sendFinishEvent(){
//		if(fsms.Length>0){
//			foreach(PlayMakerFSM fsm in fsms){
				fsm.Fsm.Event("type done");
//			}
//		}
	}
	}


