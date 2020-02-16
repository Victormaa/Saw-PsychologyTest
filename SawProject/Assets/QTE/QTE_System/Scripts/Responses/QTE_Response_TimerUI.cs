/* QTE response that Visualizes the Time the user has to complete a QTE by showing a wiping cicle behind the UI buttons*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class QTE_Response_TimerUI : MonoBehaviour {

    Color[] StoredColors = new Color[4];



    public Vector3 PositionOffset;
    

    // Use this for initialization
    void Start () {


	}

	void TurnOffTimerUI(){
		if(QTE_main.Singleton.Button1CircleUI != null){
			QTE_main.Singleton.Button1CircleUI.gameObject.SetActive(false);
		}
		if(QTE_main.Singleton.Button2CircleUI != null){
			QTE_main.Singleton.Button2CircleUI.gameObject.SetActive(false);
		}
		if(QTE_main.Singleton.Button3CircleUI != null){
		QTE_main.Singleton.Button3CircleUI.gameObject.SetActive(false);
		}
		if(QTE_main.Singleton.Button4CircleUI != null){
		QTE_main.Singleton.Button4CircleUI.gameObject.SetActive(false);
		}



    }

	// Update is called once per frame
	void Update () {

        if (QTE_main.Singleton.TriggeringObject == this.gameObject) {



			//while the QTE is happening
			if(QTE_main.Singleton.QTEactive){
				if(!QTE_main.Singleton.Hidden){
					if(QTE_main.Singleton.Button1CircleUI != null){
						QTE_main.Singleton.Button1CircleUI.gameObject.SetActive(true);
						QTE_main.Singleton.Button1CircleUI.transform.position = QTE_main.Singleton.UIButtons[0].transform.position + PositionOffset;
						QTE_main.Singleton.Button1CircleUI.transform.rotation = QTE_main.Singleton.UIButtons[0].transform.rotation;
						QTE_main.Singleton.Button1CircleUI.TimerPercentage = QTE_main.Singleton.GetTimerPercentage(1);
                        StoredColors[0] = QTE_main.Singleton.Button1TimerUI.GetComponent<Image>().color;
                        StoredColors[0].a = QTE_main.Singleton.UIButtons[0].color.a;
                        QTE_main.Singleton.Button1TimerUI.GetComponent<Image>().color = StoredColors[0];

                    }
                    else{
						Debug.LogError("UI Timer for Button 1 Not Found!, Please place the QTE_TimerUI Prefab in your Canvas, above the QTE_UI prefab");
					}

					if(QTE_main.Singleton.UIButtons[1].gameObject.activeSelf){
						if(QTE_main.Singleton.Button2CircleUI != null){
							//Debug.Log("Timer 2 ON");
							QTE_main.Singleton.Button2CircleUI.gameObject.SetActive(true);
                            

                            QTE_main.Singleton.Button2CircleUI.transform.position = QTE_main.Singleton.UIButtons[1].transform.position + PositionOffset;
							QTE_main.Singleton.Button2CircleUI.transform.rotation = QTE_main.Singleton.UIButtons[1].transform.rotation;
							QTE_main.Singleton.Button2CircleUI.TimerPercentage = QTE_main.Singleton.GetTimerPercentage(2);
                            //							Debug.Log(QTE_main.Singleton.GetTimerPercentage2());

                            StoredColors[1] = QTE_main.Singleton.Button2TimerUI.GetComponent<Image>().color;
                            StoredColors[1].a = QTE_main.Singleton.UIButtons[1].color.a;
                            QTE_main.Singleton.Button2TimerUI.GetComponent<Image>().color = StoredColors[1];
                        }
                        else{
							Debug.LogError("UI Timer for Button 2 Not Found!, Please place the QTE_TimerUI Prefab in your Canvas, above the QTE_UI prefab");
						}
					}

					if(QTE_main.Singleton.UIButtons[2].gameObject.activeSelf){
						if(QTE_main.Singleton.Button3CircleUI != null){
							QTE_main.Singleton.Button3CircleUI.gameObject.SetActive(true);
							QTE_main.Singleton.Button3CircleUI.transform.position = QTE_main.Singleton.UIButtons[2].transform.position + PositionOffset;
							QTE_main.Singleton.Button3CircleUI.transform.rotation = QTE_main.Singleton.UIButtons[2].transform.rotation;
							QTE_main.Singleton.Button3CircleUI.TimerPercentage = QTE_main.Singleton.GetTimerPercentage(3);

                            StoredColors[2] = QTE_main.Singleton.Button3TimerUI.GetComponent<Image>().color;
                            StoredColors[2].a = QTE_main.Singleton.UIButtons[2].color.a;
                            QTE_main.Singleton.Button3TimerUI.GetComponent<Image>().color = StoredColors[2];
                            //Debug.Log(QTE_main.Singleton.GetTimerPercentage());
                        }
                        else{
							Debug.LogError("UI Timer for Button 3 Not Found!, Please place the QTE_TimerUI Prefab in your Canvas, above the QTE_UI prefab");
						}
					}

					if(QTE_main.Singleton.UIButtons[3].gameObject.activeSelf){
						if(QTE_main.Singleton.Button4CircleUI != null){
							QTE_main.Singleton.Button4CircleUI.gameObject.SetActive(true);
							QTE_main.Singleton.Button4CircleUI.transform.position = QTE_main.Singleton.UIButtons[3].transform.position + PositionOffset;
							QTE_main.Singleton.Button4CircleUI.transform.rotation = QTE_main.Singleton.UIButtons[3].transform.rotation;
							QTE_main.Singleton.Button4CircleUI.TimerPercentage = QTE_main.Singleton.GetTimerPercentage(4);
                            //Debug.Log(QTE_main.Singleton.GetTimerPercentage());

                            StoredColors[3] = QTE_main.Singleton.Button4TimerUI.GetComponent<Image>().color;
                            StoredColors[3].a = QTE_main.Singleton.UIButtons[3].color.a;
                            QTE_main.Singleton.Button4TimerUI.GetComponent<Image>().color = StoredColors[3];
                        }
                        else{
							Debug.LogError("UI Timer for Button 4 Not Found!, Please place the QTE_TimerUI Prefab in your Canvas, above the QTE_UI prefab");
						}
					}
				}
			}
			//if the QTE completed, and he succedded with option 1
			if(QTE_main.Singleton.succeeded && QTE_main.Singleton.QTECompleted && !QTE_main.Singleton.QTEactive){
				TurnOffTimerUI();
			}

			//if the QTE completed, and he succedded with option 2 (Dual QTE only)
			if(QTE_main.Singleton.succeeded2 && QTE_main.Singleton.QTECompleted && !QTE_main.Singleton.QTEactive){
				TurnOffTimerUI();
			}

			//if the QTE completed, and he succedded with option 3 (Tri QTE only)
			if(QTE_main.Singleton.succeeded3 && QTE_main.Singleton.QTECompleted && !QTE_main.Singleton.QTEactive){
				TurnOffTimerUI();
			}

			//if the QTE completed, and he succedded with option 4 (Quad QTE only)
			if(QTE_main.Singleton.succeeded4 && QTE_main.Singleton.QTECompleted && !QTE_main.Singleton.QTEactive){
				TurnOffTimerUI();
			}

			//if the QTE completed, and he failed
			if(!QTE_main.Singleton.succeeded && !QTE_main.Singleton.succeeded2 && !QTE_main.Singleton.succeeded3 && !QTE_main.Singleton.succeeded4 && QTE_main.Singleton.QTECompleted  && !QTE_main.Singleton.QTEactive){

				TurnOffTimerUI();
			}
		}

	}
}
