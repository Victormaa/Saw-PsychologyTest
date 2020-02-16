/*Response script that plays a legacy animation*/

using UnityEngine;
using System.Collections;

public class QTE_Response_PlayLegacyAnimation : MonoBehaviour
{
    [System.Serializable]
    public class Stuff
    {
        public GameObject ObjectToAnimate;
        public bool Blend;
        public string ActiveAnimation;
        public string SuccessAnimation1;
        public string SuccessAnimation2;
        public string SuccessAnimation3;
        public string SuccessAnimation4;
        public string FailAnimation;
    }

    public Stuff[] ListOfThingsToAnimate;


    // Use this for initialization
    void Start ()
	{
		

	
	}

    void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        for (int i = 0; i < ListOfThingsToAnimate.Length; i++)
        {
            ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Stop();
        }
    }

        // Update is called once per frame
        void Update ()
	{
		
		if (QTE_main.Singleton.TriggeringObject == this.gameObject) {		
			
			//while the QTE is happening
			if (QTE_main.Singleton.QTEactive) {
                for (int i = 0; i < ListOfThingsToAnimate.Length; i++)
                {
                    if (!string.IsNullOrEmpty(ListOfThingsToAnimate[i].ActiveAnimation))
                    {
                       //if( ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().IsPlaying(ListOfThingsToAnimate[i].ActiveAnimation)
                        if (ListOfThingsToAnimate[i].Blend)
                        {
                            ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Blend(ListOfThingsToAnimate[i].ActiveAnimation);
                        }
                        else
                        {
                            ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Play(ListOfThingsToAnimate[i].ActiveAnimation);
                        }
                    }
                }

			}		

			if (QTE_main.Singleton.QTECompleted && !QTE_main.Singleton.QTEactive) {
			
				//if the QTE completed, and he succedded with option 1
				if (QTE_main.Singleton.succeeded) {
                    for (int i = 0; i < ListOfThingsToAnimate.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(ListOfThingsToAnimate[i].SuccessAnimation1))
                        {
                            ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Stop();
                            if (ListOfThingsToAnimate[i].Blend)
                            {
                                ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Blend(ListOfThingsToAnimate[i].SuccessAnimation1);
                            }
                            else
                            {
                                ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Play(ListOfThingsToAnimate[i].SuccessAnimation1);
                            }
                        }
                    }
				}
			
				//if the QTE completed, and he succedded with option 2 (Dual QTE only)
				if (QTE_main.Singleton.succeeded2) {
                    for (int i = 0; i < ListOfThingsToAnimate.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(ListOfThingsToAnimate[i].SuccessAnimation2))
                        {
                            ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Stop();
                            if (ListOfThingsToAnimate[i].Blend)
                            {
                                ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Blend(ListOfThingsToAnimate[i].SuccessAnimation2);
                            }
                            else
                            {
                                ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Play(ListOfThingsToAnimate[i].SuccessAnimation2);
                            }
                        }
                    }
				}
			
				//if the QTE completed, and he succedded with option 3 (Tri QTE only)
				if (QTE_main.Singleton.succeeded3) {
                    for (int i = 0; i < ListOfThingsToAnimate.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(ListOfThingsToAnimate[i].SuccessAnimation3))
                        {
                            ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Stop();
                            if (ListOfThingsToAnimate[i].Blend)
                            {
                                ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Blend(ListOfThingsToAnimate[i].SuccessAnimation3);
                            }
                            else
                            {
                                ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Play(ListOfThingsToAnimate[i].SuccessAnimation3);
                            }
                        }
                    }
				}
			
				//if the QTE completed, and he succedded with option 4 (Quad QTE only)
				if (QTE_main.Singleton.succeeded4) {
                    for (int i = 0; i < ListOfThingsToAnimate.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(ListOfThingsToAnimate[i].SuccessAnimation4))
                        {
                            ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Stop();
                            if (ListOfThingsToAnimate[i].Blend)
                            {
                                ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Blend(ListOfThingsToAnimate[i].SuccessAnimation4);
                            }
                            else
                            {
                                ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Play(ListOfThingsToAnimate[i].SuccessAnimation4);
                            }
                        }
                    }
				}

                //if the QTE completed, and he failed
                if (QTE_main.Singleton.QTE_Failed_WrongButton || QTE_main.Singleton.QTE_Failed_timer)
                {
                    for (int i = 0; i < ListOfThingsToAnimate.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(ListOfThingsToAnimate[i].FailAnimation))
                        {
                            ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Stop();
                            if (ListOfThingsToAnimate[i].Blend)
                            {
                                ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Blend(ListOfThingsToAnimate[i].FailAnimation);
                            }
                            else
                            {
                                ListOfThingsToAnimate[i].ObjectToAnimate.GetComponent<Animation>().Play(ListOfThingsToAnimate[i].FailAnimation);
                            }
                        }
                    }
                }
			}
		}
	
	}
}
