using UnityEngine;
using System.Collections;

public class Trigger_Animation : MonoBehaviour {
	
	public GameObject ThingToAnimate;

	private Animator ani;

	// Use this for initialization
	void Start () {
		ani = ThingToAnimate.GetComponent<Animator>();
	}
	
	void OnTriggerEnter (Collider collision) { 		
		 if(!collision.CompareTag("Player")){
				 return;
			 }

		Debug.Log("Play Animation");
		ani.SetTrigger("Play");
	}
}
