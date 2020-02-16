using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE_ArrowRadialWipe : MonoBehaviour {

    private Material CircleMaterial;
    private Material NewMat;

    public float Cutoff = 1.0f;


    // Use this for initialization
    void Start () {

        CircleMaterial = this.GetComponent<Image>().material;
        NewMat = new Material(CircleMaterial);
        this.GetComponent<Image>().material = NewMat;

    }
	
	// Update is called once per frame
	void Update () {
        NewMat.SetFloat("_Angle", Mathf.Lerp(-3.14f, 3.14f, Cutoff));
    }
}
