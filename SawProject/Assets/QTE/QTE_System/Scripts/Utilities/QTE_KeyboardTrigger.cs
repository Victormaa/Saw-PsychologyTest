/* This example script fires a QTE when a keyboard button is pressed*/

using UnityEngine;
using System.Collections;

public class QTE_KeyboardTrigger : MonoBehaviour {

	public string KeyName = "Space";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyName)){
			this.GetComponent<QTE_Trigger>().TriggerQTE();
		}
	}
}
