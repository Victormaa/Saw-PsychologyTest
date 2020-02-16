/*Changes the camera*/

using UnityEngine;
using System.Collections;

public class QTE_Response_ChangeCamera : MonoBehaviour
{
	


	public bool DuringActive;
	public bool OnCompleted1;
	public bool OnCompleted2;
	public bool OnCompleted3;
	public bool OnCompleted4;
	public bool OnFail;
	public GameObject TargetCamera;
	
	private Camera[] ArrayOfCameras;
	
	// Use this for initialization
	void Start ()
	{

		//ArrayOfCameras = GameObject.FindGameObjectsWithTag("MainCamera");
		ArrayOfCameras = FindObjectsOfType (typeof(Camera)) as Camera[];
	}

	public void ChangeCamera ()
	{
		if (TargetCamera != null) {

			foreach (Camera cam in ArrayOfCameras) {
				
				cam.gameObject.SetActive (false);

			} 
	  
			//set the assoicated Target camera in the inspector to true
			TargetCamera.gameObject.SetActive (true);
		}
	}

	public void TurnOff ()
	{
		TargetCamera.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (QTE_main.Singleton.TriggeringObject == this.gameObject) {	
						
			if (DuringActive) {//while the QTE is happening
				if (QTE_main.Singleton.QTEactive) {
					ChangeCamera ();
				}
			}

			if (QTE_main.Singleton.QTECompleted && !QTE_main.Singleton.QTEactive) {
			
				if (OnCompleted1) {//if the QTE completed, and he succedded with option 1
					if (QTE_main.Singleton.succeeded) {
						ChangeCamera ();
					}
				}
			
				if (OnCompleted2) {	//if the QTE completed, and he succedded with option 2 (Dual QTE only)
					if (QTE_main.Singleton.succeeded2) {
						ChangeCamera ();
					}
				}
			
				if (OnCompleted3) {//if the QTE completed, and he succedded with option 3 (Tri QTE only)
					if (QTE_main.Singleton.succeeded3) {
						ChangeCamera ();
					}
				}
			
				if (OnCompleted4) {//if the QTE completed, and he succedded with option 4 (Quad QTE only)
					if (QTE_main.Singleton.succeeded4) {
						ChangeCamera ();
					}
				}
			
				//if the QTE completed, and he failed

//				Debug.Log (QTE_main.Singleton.QTE_Failed_WrongButton + ", " + QTE_main.Singleton.QTE_Failed_timer);
				if (QTE_main.Singleton.QTE_Failed_WrongButton) {
					if (OnFail) {
						//Debug.Log ("Failed Button Change");
						ChangeCamera ();	
					}
				}

				if (QTE_main.Singleton.QTE_Failed_timer) {
					if (OnFail) {
//						Debug.Log ("Failed Timer Change");
						ChangeCamera ();	
					}
				}
			}
		}
	
	}
}
