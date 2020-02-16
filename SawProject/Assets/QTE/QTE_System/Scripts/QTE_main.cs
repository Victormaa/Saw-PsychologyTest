/* Main logic for the Quick Time Event system */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public class QTE_main : QTE_BaseClass
{

    #region Properties
    public static QTE_main Singleton;

    //This is the canvas referance this script uses, gets re-assigned depending on if the default canvas is used, or is overridden.
    [HideInInspector]
    public GameObject MainCanvas;

    //Referance to the default fallback canvas to use
    public GameObject DefaultCanvas;

    //the other override canvas
    [HideInInspector]
    public GameObject OtherCanvas;

    [HideInInspector]
    public Image[] UIButtons = new Image[4];

    //The Vector3 Positions to use if OverideButtonPosition is true
    [HideInInspector]
    public bool TravelWithParent;
    [HideInInspector]
    public Vector3 TravelOffset;

    //The timer's time
    [HideInInspector]
    private float triggerTimeout = 0f;

    [HideInInspector]
    public bool MultiTimerDual;
    [HideInInspector]
    public bool MultiTimerTri;
    [HideInInspector]
    public bool MultiTimerQuad;

    //the Delay timer
    [HideInInspector]
    public float DelayTime;
    private bool DelayToggle;
    private float DelayTimeout = 0.0f;

    [HideInInspector]
    public float TimerDelayTime;
    private float TimerDelayTimeout;

    private bool TimerDelayToggle = false;

    //weither or not the player pressed any keys during the QTE
    public bool KeyGotPressed;

    //if the QTE has completed or not (useful in Response Scripts)
    [HideInInspector]
    public bool QTECompleted;

    //is true while the QTE is currently active
    [HideInInspector]
    public bool QTEactive;

    //stored gameobject that is triggering the QTE, to make sure that only the Response script(s) attached to it fires and not all of them.
    [HideInInspector]
    public GameObject TriggeringObject;

    //whether the player succeeded in passing the QTE
    [HideInInspector]
    public bool succeeded = false;
    [HideInInspector]
    public bool succeeded2 = false;
    [HideInInspector]
    public bool succeeded3 = false;
    [HideInInspector]
    public bool succeeded4 = false;
    [HideInInspector]
    public bool WrongButtonFail;

    //actually causes the QTE to happen
    [HideInInspector]
    public bool Triggered;

    //used only in the mashable QTE
    [HideInInspector]
    public bool Mashable;
    [HideInInspector]
    public float MashValue = 0.0f;


    //checks to see if the user wants to use Inputs, or straight up Keyboard keys
    //the string of the actual input for
    [HideInInspector]
    public string KeyPress;
    //To shake, or not to shake...
    //how much to offset the shake

    [HideInInspector]
    public bool RandomButton;

    //used only in the Dual QTE
    [HideInInspector]
    public bool DualTrigger;
    [HideInInspector]
    public string KeyPress2;

    [HideInInspector]
    public bool RandomButton2;

    //used only in the Tri QTE
    [HideInInspector]
    public bool TriTrigger;
    [HideInInspector]
    public string KeyPress3;

    [HideInInspector]
    public bool RandomButton3;

    //used only in the Quad QTE
    [HideInInspector]
    public bool QuadTrigger;
    [HideInInspector]
    public string KeyPress4;

    [HideInInspector]
    public bool RandomButton4;

    //For Mashable QTE's, Need to store the scale of the button so I can apply the pulsating offset to it... without it constantly growing in size
    private Vector3 StoredScale;

    //Gameobject Referances to the UI Timer Visulzations

    [HideInInspector]
    public GameObject Button1TimerUI, Button2TimerUI, Button3TimerUI, Button4TimerUI;

    [HideInInspector]
    public QTE_CircleTimerUI Button1CircleUI, Button2CircleUI, Button3CircleUI, Button4CircleUI;

    //These float vaules count down while the QTE timer is active, they are used to calculate the percentage of time left for the UI Timers
    [HideInInspector]
    public float MainTimerCount1, MainTimerCount2, MainTimerCount3, MainTimerCount4;

    [HideInInspector]
    public bool QTE_Failed_WrongButton;
    [HideInInspector]
    public bool QTE_Failed_timer;

    private bool QTECompletedFlip;

    [HideInInspector]
    public GameObject MashUI;
    [HideInInspector]
    public GameObject MashParent;
    [HideInInspector]
    public QTE_MashCircleUI MashCircle;
    [HideInInspector]
    public GameObject MashRing;
    [HideInInspector]
    public GameObject SuccessUI;

    float WidthMin;
    float WidthMax;
    float HeightMin;
    float HeightMax;

    [HideInInspector]
    public bool QTEStarted;

    [HideInInspector]
    public Color[] StoredColors = new Color[4];

    float FadeInTimer;

    //public Text DebugText;

    public QTE_Images QTEImagesAsset;

    #endregion

    void Start()
    {
        if (DefaultCanvas != null)
        {
            MainCanvas = DefaultCanvas;
            SetupCanvas(MainCanvas);
        }
        else
        {
            Debug.LogError("QTE_main: There is No Default Canvas Specified in QTE_main.cs, please assign one in the Inspector");
        }
    }

    public void OnEnable()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    private void SetupCanvas(GameObject myCanvas)
    {
        //This function grabs the referances to the UI buttons and UI Timers from the provided Canvas to use

        if (myCanvas.transform.Find("QTE_UI/SuccessMessage") != null)
        {
            SuccessUI = myCanvas.transform.Find("QTE_UI/SuccessMessage").gameObject;
            SuccessUI.SetActive(false);
            Debug.Log(myCanvas.name);
        }
        else
        {
            //  Debug.LogError("QTE_main: Sucess Message Prefab Not Found!, Please Place the QTE_UI Prefab in your Canvas");
        }

        if (myCanvas.transform.Find("QTE_UI/Button1"))
        {
            UIButtons[0] = myCanvas.transform.Find("QTE_UI/Button1").GetComponent<Image>();
            UIButtons[0].gameObject.SetActive(false);
            if (myCanvas.transform.Find("QTE_TimerUI/QTE_Timer1") != null)
            {
                Button1TimerUI = myCanvas.transform.Find("QTE_TimerUI/QTE_Timer1").gameObject;
                Button1TimerUI.SetActive(false);
                Button1CircleUI = Button1TimerUI.GetComponent<QTE_CircleTimerUI>();
            }
        }
        else
        {
            Debug.LogError("QTE_main: Button1 Prefab Not Found!, Please Place the QTE_UI Prefab in your Canvas");
            //break;
        }
        if (myCanvas.transform.Find("QTE_UI/Button2"))
        {
            UIButtons[1] = myCanvas.transform.Find("QTE_UI/Button2").GetComponent<Image>();
            UIButtons[1].gameObject.SetActive(false);
            if (myCanvas.transform.Find("QTE_TimerUI/QTE_Timer2") != null)
            {
                Button2TimerUI = myCanvas.transform.Find("QTE_TimerUI/QTE_Timer2").gameObject;
                Button2TimerUI.SetActive(false);
                Button2CircleUI = Button2TimerUI.GetComponent<QTE_CircleTimerUI>();
            }
        }
        else
        {
            Debug.LogError("QTE_main: Button2 Prefab Not Found!, Please Place the QTE_UI Prefab in your Canvas");
            //break;
        }
        if (myCanvas.transform.Find("QTE_UI/Button3"))
        {
            UIButtons[2] = myCanvas.transform.Find("QTE_UI/Button3").GetComponent<Image>();
            UIButtons[2].gameObject.SetActive(false);
            if (myCanvas.transform.Find("QTE_TimerUI/QTE_Timer3") != null)
            {
                Button3TimerUI = myCanvas.transform.Find("QTE_TimerUI/QTE_Timer3").gameObject;
                Button3TimerUI.SetActive(false);
                Button3CircleUI = Button3TimerUI.GetComponent<QTE_CircleTimerUI>();
            }
        }
        else
        {
            Debug.LogError("QTE_main: Button3 Prefab Not Found!, Please Place the QTE_UI Prefab in your Canvas");
            //break;
        }
        if (myCanvas.transform.Find("QTE_UI/Button4"))
        {
            UIButtons[3] = myCanvas.transform.Find("QTE_UI/Button4").GetComponent<Image>();
            UIButtons[3].gameObject.SetActive(false);
            if (myCanvas.transform.Find("QTE_TimerUI/QTE_Timer4") != null)
            {
                Button4TimerUI = myCanvas.transform.Find("QTE_TimerUI/QTE_Timer4").gameObject;
                Button4TimerUI.SetActive(false);
                Button4CircleUI = Button4TimerUI.GetComponent<QTE_CircleTimerUI>();
            }
        }
        else
        {
            Debug.LogError("QTE_main: Button4 Prefab Not Found!, Please Place the QTE_UI Prefab in your Canvas");
            //break;
        }

        if (myCanvas.transform.Find("QTE_MashUI") != null)
        {
            MashUI = myCanvas.transform.Find("QTE_MashUI").gameObject;
            MashParent = MashUI.transform.Find("QTE_MashParent").gameObject;
            MashCircle = MashParent.transform.Find("QTE_MashCircle").GetComponent<QTE_MashCircleUI>();
            MashRing = MashParent.transform.Find("QTE_MashRing").gameObject;
            MashUI.SetActive(false);
        }


    }

    //This function return the Timer Percentages normalized (0-1) for each button timer, used to Visalize the Timer onscreen.
    public float GetTimerPercentage(int index)
    {
        float myReturn;

        if (index == 1)
        {
            myReturn = MainTimerCount1 / triggerTimeout;
        }
        else if (index == 2)
        {
            if (MultiTimerDual || MultiTimerTri || MultiTimerQuad)
            {
                myReturn = MainTimerCount2 / CountDownTimes[1];
            }
            else
            {
                myReturn = MainTimerCount1 / triggerTimeout;
            }
        }
        else if (index == 3)
        {
            if (MultiTimerTri || MultiTimerQuad)
            {
                myReturn = MainTimerCount3 / CountDownTimes[2];
            }
            else
            {
                myReturn = MainTimerCount1 / triggerTimeout;
            }
        }
        else
        {
            if (MultiTimerQuad)
            {
                myReturn = MainTimerCount4 / CountDownTimes[3];
            }
            else
            {
                myReturn = MainTimerCount1 / triggerTimeout;
            }
        }
        return myReturn;
    }


    //Triggers the QTE to happen
    public void TriggerQTE(float time, float delay, float TimerDelay)
    {

        #region Initial Setup
        QTEStarted = true;

        MainTimerCount1 = 0;
        //set the CountDown timer to the supplied time
        triggerTimeout = time;
        //set the Input Delay Timer to the supplied Delay time			
        DelayTimeout = delay;
        //set the delay Timerout to the supplied Delay Time
        TimerDelayTimeout = TimerDelay;

        QTE_Failed_WrongButton = false;
        QTE_Failed_timer = false;

        //find out which canvas to use, and set it up
        if (OtherCanvas != null)
        {
            MainCanvas = OtherCanvas;
            SetupCanvas(MainCanvas);
        }
        else
        {
            MainCanvas = DefaultCanvas;
            SetupCanvas(MainCanvas);
        }

        if (TimerDelay <= 0)
        {
            //if there is no delay to the QTE, trigger it right away
            SetupQTE();
        }
        else
        {
            /*if there is a delay, Create the additional textures/objects for Multi-button QTE's (needed or else the trigger script throws Null errors because it's looking for them), but disable them,
				then flip TimerDelayToggle to true, so that later on Update() will trigger the QTE when timer finishes.*/
            EnableMultiButtons();
            ToggleMultiButtonVisibility(false);
            TimerDelayToggle = true;
            //Debug.Log("Timer Greater than 0");
        }

        //If mashable, then store the intial size of the button, used for the pulsating fucntionality
        if (Mashable)
        {
            StoredScale = UIButtons[0].transform.localScale;
        }
        #endregion

        //if the button postions are overitten, move them to the new location
        if (!OverideButtonPosition)
        {
            ButtonPositions[0] = UIButtons[0].transform.localPosition;
            ButtonPositions[1] = UIButtons[1].transform.localPosition;
            ButtonPositions[2] = UIButtons[2].transform.localPosition;
            ButtonPositions[3] = UIButtons[3].transform.localPosition;
        }


        //If the fade in open is on, Store the intial color of the button, then set them to invisble.
        if (FadeInUI)
        {
            FadeInTimer = FadeInTime;
            StoredColors[0] = UIButtons[0].color;
            StoredColors[0].a = 0;
            UIButtons[0].color = StoredColors[0];

            StoredColors[1] = UIButtons[1].color;
            StoredColors[1].a = 0;
            UIButtons[1].color = StoredColors[1];

            StoredColors[2] = UIButtons[2].color;
            StoredColors[2].a = 0;
            UIButtons[2].color = StoredColors[2];

            StoredColors[3] = UIButtons[3].color;
            StoredColors[3].a = 0;
            UIButtons[3].color = StoredColors[3];
        }

        //If Quad, Tri or Dual is in use, 
        if (MultiTimerDual || MultiTimerTri || MultiTimerQuad)
        {
            StartCoroutine("RemoveSecondButton", CountDownTimes[1]);

        }
        //If Quad or Tri is in use, 
        if (MultiTimerTri || MultiTimerQuad)
        {
            StartCoroutine("RemoveThirdButton", CountDownTimes[2]);

        }
        //If Quad is in use, 
        if (MultiTimerQuad)
        {
            // Debug.Log("Renove 4th button Started");
            StartCoroutine("RemoveForthButton", CountDownTimes[3]);
        }

    }

    private void SetupQTE()
    {
        #region Setup Initial Values
        //Flip to true so that input detection happens.
        QTEactive = true;
        Triggered = true;

        MainTimerCount1 = triggerTimeout;
        MainTimerCount2 = triggerTimeout;
        MainTimerCount3 = triggerTimeout;
        MainTimerCount4 = triggerTimeout;

        //set addition timers if nessasary
        if (MultiTimerDual || MultiTimerTri || MultiTimerQuad)
        {
            MainTimerCount2 = CountDownTimes[1];
        }
        if (MultiTimerTri || MultiTimerQuad)
        {
            MainTimerCount3 = CountDownTimes[2];
        }
        if (MultiTimerQuad)
        {
            MainTimerCount4 = CountDownTimes[3];
        }

        //reset any previously used values
        succeeded = false;
        succeeded2 = false;
        succeeded3 = false;
        succeeded4 = false;
        KeyGotPressed = false;
        MashValue = 0.0f;
        QTECompleted = false;

        //find out the current size of the canvas
        Rect size = MainCanvas.GetComponent<RectTransform>().rect;
        WidthMin = (-size.width / 2) + CanvasPadding.x;
        WidthMax = (size.width / 2) - CanvasPadding.x;
        HeightMin = (-size.height / 2) + CanvasPadding.y;
        HeightMax = (size.height / 2) - CanvasPadding.y;

        #endregion

        #region Randomize Button Positions
        //if Randomize poistions is checked, generate a random posistion to use
        if (RandomizeButtonPositions[0])
        {

            ButtonPositions[0] = new Vector3(Random.Range(WidthMin, WidthMax), Random.Range(HeightMin, HeightMax), 0);
            UIButtons[0].transform.localPosition = ButtonPositions[0];
        }

        if (RandomizeButtonPositions[1])
        {

            ButtonPositions[1] = new Vector3(Random.Range(WidthMin, WidthMax), Random.Range(HeightMin, HeightMax), 0);
            UIButtons[1].transform.localPosition = ButtonPositions[1];
        }

        if (RandomizeButtonPositions[2])
        {

            ButtonPositions[2] = new Vector3(Random.Range(WidthMin, WidthMax), Random.Range(HeightMin, HeightMax), 0);
            UIButtons[3].transform.localPosition = ButtonPositions[2];
        }

        if (RandomizeButtonPositions[3])
        {

            ButtonPositions[3] = new Vector3(Random.Range(WidthMin, WidthMax), Random.Range(HeightMin, HeightMax), 0);
            UIButtons[3].transform.localPosition = ButtonPositions[3];
        }
        #endregion

        if (OverideButtonPosition)
        {
            UIButtons[0].transform.localPosition = ButtonPositions[0];
        }

        UIButtons[0].sprite = UISprites[0];
        UIButtons[0].gameObject.SetActive(true);

        if (!TimerDelayToggle)
        {
            EnableMultiButtons();
        }

    }

    //This Function Enables the other UI Buttons if needed
    private void EnableMultiButtons()
    {

        if (DualTrigger || TriTrigger || QuadTrigger)
        {
            UIButtons[1].gameObject.SetActive(true);
            // Debug.Log(UIButtons[1].gameObject.activeSelf);
            UIButtons[1].sprite = UISprites[1];
            if (OverideButtonPosition)
            {
                UIButtons[1].transform.localPosition = ButtonPositions[1];
            }
        }

        if (TriTrigger || QuadTrigger)
        {
            UIButtons[2].gameObject.SetActive(true);
            UIButtons[2].sprite = UISprites[2];
            if (OverideButtonPosition)
            {
                UIButtons[2].transform.localPosition = ButtonPositions[2];
            }
        }

        if (QuadTrigger)
        {
            UIButtons[3].gameObject.SetActive(true);
            UIButtons[3].sprite = UISprites[3];
            if (OverideButtonPosition)
            {
                UIButtons[3].transform.localPosition = ButtonPositions[3];
            }
        }
    }

    //enables or disables the addition Buttons that get generated, this is to hide them while the Delay Timer is active.
    private void ToggleMultiButtonVisibility(bool flip)
    {
        if (flip == true)
        {
            if (DualTrigger || TriTrigger || QuadTrigger)
            {
                UIButtons[1].gameObject.SetActive(true);
            }
            if (TriTrigger || QuadTrigger)
            {
                UIButtons[2].gameObject.SetActive(true);
            }
            if (QuadTrigger)
            {
                UIButtons[3].gameObject.SetActive(true);
            }
        }
        else
        {
            if (DualTrigger || TriTrigger || QuadTrigger)
            {
                UIButtons[1].gameObject.SetActive(false);
            }
            if (TriTrigger || QuadTrigger)
            {
                UIButtons[2].gameObject.SetActive(false);
            }
            if (QuadTrigger)
            {
                UIButtons[3].gameObject.SetActive(false);
            }
        }
    }

    //The coroutine Timer for delaying Input Detection
    IEnumerator InputDelay(float delay)
    {
        DelayToggle = true;
        yield return new WaitForSeconds(delay);
        DelayTimeout = 0;
        DelayToggle = false;
    }

    //function evaluates what Input button got pressed.
    void PressButton()
    {

        if (Input.GetButtonDown(KeyPress) && UIButtons[0].gameObject.activeSelf)
        {
            if (!ButtonIsAxisCheck[0])
            { // Debug.Log("Button 1 succeeded");
                succeeded = true;
                KeyGotPressed = true;
                WrongButtonFail = false;
                CheckResult();
            }
        }
        else if (DualTrigger && Input.GetButtonDown(KeyPress2) && UIButtons[1].gameObject.activeSelf || TriTrigger && Input.GetButtonDown(KeyPress2) && UIButtons[1].gameObject.activeSelf || QuadTrigger && Input.GetButtonDown(KeyPress2) && UIButtons[1].gameObject.activeSelf)
        {
            if (!ButtonIsAxisCheck[1])
            {
                // Debug.Log("Button 2 succeeded");
                succeeded2 = true;
                KeyGotPressed = true;
                WrongButtonFail = false;
                CheckResult();
            }
        }
        else if (TriTrigger && Input.GetButtonDown(KeyPress3) && UIButtons[2].gameObject.activeSelf || QuadTrigger && Input.GetButtonDown(KeyPress3) && UIButtons[2].gameObject.activeSelf)
        {
            if (!ButtonIsAxisCheck[2])
            {
                // Debug.Log("Button 3 succeeded");
                succeeded3 = true;
                KeyGotPressed = true;
                WrongButtonFail = false;
                CheckResult();
            }
        }
        else if (QuadTrigger && Input.GetButtonDown(KeyPress4) && UIButtons[3].gameObject.activeSelf)
        {
            if (!ButtonIsAxisCheck[3])
            {
                // Debug.Log("Button 4 succeeded");
                succeeded4 = true;
                KeyGotPressed = true;
                WrongButtonFail = false;
                CheckResult();
            }
        }
        else
        {
            if (EnableButtonFail)
            {
                if (!ButtonIsAxisCheck[0] && !ButtonIsAxisCheck[1] && !ButtonIsAxisCheck[2] && !ButtonIsAxisCheck[3])
                {
                    // Debug.Log("Failed Wrong BUtton");
                    KeyGotPressed = true;
                    WrongButtonFail = true;
                    MainTimerCount1 = 0;
                    CheckResult();
                }
            }
        }
    }

    //function evaluates what Keyboard button got pressed.
    void PressKey()
    {
        // Debug.Log("Press Key");
        if (Input.GetKeyDown(KeyPress) && UIButtons[0].gameObject.activeSelf)
        {
            // Debug.Log("Key 1 succeeded");
            succeeded = true;
            KeyGotPressed = true;
            WrongButtonFail = false;
            CheckResult();

        }
        else if (DualTrigger && Input.GetKeyDown(KeyPress2) && UIButtons[1].gameObject.activeSelf || TriTrigger && Input.GetKeyDown(KeyPress2) && UIButtons[1].gameObject.activeSelf || QuadTrigger && Input.GetKeyDown(KeyPress2) && UIButtons[1].gameObject.activeSelf)
        {
            // Debug.Log("Key 2 succeeded");
            succeeded2 = true;
            KeyGotPressed = true;
            WrongButtonFail = false;
            CheckResult();

        }
        else if (TriTrigger && Input.GetKeyDown(KeyPress3) && UIButtons[2].gameObject.activeSelf || QuadTrigger && Input.GetKeyDown(KeyPress3) && UIButtons[2].gameObject.activeSelf)
        {
            // Debug.Log("Button 3 succeeded");
            succeeded3 = true;
            KeyGotPressed = true;
            WrongButtonFail = false;
            CheckResult();

        }
        else if (QuadTrigger && Input.GetKeyDown(KeyPress4) && UIButtons[3].gameObject.activeSelf)
        {
            // Debug.Log("Button 4 succeeded");
            succeeded4 = true;
            KeyGotPressed = true;
            WrongButtonFail = false;
            CheckResult();

        }
        else
        {
            if (EnableButtonFail)
            {
                KeyGotPressed = true;
                WrongButtonFail = true;
                MainTimerCount1 = 0;
                CheckResult();
            }
        }
    }

    //function Detects what axis or Input button is used
    void AxisDectection(int index, string name, bool mash)
    {
        //Only Run if the input is actually an axis, if not it's a button to be pressed.
        if (ButtonIsAxisCheck[index])
        {
            Debug.Log("detecting axis Input");
            //Only Active if the onscreen Button is active.
            if (UIButtons[index].gameObject.activeSelf)
            {

                if (ButtonAxisDetection[index] == 0)
                {
                    //Debug.Log(name + " 0");
                    if (Input.GetAxis(name) > ButtonAxisThresholds[index])
                    {
                        if (mash)
                        {
                            MashValue += 0.2f;
                        }
                        else
                        {
                            succeeded = true;
                            KeyGotPressed = true;
                            CheckResult();
                        }

                    }
                    else if (Input.GetAxis(name) < -0.1)
                    {
                        if (!mash)
                        {
                            if (EnableButtonFail)
                            {
                                WrongButtonFail = true;
                                KeyGotPressed = true;
                                CheckResult();
                            }
                        }
                    }
                    if (AlternateAxisFailure[index])
                    {
                        if (Mathf.Abs(Input.GetAxis(FailureInputName[0])) > 0.1f)
                        {
                            WrongButtonFail = true;
                            KeyGotPressed = true;
                            CheckResult();
                        }
                    }
                }
                else if (ButtonAxisDetection[index] == 1)
                {
                    Debug.Log(name + " 1");
                    if (Input.GetAxis(name) < -ButtonAxisThresholds[index])
                    {
                        if (mash)
                        {
                            MashValue += 0.2f;
                        }
                        else
                        {
                            succeeded = true;
                            KeyGotPressed = true;
                            CheckResult();
                        }
                    }
                    else if (Input.GetAxis(name) > 0.1f)
                    {
                        if (!mash)
                        {
                            if (EnableButtonFail)
                            {
                                WrongButtonFail = true;
                                KeyGotPressed = true;
                                CheckResult();
                            }
                        }
                    }

                    if (AlternateAxisFailure[index])
                    {
                        // Debug.Log(Input.GetAxis(FailureInputName[0]));
                        if (Mathf.Abs(Input.GetAxis(FailureInputName[0])) > 0.1f)
                        {
                            WrongButtonFail = true;
                            KeyGotPressed = true;
                            CheckResult();
                        }
                    }
                }
            }
        }
    }

   
    public static float Angle(Vector2 p_vector2)
    {
        if (p_vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (QTECompleted)
        {
            QTECompleted = false;
        }

        if (QTEStarted)
        {
            QTEStarted = false;
        }

        

        //if hidden is true, remove the textures.
        if (Hidden)
        {
            //			QTE_texture.texture = null;
            UIButtons[0].color = Color.clear;
            if (DualTrigger || TriTrigger || QuadTrigger)
            {
                UIButtons[1].color = Color.clear;
            }
            if (TriTrigger || QuadTrigger)
            {
                UIButtons[2].color = Color.clear;
            }
            if (QuadTrigger)
            {
                UIButtons[3].color = Color.clear;
            }

        }

        #region Shaking Buttons
        //if shake is true
        if (ButtonShaking[0] && QTEactive)
        {
            //Debug.Log("Shaking");

            var newX = Random.Range(0.5f - ButtonShakeOffests[0], 0.5F + ButtonShakeOffests[0]);
            var newY = Random.Range(0.5f - ButtonShakeOffests[0], 0.5F + ButtonShakeOffests[0]);

            //transform.localPosition = new Vector3(newX, newY, 0.0f);
            //Debug.Log(ButtonPositions[0]);
            Vector3 myPos = new Vector3(ButtonPositions[0].x + newX, ButtonPositions[0].y + newY, ButtonPositions[0].z);

            UIButtons[0].transform.localPosition = myPos;
        }

        if (ButtonShaking[1] && QTEactive && UIButtons[1].gameObject.activeSelf)
        {

            var newX = Random.Range(0.5f - ButtonShakeOffests[1], 0.5F + ButtonShakeOffests[1]);
            var newY = Random.Range(0.5f - ButtonShakeOffests[1], 0.5F + ButtonShakeOffests[1]);

            Vector3 myPos = new Vector3(ButtonPositions[1].x + newX, ButtonPositions[1].y + newY, ButtonPositions[1].z);
            UIButtons[1].transform.localPosition = myPos;

        }

        if (ButtonShaking[2] && QTEactive && UIButtons[2].gameObject.activeSelf)
        {

            var newX = Random.Range(0.5f - ButtonShakeOffests[2], 0.5F + ButtonShakeOffests[2]);
            var newY = Random.Range(0.5f - ButtonShakeOffests[2], 0.5F + ButtonShakeOffests[2]);
            Vector3 myPos = new Vector3(ButtonPositions[2].x + newX, ButtonPositions[2].y + newY, ButtonPositions[2].z);
            UIButtons[2].transform.localPosition = myPos;

        }

        if (ButtonShaking[3] && QTEactive && UIButtons[3].gameObject.activeSelf)
        {

            var newX = Random.Range(0.5f - ButtonShakeOffests[3], 0.5F + ButtonShakeOffests[3]);
            var newY = Random.Range(0.5f - ButtonShakeOffests[3], 0.5F + ButtonShakeOffests[3]);
            Vector3 myPos = new Vector3(ButtonPositions[3].x + newX, ButtonPositions[3].y + newY, ButtonPositions[3].z);
            UIButtons[3].transform.localPosition = myPos;

        }
        #endregion


        if (TimerDelayToggle)
        {
            //Debug.Log("delay timer started");
            if (TimerDelayTimeout > 0)
            {

                TimerDelayTimeout -= Time.deltaTime;

                //when the timer finishes.
                if (TimerDelayTimeout <= 0)
                {
                    //Debug.Log("delay timer finished");
                    ToggleMultiButtonVisibility(true);
                    SetupQTE();
                    //turn off the timer's countdown
                    TimerDelayToggle = false;

                    //reset the timer
                    TimerDelayTimeout = 0;
                }
            }
        }


        //when the QTE gets Triggered, Is Active, and is not being prevented by the Delay Timer.
        if (Triggered && QTEactive && !DelayToggle)
        {
            //If Fade In Ui is checked, Increase the value of the alpha channel over time
            if (FadeInUI)
            {
                if (StoredColors[0].a < 1)
                {
                    StoredColors[0].a += Time.deltaTime / FadeInTimer;
                    UIButtons[0].color = StoredColors[0];
                }

                if (StoredColors[1].a < 1)
                {
                    StoredColors[1].a += Time.deltaTime / FadeInTimer;
                    UIButtons[1].color = StoredColors[1];
                }

                if (StoredColors[2].a < 1)
                {
                    StoredColors[2].a += Time.deltaTime / FadeInTimer;
                    UIButtons[2].color = StoredColors[2];
                }

                if (StoredColors[3].a < 1)
                {
                    StoredColors[3].a += Time.deltaTime / FadeInTimer;
                    UIButtons[3].color = StoredColors[3];
                }
            }



            //if the QTE is Mashable
            if (Mashable)
            {
                //only execute when the UI texture is visble.
                if (UIButtons[0].gameObject.activeSelf)
                {

                    //scale the texture up and down (pulsate)
                    float frequency = Time.time * PulsateSpeed;
                    float amplitude = Mathf.Cos(frequency) * PulsateFrequency + PulsateFrequency;
                    UIButtons[0].transform.localScale = new Vector3(StoredScale.x + amplitude, StoredScale.y + amplitude, 1.0f);

                    //if the Mashvalue is greater than 0, constantly subtract away from it using the tolerance value
                    if (MashValue > 0)
                    {
                        //15 02 2020 Victor ma for testing a better outcome
                        //MashValue -= tolerance;
                        MashValue -= 0;
                    }

                    //if the player sucessfully got the value at or over 1, he succeeded
                    if (MashValue >= 1.0f)
                    {
                        succeeded = true;
                        KeyGotPressed = true;

                        /*
                         * When mashing success TODO:
                         */

                        CheckResult();
                    }

                    //If using Unitys inputs
                    if (myInputOptions == InputOptions.UnityInputManager)
                    {
                        if (KeyPress != null)
                        {
                            //If input is an axis
                            AxisDectection(0, KeyPress, true);

                            //if input is a button
                            if (Input.GetButtonDown(KeyPress))
                            {
                                if (!ButtonIsAxisCheck[0])
                                {
                                    MashValue += 0.2f;
                                }
                            }
                        }

                    }
                    else
                    {
                        //if using regular keyboard input
                        if (KeyPress != null)
                        {
                            if (Input.GetKeyDown(KeyPress))
                            {
                                MashValue += 0.015f;
                            }
                        }
                    }

                }

            }
            else
            {
                // If not mashable (then its the other QTEs)

                if (EnableButtonFail)
                {
                    //  Debug.Log("Button Can be Failed");

                    //If one of the buttons is an Axis
                    if (myInputOptions == InputOptions.UnityInputManager && KeyPress != null)
                    {
                        AxisDectection(0, KeyPress, false);
                        if (DualTrigger || TriTrigger || QuadTrigger)
                        {
                            AxisDectection(1, KeyPress2, false);
                        }
                        if (TriTrigger || QuadTrigger)
                        {
                            AxisDectection(2, KeyPress3, false);
                        }
                        if (QuadTrigger)
                        {
                            AxisDectection(3, KeyPress4, false);
                        }
                    }

                    //grab any key input, but only while the texture is being shown, and the Keypress is not one of the Axes.

                    if (Input.anyKeyDown && KeyPress != null)
                    {
                        //if using Unity's predefined Inputs
                        if (myInputOptions == InputOptions.UnityInputManager)
                        {
                            // Debug.Log("Using Inputs, Failure enabled");
                            PressButton();
                        }
                        else
                        {
                            //if using regular keyboard input	
                            // Debug.Log("Using Keys, Failure enabled");
                            PressKey();
                        }
                    }
                }
                else
                {
                    //grab any key input, but only while the texture is being shown, and the Keypress is not one of the Axes.
                    if (UIButtons[0].gameObject.activeSelf)
                    {
                        
                        //if using Unity's predefined Inputs
                        if (myInputOptions == InputOptions.UnityInputManager)
                        {
                            // Debug.Log("Using Inputs, Failure disabled");
                            PressButton();
                        }
                        else
                        {
                            // Debug.Log("Using Keys, Failure disabled");
                            //if using regular keyboard input	
                            PressKey();
                        }
                    }
                }
            }


            #region Timers

            

            /*   if (TouchHoldToggle)
               {
                   //DebugText.text = TouchHoldCounter.ToString ();
                   if (TouchHoldCounter > 0)
                   {
                       TouchHoldCounter -= Time.deltaTime;

                       if (TouchHoldCounter <= 0)
                       {
                           succeeded = true;
                           KeyGotPressed = true;
                           CheckResult();

                           TouchHoldCounter = 0;
                           TouchHoldToggle = false;
                       }
                   }
               */



            //Input Delay Countdown
            if (DelayTimeout > 0)
            {
                StartCoroutine("InputDelay", DelayTimeout);
            }

            



            //Timer that counts down for Fading in UI buttons
            if (FadeInUI)
            {
                if (FadeInTimer > 0)
                {
                    FadeInTimer -= Time.deltaTime;

                    //when the timer finishes.
                    if (FadeInTimer <= 0)
                    {
                        //reset the timer
                        FadeInTimer = 0;
                    }
                }
            }

            //Theses are the 4 timers that count down for each QTE button
            if (MainTimerCount1 > 0)
            {

                MainTimerCount1 -= Time.deltaTime;

                //when the timer finishes.
                if (MainTimerCount1 <= 0)
                {
                    //   Debug.Log("Timer 1 finished");
                    if (!WrongButtonFail)
                    {
                        CheckResult();
                    }

                    //reset the timer
                    MainTimerCount1 = 0;
                }
            }

            if (MultiTimerDual || MultiTimerTri || MultiTimerQuad)
            {
                if (MainTimerCount2 > 0)
                {

                    MainTimerCount2 -= Time.deltaTime;

                    //when the timer finishes.
                    if (MainTimerCount2 <= 0)
                    {

                        //reset the timer
                        MainTimerCount2 = 0;
                    }
                }
            }

            if (MultiTimerTri || MultiTimerQuad)
            {
                if (MainTimerCount3 > 0)
                {

                    MainTimerCount3 -= Time.deltaTime;

                    //when the timer finishes.
                    if (MainTimerCount3 <= 0)
                    {

                        //reset the timer
                        MainTimerCount3 = 0;
                    }
                }
            }

            if (MultiTimerQuad)
            {
                if (MainTimerCount4 > 0)
                {

                    MainTimerCount4 -= Time.deltaTime;

                    //when the timer finishes.
                    if (MainTimerCount4 <= 0)
                    {
                        //reset the timer
                        MainTimerCount4 = 0;
                    }
                }
            }
        }
        #endregion

        if (NoTimer)
        {
            // if the player has stopped mashing, begin the countdown timer.
            if (Mashable)
            {
                if (MashValue <= 0)
                {
                    if (triggerTimeout > 0)
                    {
                        triggerTimeout -= Time.deltaTime;


                        //when the timer finishes.
                        if (triggerTimeout <= 0)
                        {

                            triggerTimeout = 0f;

                            // if no key got pressed, then he failed
                            if (!KeyGotPressed)
                            {
                                CheckResult();
                            }
                        }
                    }
                }
                else
                {
                    //if the player is mashing, reset the timer
                    triggerTimeout = CountDownTimes[0];
                }
            }
        }

    }

    #region ButtonCleanup
    //Functions that remove the buttons from displaying.

    void Remove2ndButton()
    {
        UIButtons[1].gameObject.SetActive(false);
        if (Button2TimerUI != null)
        {
            Button2TimerUI.SetActive(false);
        }

    }

    void Remove3rdButton()
    {

        UIButtons[2].gameObject.SetActive(false);
        if (Button3TimerUI != null)
        {
            Button3TimerUI.SetActive(false);
        }

    }

    void Remove4thButton()
    {
        // Debug.Log("Remove 4th button");
        UIButtons[3].gameObject.SetActive(false);
        if (Button4TimerUI != null)
        {
            Button4TimerUI.SetActive(false);
        }
    }

    //Coroutine Timers for removing buttons
    IEnumerator RemoveSecondButton(float delay)
    {
        yield return new WaitForSeconds(delay);
        Remove2ndButton();
    }

    IEnumerator RemoveThirdButton(float delay)
    {
        yield return new WaitForSeconds(delay);
        Remove3rdButton();
    }

    IEnumerator RemoveForthButton(float delay)
    {
        yield return new WaitForSeconds(delay);
        Remove4thButton();
    }
    #endregion

    //This funtion when called cancells out any active QTEs, No response happens.
    public void InteruptQTE()
    {
        QTEactive = false;
        Triggered = false;
        UIButtons[0].gameObject.SetActive(false);

        if (Button1TimerUI != null)
        {
            Button1TimerUI.SetActive(false);
        }

        if (DualTrigger || TriTrigger || QuadTrigger)
        {
            Remove2ndButton();
        }
        if (TriTrigger || QuadTrigger)
        {
            Remove3rdButton();
        }
        if (QuadTrigger)
        {
            Remove4thButton();
        }
        OtherCanvas = null;

        if (Mashable)
        {
            UIButtons[0].transform.localScale = StoredScale;
        }
        Mashable = false;
        DualTrigger = false;
        TriTrigger = false;
        QuadTrigger = false;
        MultiTimerDual = false;
        MultiTimerTri = false;
        MultiTimerQuad = false;
        TravelWithParent = false;
        ButtonShaking[0] = false;
        ButtonShaking[1] = false;
        ButtonShaking[2] = false;
        ButtonShaking[3] = false;
        QTEactive = false;
        KeyPress = null;
        KeyPress2 = null;
        KeyPress3 = null;
        KeyPress4 = null;
        MainTimerCount1 = 0;
        MainTimerCount2 = 0;
        MainTimerCount3 = 0;
        MainTimerCount4 = 0;
        triggerTimeout = 0;
    }

    //CheckResult is called when the QTE completes, evaulates what happend, and determines the success or failure
    public void CheckResult()
    {
        //disable the texture
        UIButtons[0].gameObject.SetActive(false);

        FadeInTimer = 0;


        if (DualTrigger || TriTrigger || QuadTrigger)
        {
            Remove2ndButton();
        }
        if (TriTrigger || QuadTrigger)
        {
            Remove3rdButton();
        }
        if (QuadTrigger)
        {
            Remove4thButton();
        }

        if (Mashable)
        {
            UIButtons[0].transform.localScale = StoredScale;
        }


        //reset all values
        Mashable = false;
        MashValue = 0.0f;
        DualTrigger = false;
        TriTrigger = false;
        QuadTrigger = false;
        TravelWithParent = false;
        ButtonShaking[0] = false;
        ButtonShaking[1] = false;
        ButtonShaking[2] = false;
        ButtonShaking[3] = false;
        MainTimerCount1 = 0;
        MainTimerCount2 = 0;
        MainTimerCount3 = 0;
        MainTimerCount4 = 0;

        MultiTimerDual = false;
        MultiTimerTri = false;
        MultiTimerQuad = false;


        triggerTimeout = 0;

        KeyPress = null;
        KeyPress2 = null;
        KeyPress3 = null;
        KeyPress4 = null;
        //TouchHoldToggle = false;

        StopCoroutine("Timer");
        StopCoroutine("InputDelay");
        StopCoroutine("RemoveSecondButton");
        StopCoroutine("RemoveThirdButton");
        StopCoroutine("RemoveFourthButton");

        UIButtons[0].transform.rotation = Quaternion.identity;

        //check to see if he suceeded or not;
        if (succeeded)
        {
            //  Debug.Log("Passed Option 1");
        }
        else if (succeeded2)
        {
            //Debug.Log("Passed Option 2");
        }
        else if (succeeded3)
        {
            // Debug.Log("Passed Option 3");
        }
        else if (succeeded4)
        {
            // Debug.Log("Passed Option 4");
        }
        else
        {
            if (WrongButtonFail)
            {
                //  Debug.Log("Failed - Wrong Button");
                QTE_Failed_WrongButton = true;
            }
            else
            {
                // Debug.Log("Failed - Timer:" + TriggeringObject);
                QTE_Failed_timer = true;
            }

        }
        KeyGotPressed = false;
        WrongButtonFail = false;
        QTEactive = false;
        QTECompleted = true;

        //		TurnOffQTECompleted();
    }
}
