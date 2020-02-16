using UnityEngine;
using System.Collections;

public class SetMecaimParameter : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
	}
	


	public void SetBoolTrue(string name){
		animator.SetBool(name, true);
	}

	public void SetBoolFalse(string name){
		animator.SetBool(name, false);
	}
}
