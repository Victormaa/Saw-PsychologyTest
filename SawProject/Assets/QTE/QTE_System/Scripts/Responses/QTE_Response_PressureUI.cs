/* Respone Script that displays a Visulazation of the Mashing struggle for mashable QTE's*/

using UnityEngine;
using System.Collections;

public class QTE_Response_PressureUI : MonoBehaviour
{
	
	public bool MoveToButton1Position;


	public Color start = Color.red;
	public Color end = Color.green;
	//public bool StartAtZeroScale;
    public float MaxSize = 1.4f;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (QTE_main.Singleton.TriggeringObject == this.gameObject) {

            // MaxSize = QTE_main.Singleton.PressureThreshold[0];


            QTE_main.Singleton.MashCircle.start = start;
			QTE_main.Singleton.MashCircle.end = end;
			QTE_main.Singleton.MashCircle.StartAtZeroScale = false;
			QTE_main.Singleton.MashCircle.MaxSize = MaxSize;
			QTE_main.Singleton.MashCircle.Mashing = false;
            //QTE_main.Singleton.MashRing.transform.localScale = new Vector3 (MaxSize, MaxSize, MaxSize);



            if (QTE_main.Singleton.QTEactive) {
				//while the QTE is happening

				if(QTE_main.Singleton.MashUI !=null){
					QTE_main.Singleton.MashUI.SetActive(true);
					if(MoveToButton1Position){
						QTE_main.Singleton.MashParent.transform.position = QTE_main.Singleton.UIButtons[0].gameObject.transform.position;
						QTE_main.Singleton.MashParent.transform.position = QTE_main.Singleton.UIButtons[0].gameObject.transform.position;
					}
				}
				else{
					Debug.LogError("Mashing UI gameobject 1 Not Found!, Please place the QTE_MashUI Prefab in your Canvas, above the QTE_UI prefab");
				}
			}

			if (QTE_main.Singleton.QTECompleted && !QTE_main.Singleton.QTEactive) {

				QTE_main.Singleton.MashUI.SetActive(false);

			}


		}
	
	}
}
