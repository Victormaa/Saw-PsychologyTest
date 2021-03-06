/* Script that attaches to the UI timer images, animates the wiping effect and changes it color*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QTE_CircleTimerUI : MonoBehaviour 
{
    [SerializeField]
	Color start = Color.red;
    [SerializeField]
	Color end = Color.green;

    //[SerializeField]
    private Material CircleMaterial;
	private Material NewMat;

    [Range(0, 1)]
    public float TimerPercentage;

	//public float Cutoff = 0.05f;

	void Start(){
		//CircleMaterial = ;

		CircleMaterial = this.GetComponent<Image>().material;
		NewMat = new Material(CircleMaterial);
		this.GetComponent<Image>().material = NewMat;

		this.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () 
	{
		NewMat.SetFloat("_Angle", Mathf.Lerp(-3.14f, 3.14f, TimerPercentage));
		NewMat.SetColor("_Color", Color.Lerp(start, end, TimerPercentage));
	}
}
