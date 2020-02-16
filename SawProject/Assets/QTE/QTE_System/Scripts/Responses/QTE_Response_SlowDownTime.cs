/* A Script that slow downs time while a QTE is happening, and returns to full speed afterward*/

using UnityEngine;
using System.Collections;

public class QTE_Response_SlowDownTime : MonoBehaviour {
	

	public float TimeScale = 0.5f;

	// Use this for initialization
	void Start () {
		

	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(QTE_main.Singleton.TriggeringObject == this.gameObject ){		
			
			//while the QTE is happening
			if(QTE_main.Singleton.QTEactive){
				Time.timeScale = TimeScale;
			}
			else{
				Time.timeScale = 1;
			}
		}
	
	}
}
