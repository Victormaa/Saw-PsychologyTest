/* This is a template response script, for you to write your own responses to QTEs, right now the script does nothing, fill in the sections commneted below with your own code*/

using UnityEngine;
using System.Collections;

public class QTE_Response_Debug : MonoBehaviour
{
	

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (QTE_main.Singleton.TriggeringObject == this.gameObject) {

            if (QTE_main.Singleton.QTEStarted)
            {
                Debug.Log("QTE Started");
            }
			

			if (QTE_main.Singleton.QTEactive) {
				//while the QTE is happening
			}


			if (QTE_main.Singleton.QTECompleted && !QTE_main.Singleton.QTEactive) {
				
				if (QTE_main.Singleton.succeeded) {
                    //if the QTE completed, and he succedded with option 1
                    Debug.Log("Button 1 Success");
				}
			

				if (QTE_main.Singleton.succeeded2) {
                    //if the QTE completed, and he succedded with option 2 (Dual QTE only)
                    Debug.Log("Button 2 Success");
                }
			

				if (QTE_main.Singleton.succeeded3) {
                    //if the QTE completed, and he succedded with option 3 (Tri QTE only)
                    Debug.Log("Button 3 Success");
                }
			

				if (QTE_main.Singleton.succeeded4) {
                    //if the QTE completed, and he succedded with option 4 (Quad QTE only)
                    Debug.Log("Button 4 Success");
                }
			
				if (QTE_main.Singleton.QTE_Failed_WrongButton) {
                    //If failed due to pressing the wrong button
                    Debug.Log("Failed Wrong Button");
                }
				
				if (QTE_main.Singleton.QTE_Failed_timer) {
                    //if failed due to the timer finishing.
                    Debug.Log("Failed Time Out");
                }

			}
		}
	
	}
}
