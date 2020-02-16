/* This script Lowers the Platform the player steps on and changes it color, used in the Example Scene*/

using UnityEngine;
using System.Collections;

public class QTE_PlatformLower : MonoBehaviour {

	private Vector3 StartPos;
	public float YOffset = 2.0f;
	private Vector3 NewPos;
	public float Speed = 2.0f;

	public Material EnterMat;
	private Material ExitMat;

	private MeshRenderer meshrend;

	private bool MoveUp;

	// Use this for initialization
	void Start () {
		//StartPos = this.transform.parent.transform.position;
		//NewPos = this.transform.parent.transform.position;
		//NewPos.y = NewPos.y - YOffset;

		//meshrend = this.transform.parent.GetComponent<MeshRenderer>();
		//ExitMat = meshrend.material;
	}
	
	// Update is called once per frame
	void Update () {

		if(MoveUp){


			//this.transform.parent.transform.position = Vector3.Lerp(this.transform.parent.transform.position, StartPos, Time.deltaTime * Speed);
			//meshrend.material = ExitMat;
		}
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player"){
			MoveUp= false;
			//this.transform.parent.transform.position = NewPos;
			//meshrend.material = EnterMat;
		}
	}

	void OnTriggerStay(Collider other) {
		//Destroy(other.gameObject);
		if(other.gameObject.tag == "Player"){
			MoveUp= false;
			//this.transform.parent.transform.position = NewPos;
		}
	}

	void OnTriggerExit(Collider other) {
		// Destroy everything that leaves the trigger
		if(other.gameObject.tag == "Player"){
			MoveUp = true;
		}
	}
}
