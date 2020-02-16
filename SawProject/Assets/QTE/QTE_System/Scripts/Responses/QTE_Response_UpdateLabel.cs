/*Response script that updates a GUI label*/

using UnityEngine;
using System.Collections;

public class QTE_Response_UpdateLabel : MonoBehaviour
{
	

	private GUI_Text LabelScript;
	public GameObject GameobjectWithAttachedScript;
	public string SuccessText1 = "";
	public string SuccessText2 = "";
	public string SuccessText3 = "";
	public string SuccessText4 = "";
	public string FailText = "";
	public bool Reset;
	public string ResetText;
	public float ResetTimer = 2.0f;
	private float triggerTimeout;

	// Use this for initialization
	void Start ()
	{
		

		LabelScript = GameobjectWithAttachedScript.GetComponent<GUI_Text> ();
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (Reset) {
			if (triggerTimeout > 0) {
			
				triggerTimeout -= Time.deltaTime;
			
				//when the timer finishes.
				if (triggerTimeout <= 0) {
			
					triggerTimeout = 0f;
					
					if (string.IsNullOrEmpty (ResetText)) {
						LabelScript.text = "";
					} else {
						LabelScript.text = ResetText;
					}
				}
			}
		}
		
		if (QTE_main.Singleton.TriggeringObject == this.gameObject) {		

			if (QTE_main.Singleton.QTECompleted && !QTE_main.Singleton.QTEactive) {
			
				//if the QTE completed, and he succedded with option 1
				if (QTE_main.Singleton.succeeded) {
					LabelScript.text = SuccessText1;
					if (Reset) {
						triggerTimeout = ResetTimer;
					}
				
				}
			
				//if the QTE completed, and he succedded with option 2 (Dual QTE only)
				if (QTE_main.Singleton.succeeded2) {
					LabelScript.text = SuccessText2;
					if (Reset) {
						triggerTimeout = ResetTimer;
					}
				}
			
				//if the QTE completed, and he succedded with option 3 (Tri QTE only)
				if (QTE_main.Singleton.succeeded3) {
					LabelScript.text = SuccessText3;
					if (Reset) {
						triggerTimeout = ResetTimer;
					}
				}
			
				//if the QTE completed, and he succedded with option 4 (Quad QTE only)
				if (QTE_main.Singleton.succeeded4) {
					LabelScript.text = SuccessText4;
					if (Reset) {
						triggerTimeout = ResetTimer;
					}
				}
			
				//if the QTE completed, and he failed
				if (QTE_main.Singleton.QTE_Failed_WrongButton || QTE_main.Singleton.QTE_Failed_timer) {
					LabelScript.text = FailText;
					if (Reset) {
						triggerTimeout = ResetTimer;
					}
				}
			}
		}
	
	}
}
