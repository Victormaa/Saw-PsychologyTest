/* SCript that attaches to the expanding Circle image of the Mashing UI visalization  */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QTE_MashCircleUI : MonoBehaviour {

	//[SerializeField]
	public Color start = Color.red;
	//[SerializeField]
	public Color end = Color.green;

	//[SerializeField]
	private Material CircleMaterial;
	private Material NewMat;

	public bool StartAtZeroScale;

	private float percentage;

	Vector3 storedScale;
    Vector3 NewScale;

	public float MaxSize = 1.4f;
	Vector3 myScale;

	public bool Mashing = true;
    

	// Use this for initialization
	void Start () {

		CircleMaterial = this.GetComponent<Image>().material;
		NewMat = new Material(CircleMaterial);
		this.GetComponent<Image>().material = NewMat;
        //NewMat = this.GetComponent<Image>().material;


       storedScale = this.transform.localScale;

        //this.gameObject.SetActive(false);


    }
	
	// Update is called once per frame
	void Update () {

		if(QTE_main.Singleton.QTEactive){


            if (Mashing)
            {
                percentage = QTE_main.Singleton.MashValue / 1.0f;
            }

            if (percentage > 1.0f){
				percentage = 1.0f;
			}
            if (StartAtZeroScale)
            {
                myScale = Vector3.Lerp(Vector3.zero, new Vector3(MaxSize, MaxSize, MaxSize), percentage);
            }
            else
            {
                myScale = Vector3.Lerp(storedScale, new Vector3(MaxSize, MaxSize, MaxSize), percentage);
            }
			this.transform.localScale = myScale;


			NewMat.color = Color.Lerp(start, end, percentage);
		}else{
			percentage = 0;
			this.transform.localScale = storedScale;
		}
	
	}
}
