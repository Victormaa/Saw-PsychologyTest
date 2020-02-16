/*This script creates a combo of mutliple QTEs, all QTEs specificed must be succeeded to pass*/

using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class QTE_Response_Combo : MonoBehaviour
{
	

	private bool Failed;
	private int iterator;
	private bool BeingCombo;

    [HideInInspector]
	public List<GameObject> Triggers;

    public UnityEvent OnSuccess = new UnityEvent ();
	public UnityEvent OnFail = new UnityEvent ();

	// Use this for initialization
	void Start ()
	{
        Triggers.Clear();
        foreach (Transform child in transform)
        {
            Triggers.Add(child.gameObject);//child is your child transform
        }
       // Debug.Log(Triggers.Count);
    }

	void OnTriggerEnter (Collider collision)
	{ 
		if (collision.tag == "Player") {
			StartCombo ();
		}
	}

	public void StartCombo ()
	{
		//Debug.Log (iterator);
		Triggers [0].GetComponent<QTE_Trigger> ().TriggerQTE ();
		iterator = 0;
		BeingCombo = true;
		Failed = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (BeingCombo) {
			if (!QTE_main.Singleton.QTEactive) {

				//when the Combo Completes
				if (iterator == Triggers.Count - 1) {
					if (Failed || QTE_main.Singleton.QTE_Failed_WrongButton) {
						OnComboFailed ();
					} else {
						OnComboPassed ();
					}
					//BeingCombo = false;
				} else {


					if (QTE_main.Singleton.QTECompleted) {	
						if (QTE_main.Singleton.QTE_Failed_WrongButton || QTE_main.Singleton.QTE_Failed_timer) {	
							Failed = true;
							OnComboFailed ();
						}else{
							iterator++;
							//Debug.Log (iterator);
							Triggers [iterator].GetComponent<QTE_Trigger> ().TriggerQTE ();
						}
					}
				}
			}
		}
	}

	void OnComboPassed ()
	{
		Debug.Log ("Passed Combo");
		BeingCombo = false;
		iterator = 0;
		Failed = false;
		OnSuccess.Invoke ();
	}

	void OnComboFailed ()
	{
		Debug.Log ("Failed Combo");
		BeingCombo = false;
		iterator = 0;
		Failed = false;
		OnFail.Invoke ();
	}
}
