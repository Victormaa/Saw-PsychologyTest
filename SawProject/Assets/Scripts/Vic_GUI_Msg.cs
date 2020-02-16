using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Vic_GUI_Msg : MonoBehaviour
{
    //private QTE_main MainScript;
    private Image ImageToUse;
    public Sprite SuccessTexture;
    public Sprite FailTexture;
    public float CountDownTimer = 0.5f;
    public bool FadeOut;
    private float triggerTimeout = 0.0f;
    public bool PositionManually;
    public Vector3 NewPosition;

    private bool TimerTrigger;

    // Use this for initialization
    void Start()
    {
        //MainScript = GameObject.Find("QTE_Texture").GetComponent<QTE_main>();	

        if (QTE_main.Singleton.SuccessUI != null)
        {
            ImageToUse = QTE_main.Singleton.SuccessUI.GetComponent<Image>();
        }
        else
        {
            Debug.LogError("QTE_Response_GUIMsg on " + this.gameObject.name + ": There is no SuccessUI ");
        }



        if (SuccessTexture == null)
        {
            Debug.LogError("QTE_Response_GUIMsg on " + this.gameObject.name + ": There is no Success Sprite set in the inspector ");
        }

        if (FailTexture == null)
        {
            Debug.LogError("QTE_Response_GUIMsg on " + this.gameObject.name + ": There is no Fail Sprite set in the inspector ");
        }
        triggerTimeout = CountDownTimer;
    }

    void CreateTexture(float xVal, float yVal)
    {
        //create a new texture
        if (ImageToUse != null)
        {
            ImageToUse.gameObject.SetActive(true);
        }


        //enable the timer, and set its countdown time
        TimerTrigger = true;
        triggerTimeout = CountDownTimer;


    }

    public void Success()
    {
        ShowMsg(SuccessTexture);
    }

    public void Fail()
    {
        ShowMsg(FailTexture);
    }


    private void ShowMsg(Sprite mySprite)
    {
        ImageToUse = QTE_main.Singleton.SuccessUI.GetComponent<Image>();
        if (ImageToUse != null)
        {
            // Debug.LogError("QTE_Response_GUIMsg on " + this.gameObject.name + ": There is no UI Image set in \"ImageToUse\" in the inspector");

            CreateTexture(0.5f, 0.5f);

            //load the texture
            ImageToUse.sprite = mySprite;
            ImageToUse.color = Color.white;

            if (PositionManually)
            {

                //ResultTexture.GetComponent<GUITexture>().pixelInset = new Rect(-SuccessTexture.width/2, -SuccessTexture.height/2, SuccessTexture.width, SuccessTexture.height);	
                ImageToUse.transform.localPosition = NewPosition;
            }
        }
        else
        {
            Debug.LogError("No Success UI found");
        }

    }

    // Update is called once per frame
    void Update()
    {

        //the Timer that destroys the texture when finished.
        if (triggerTimeout > 0 && TimerTrigger)
        {

            triggerTimeout -= Time.deltaTime;

            //when the timer finishes.
            if (triggerTimeout <= 0)
            {

                //Debug.Log("Timer Finished");

                //turn off the timer's countdown
                TimerTrigger = false;

                //reset the timer
                triggerTimeout = 0;

                //Turn off the QTE so the checks below stop constantly setting the timer to be on.				
                //QTE_main.Singleton.QTECompleted = false;

                ImageToUse.gameObject.SetActive(false);
                //Destroy(ResultTexture);	
            }
        }

        //only run if the triggering Gameobject is the one this script is also attached to.
        if (QTE_main.Singleton.TriggeringObject == this.gameObject && QTE_main.Singleton.QTECompleted)
        {
            //

            /*if(QTE_main.Singleton.QTECompleted){
				Debug.Log("Timer " + QTE_main.Singleton.QTE_Failed_timer + ", WrongBUtton " + QTE_main.Singleton.QTE_Failed_WrongButton);
			}*/

            //if the QTE completed, and he succedded with option 1
            if (QTE_main.Singleton.succeeded)
            {

                ShowMsg(SuccessTexture);
            }

            //if the QTE completed, and he succedded with option 2
            if (QTE_main.Singleton.succeeded2)
            {

                ShowMsg(SuccessTexture);

            }

            //if the QTE completed, and he succedded with option 3
            if (QTE_main.Singleton.succeeded3)
            {

                ShowMsg(SuccessTexture);

            }

            //if the QTE completed, and he succedded with option 4
            if (QTE_main.Singleton.succeeded4)
            {

                ShowMsg(SuccessTexture);

            }
            //if the QTE completed, and he failed
            if (QTE_main.Singleton.QTE_Failed_WrongButton || QTE_main.Singleton.QTE_Failed_timer)
            {

                ShowMsg(FailTexture);
            }
        }

        //if the fadeout option is checked, and the Timer is running, fade out the texture to zero.
        if (FadeOut && TimerTrigger)
        {
            if (ImageToUse != null && ImageToUse.GetComponent<Image>().color.a >= 0)
            {

                Color col = ImageToUse.GetComponent<Image>().color;
                col.a -= Time.deltaTime / CountDownTimer;
                ImageToUse.GetComponent<Image>().color = col;
            }
        }

    }
}
