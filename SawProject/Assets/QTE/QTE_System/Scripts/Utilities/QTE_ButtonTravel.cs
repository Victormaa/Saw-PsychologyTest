/* Script that Attaches to World Space Canvas', and allows UI buttons to move in the world along with speficied Gameobjects*/

using UnityEngine;
using System.Collections;

public class QTE_ButtonTravel : MonoBehaviour {
	

	public GameObject ButtonLocation1;
	public GameObject ButtonLocation2;	
	public GameObject ButtonLocation3;
	public GameObject ButtonLocation4;
	//public Vector3 Scale = new Vector3(1,1,1);
	//public bool LookAtCamera;
//	public bool ParentToObject;
	public bool TravelWithParent;
	public Vector3 TravelOffset= new Vector3(0,1,0);
	public Vector3 Rotation;

	private GameObject Button1;
	private GameObject Button2;
	private GameObject Button3;
	private GameObject Button4;


	void Start(){
		Button1 = transform.Find("QTE_UI/Button1").gameObject;
		Button2 = transform.Find("QTE_UI/Button2").gameObject;
		Button3 = transform.Find("QTE_UI/Button3").gameObject;
		Button4 = transform.Find("QTE_UI/Button4").gameObject;

		Button1.transform.position = ButtonLocation1.transform.position + TravelOffset;
		Button2.transform.position = ButtonLocation2.transform.position + TravelOffset;
		Button3.transform.position = ButtonLocation3.transform.position + TravelOffset;
		Button4.transform.position = ButtonLocation4.transform.position + TravelOffset;
	}

	void Update(){
		if(QTE_main.Singleton.QTEactive){
			if(TravelWithParent){
				Button1.transform.position = ButtonLocation1.transform.position + TravelOffset;
				Button2.transform.position = ButtonLocation2.transform.position + TravelOffset;
				Button3.transform.position = ButtonLocation3.transform.position + TravelOffset;
				Button4.transform.position = ButtonLocation4.transform.position + TravelOffset;

				Button1.transform.localRotation = Quaternion.Euler(Rotation);
				Button2.transform.localRotation = Quaternion.Euler(Rotation);
				Button3.transform.localRotation = Quaternion.Euler(Rotation);
				Button4.transform.localRotation = Quaternion.Euler(Rotation);
			}

			/*if(ParentToObject){
				Button1.transform.SetParent(ButtonLocation1.transform);
				Button2.transform.SetParent(ButtonLocation2.transform);
				Button3.transform.SetParent(ButtonLocation3.transform);
				Button4.transform.SetParent(ButtonLocation4.transform);

				Button1.transform.localPosition = TravelOffset;
				Button2.transform.localPosition = TravelOffset;
				Button3.transform.localPosition = TravelOffset;
				Button4.transform.localPosition = TravelOffset;

			}*/
		}
	}
		
}
