using UnityEngine;
using System.Collections;

public class QTE_TurnOffCompleted : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(QTE_main.Singleton.QTECompleted){
			QTE_main.Singleton.QTECompleted = false;
		}
	}
}
