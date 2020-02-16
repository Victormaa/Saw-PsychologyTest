using UnityEngine;
using System.Collections;

public class GUI_Text : MonoBehaviour {
	
	public string text;
	
	void OnGUI () {
		GUI.Label(new Rect(10,10,500, 90), text);
	}
}
