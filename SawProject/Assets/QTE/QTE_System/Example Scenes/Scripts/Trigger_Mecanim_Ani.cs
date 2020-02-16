using UnityEngine;
using System.Collections;

public class Trigger_Mecanim_Ani : MonoBehaviour {
	
	public Animator ani;

	void OnTriggerEnter (Collider collision) { 
		 if(!collision.CompareTag("Player")){
				 return;
			 }
		
		ani.SetBool("Jump", true);
		ani.SetFloat("Speed", 0.2f);
		
		
	
	}	
	
	void OnTriggerExit(Collider collision) { 
		 if(!collision.CompareTag("Player")){
				 return;
			 }
			ani.SetBool("Jump", false);
			ani.SetFloat("Speed", 0.0f);
	}
}
