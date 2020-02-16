/* This script looks for an animation curve from a Mecanim character, and triggers a QTE based upon when the curve reaches the Trigger Value specified*/

using UnityEngine;
using System.Collections;

public class QTE_AnimationCurveTrigger : MonoBehaviour {

    private Animator animator;
	public string ParameterName;
	public float TriggerValue = 1.0f;
	private float AnimatorValue;
	


	private QTE_Trigger QTE;

	
	// Use this for initialization
	void Awake () {
        animator = GetComponent<Animator>();
        QTE = GetComponent<QTE_Trigger>();

        if(animator == null)
        {
            Debug.LogError("No Animator found on:" + gameObject.name);
        }

	}
	
	// Update is called once per frame
	void Update () {
		if(!QTE_main.Singleton.QTEactive){
			AnimatorValue = animator.GetFloat(ParameterName);
			
			if(AnimatorValue >= TriggerValue){
				if(QTE != null){
					QTE.TriggerQTE();
				}
			}
		}
		
	
	}
}
