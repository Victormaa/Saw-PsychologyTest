using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class VicTrigger : QTE_BaseClass
{
    //The override Canvas
    public GameObject Canvas;
    public bool TriggerEnabled = true;

    //If false, QTE can only be triggered once
    public bool Repeatable = false;

    public string[] myNewDirections = new string[] { "Positive", "Negitive" };

    public List<string> ListOfInputs = new List<string>();

    public int[] ButtonIndexes = new int[4];

    public bool MultiTimer;
    public float DelayTime;
    public float TimerDelayTime;

    public string[] ButtonKeyPresses = new string[4];

    public bool[] UseRandomButtons = new bool[4];

    public string[] ArrayOfRandomButtons1, ArrayOfRandomButtons2, ArrayOfRandomButtons3, ArrayOfRandomButtons4;

    public int[] RandomNumbers = new int[4];

    private Object[] textures;

    private Collider2D Bcoll;
    private bool IsCollider;

    public enum OPTIONS
    {
        Single,
        Dual,
        Tri,
        Quad,
        Mash,
    }



    public OPTIONS QTEtype = OPTIONS.Single;


    // bool direction;

    void Start()
    {

        //Check to see if the attached gameobject has a Collider or not
        Bcoll = GetComponent<Collider2D>();

        if (Bcoll != null && Bcoll.isTrigger)
        {
            IsCollider = true;
        }
        else
        {
            IsCollider = false;
        }

        if (!IsCollider)
        {
            TriggerEnabled = false;
            Repeatable = false;
        }

        if (Canvas != null)
        {
            Canvas.transform.Find("QTE_UI/Button1").gameObject.SetActive(false);
            Canvas.transform.Find("QTE_UI/Button2").gameObject.SetActive(false);
            Canvas.transform.Find("QTE_UI/Button3").gameObject.SetActive(false);
            Canvas.transform.Find("QTE_UI/Button4").gameObject.SetActive(false);
        }
    }

    //Called by the user when QTE is being used manually.
    public void TriggerQTE()
    {
        TriggerEnabled = true;
    }

    void PrintError(string button)
    {
        Debug.LogError("QTE System: An Image for the Key or Input '" + button + "' Could not be found, please make one and put it in the Images Asset.");
    }

    //Function that takes the requested button information, and returns the sprite
    Sprite GetButtonSprite(bool isAxis, int direction, string name)
    {
        Sprite mySprite;

        if (isAxis)
        {
            if (direction == 0)
            {
                mySprite = QTE_main.Singleton.QTEImagesAsset.InputSprite.Where(obj => obj.name == "QTE_" + name + "_Pos").SingleOrDefault();
            }
            else
            {
                mySprite = QTE_main.Singleton.QTEImagesAsset.InputSprite.Where(obj => obj.name == "QTE_" + name + "_Neg").SingleOrDefault();
            }

        }
        else
        {
            mySprite = QTE_main.Singleton.QTEImagesAsset.InputSprite.Where(obj => obj.name == "QTE_" + name).SingleOrDefault();
        }

        return mySprite;
    }

    //This function strips out text from the filename of a sprite, to retrieve the base name of the input/key it is for.
    string StripInputName(string myString)
    {
        string RemovePos = myString.Replace("_Pos", "");
        string RemoveNeg = RemovePos.Replace("_Neg", "");
        string result = RemoveNeg.Replace("QTE_", "");
        return result;
    }

    //This fuction Detects if the filename contains "pos" or "neg", meaning it's an direction of an AXis and not a button.
    //if so, set the ButtonIsAxisCheck value to true
    void DetectAxisDirection(int index, string filename)
    {
        if (filename.Contains("_Pos") || filename.Contains("_Neg"))
        {
            ButtonIsAxisCheck[index] = true;
            if (filename.Contains("_Pos"))
            {
                ButtonAxisDetection[index] = 0;
            }

            if (filename.Contains("_Neg"))
            {
                ButtonAxisDetection[index] = 1;
            }
        }
        else
        {
            ButtonIsAxisCheck[index] = false;
        }
    }


    //This fucnction will Generate random numbers stored in the RandomNumbers Array.
    //the index varible (0-3) determines what button it's generating a number for. 
    //In order to make sure the same button dose not get randomly chosen twice, the GenerateRandomExculsive functions are used
    void GenerateRandomNumbers(int index, List<Sprite> ListOfSprites)
    {
        if (index == 0)
        {
            RandomNumbers[index] = Random.Range(0, ListOfSprites.Count);
        }
        else if (index == 1)
        {
            RandomNumbers[index] = GenerateRandomExculsive(ListOfSprites, RandomNumbers[0]);
        }
        else if (index == 2)
        {
            RandomNumbers[index] = GenerateRandomExculsive(ListOfSprites, RandomNumbers[0], RandomNumbers[1]);
        }
        else
        {
            RandomNumbers[index] = GenerateRandomExculsive(ListOfSprites, RandomNumbers[0], RandomNumbers[1], RandomNumbers[2]);
        }
    }


    //function that returns the string name of the input to use, and loads it's sprite.
    string ButtonInput(int index, string[] randomOptions)
    {

        string filename;
        string result;

        //if using a random button
        if (UseRandomButtons[index])
        {

            //if the array is empty, the user has not specified a list of inputs to choose from, so instead choose from all avalible inputs.
            if (randomOptions.Length == 0)
            {

                if (myInputOptions != InputOptions.UnityInputManager)
                {

                    GenerateRandomNumbers(index, QTE_main.Singleton.QTEImagesAsset.KeyboardSprite);
                    filename = QTE_main.Singleton.QTEImagesAsset.KeyboardSprite[RandomNumbers[index]].name;
                }
                else
                {
                    GenerateRandomNumbers(index, QTE_main.Singleton.QTEImagesAsset.InputSprite);
                    filename = QTE_main.Singleton.QTEImagesAsset.InputSprite[RandomNumbers[index]].name;
                }

                //If the filename for the button contains "_Pos" or "_Neg", that means the input is an axis, and not a button.
                //So Set the Axis check value to true, and determine which direction the AXis wants

                DetectAxisDirection(index, filename);

                result = StripInputName(filename);

                if (myInputOptions != InputOptions.UnityInputManager)
                {

                    UISprites[index] = QTE_main.Singleton.QTEImagesAsset.KeyboardSprite.Where(obj => obj.name == "QTE_" + result).SingleOrDefault();
                }
                else
                {
                    UISprites[index] = GetButtonSprite(ButtonIsAxisCheck[index], ButtonAxisDetection[index], result);
                }

                return result;

            }
            else
            {
                //choose a random input the user specified in the array
                var randomNumber = Random.Range(0, randomOptions.Length);
                filename = randomOptions[randomNumber].ToString();

                //detect if it is an axis.
                DetectAxisDirection(index, filename);

                //find out the name of the input
                result = StripInputName(filename);

                //load the Input Sprite
                if (myInputOptions == InputOptions.UnityInputManager)
                {
                    UISprites[index] = GetButtonSprite(ButtonIsAxisCheck[index], ButtonAxisDetection[index], result);
                    QTE_main.Singleton.UISprites[index] = UISprites[index];
                }
                else
                {
                    UISprites[index] = QTE_main.Singleton.QTEImagesAsset.KeyboardSprite.Where(obj => obj.name == "QTE_" + result).SingleOrDefault();
                    QTE_main.Singleton.UISprites[index] = UISprites[index];
                }

                //return input name
                return result;
            }

        }
        else
        {
            // if not using a random button 


            //load the input's sprite
            if (myInputOptions != InputOptions.UnityInputManager)
            {

                UISprites[index] = QTE_main.Singleton.QTEImagesAsset.KeyboardSprite.Where(obj => obj.name == "QTE_" + ButtonKeyPresses[index]).SingleOrDefault();
                //Debug.Log(UISprites[index]);
            }
            else
            {
                UISprites[index] = GetButtonSprite(ButtonIsAxisCheck[index], ButtonAxisDetection[index], ButtonKeyPresses[index]);
            }

            return ButtonKeyPresses[index];

        }
    }


    void QTE()
    {
        if (myInputOptions == InputOptions.UnityInputManager)
        {
            ButtonKeyPresses[0] = ListOfInputs[ButtonIndexes[0]];
            ButtonKeyPresses[1] = ListOfInputs[ButtonIndexes[1]];
            ButtonKeyPresses[2] = ListOfInputs[ButtonIndexes[2]];
            ButtonKeyPresses[3] = ListOfInputs[ButtonIndexes[3]];

            FailureInputName[0] = ListOfInputs[FailureAxis[0]];
            FailureInputName[1] = ListOfInputs[FailureAxis[1]];
            FailureInputName[2] = ListOfInputs[FailureAxis[2]];
            FailureInputName[3] = ListOfInputs[FailureAxis[3]];
        }

        if (TriggerEnabled && !QTE_main.Singleton.QTEactive)
        {
            //Set the varibles of the Main Script to the values provided by the instance of this script.

            if (Canvas != null)
            {
                QTE_main.Singleton.OtherCanvas = Canvas;
            }
            else
            {
                QTE_main.Singleton.OtherCanvas = null;
            }

            QTE_main.Singleton.OverideButtonPosition = OverideButtonPosition;

            if (OverideButtonPosition)
            {
                QTE_main.Singleton.ButtonPositions = ButtonPositions;
            }


            if (QTEtype == OPTIONS.Dual)
            {
                QTE_main.Singleton.DualTrigger = true;
            }
            else if (QTEtype == OPTIONS.Tri)
            {
                QTE_main.Singleton.TriTrigger = true;
            }
            else if (QTEtype == OPTIONS.Quad)
            {
                QTE_main.Singleton.QuadTrigger = true;
            }
            else if (QTEtype == OPTIONS.Mash)
            {
                QTE_main.Singleton.Mashable = true;
                QTE_main.Singleton.NoTimer = NoTimer;
                QTE_main.Singleton.PulsateSpeed = PulsateSpeed;
                QTE_main.Singleton.PulsateFrequency = PulsateFrequency;
                QTE_main.Singleton.tolerance = tolerance;
            }


            QTE_main.Singleton.KeyPress = ButtonInput(0, ArrayOfRandomButtons1);

            if (QTEtype == OPTIONS.Dual || QTEtype == OPTIONS.Tri || QTEtype == OPTIONS.Quad)
            {

                QTE_main.Singleton.KeyPress2 = ButtonInput(1, ArrayOfRandomButtons2);

            }

            if (QTEtype == OPTIONS.Tri || QTEtype == OPTIONS.Quad)
            {
                QTE_main.Singleton.KeyPress3 = ButtonInput(2, ArrayOfRandomButtons3);

            }

            if (QTEtype == OPTIONS.Quad)
            {
                QTE_main.Singleton.KeyPress4 = ButtonInput(3, ArrayOfRandomButtons4);
                //Debug.Log(QTE_main.Singleton.KeyPress4);
            }


            QTE_main.Singleton.EnableButtonFail = EnableButtonFail;

            QTE_main.Singleton.FailureInputName = FailureInputName;

            //These booleans determine if the Chosen input is an Axis or not
            QTE_main.Singleton.ButtonIsAxisCheck = ButtonIsAxisCheck; ;

            //These floats are the thresolds axes have to pass to succeed.
            QTE_main.Singleton.ButtonAxisThresholds = ButtonAxisThresholds;

            //these floats are either set to 0 (Positive) or 1 (Negitive) to specifiy what Axis direction to look for
            QTE_main.Singleton.ButtonAxisDetection = ButtonAxisDetection;

            QTE_main.Singleton.FailureAxis = FailureAxis;

            QTE_main.Singleton.AlternateAxisFailure = AlternateAxisFailure;

            QTE_main.Singleton.HoldActions = HoldActions;
            QTE_main.Singleton.HoldTimes = HoldTimes;

            if (MultiTimer)
            {
                QTE_main.Singleton.MultiTimerQuad = MultiTimer;
                if (QTEtype == OPTIONS.Dual || QTEtype == OPTIONS.Tri || QTEtype == OPTIONS.Quad)
                {
                    QTE_main.Singleton.CountDownTimes[1] = CountDownTimes[1];
                }
                if (QTEtype == OPTIONS.Tri || QTEtype == OPTIONS.Quad)
                {
                    QTE_main.Singleton.CountDownTimes[2] = CountDownTimes[2];
                }
                if (QTEtype == OPTIONS.Quad)
                {
                    QTE_main.Singleton.CountDownTimes[3] = CountDownTimes[3];
                }
            }

            if (UISprites[0] != null)
            {
                QTE_main.Singleton.UISprites[0] = UISprites[0];

            }
            else
            {
                PrintError(ButtonKeyPresses[0]);
            }

            if (QTEtype == OPTIONS.Dual || QTEtype == OPTIONS.Tri || QTEtype == OPTIONS.Quad)
            {
                if (UISprites[1] != null)
                {
                    QTE_main.Singleton.UISprites[1] = UISprites[1];
                }
                else
                {
                    PrintError(ButtonKeyPresses[1]);
                }
            }

            if (QTEtype == OPTIONS.Tri || QTEtype == OPTIONS.Quad)
            {
                if (UISprites[2] != null)
                {
                    QTE_main.Singleton.UISprites[2] = UISprites[2];
                }
                else
                {
                    PrintError(ButtonKeyPresses[2]);
                }
            }

            if (QTEtype == OPTIONS.Quad)
            {

                if (UISprites[3] != null)
                {
                    QTE_main.Singleton.UISprites[3] = UISprites[3];
                }
                else
                {
                    PrintError(ButtonKeyPresses[3]);
                }
            }

            QTE_main.Singleton.FadeInUI = FadeInUI;
            QTE_main.Singleton.FadeInTime = FadeInTime;
            QTE_main.Singleton.TriggeringObject = this.gameObject;
            QTE_main.Singleton.Hidden = Hidden;
            QTE_main.Singleton.UISprites[0] = UISprites[0];
            QTE_main.Singleton.RandomizeButtonPositions = RandomizeButtonPositions;
            QTE_main.Singleton.myInputOptions = myInputOptions;

            QTE_main.Singleton.CanvasPadding = CanvasPadding;
            QTE_main.Singleton.TriggerQTE(CountDownTimes[0], DelayTime, TimerDelayTime);

            //Pass the Shaking info from this instance to the main script
            if (ButtonShaking[0])
            {
                QTE_main.Singleton.ButtonShaking[0] = true;
                QTE_main.Singleton.ButtonShakeOffests[0] = ButtonShakeOffests[0];
            }
            else
            {
                QTE_main.Singleton.ButtonShaking[0] = false;
            }

            if (QTEtype == OPTIONS.Dual || QTEtype == OPTIONS.Tri || QTEtype == OPTIONS.Quad)
            {
                if (ButtonShaking[1])
                {
                    QTE_main.Singleton.ButtonShaking[1] = true;
                    QTE_main.Singleton.ButtonShakeOffests[1] = ButtonShakeOffests[1];
                }
                else
                {
                    QTE_main.Singleton.ButtonShaking[1] = false;
                }
            }

            if (QTEtype == OPTIONS.Tri || QTEtype == OPTIONS.Quad)
            {
                if (ButtonShaking[2])
                {
                    QTE_main.Singleton.ButtonShaking[2] = true;
                    QTE_main.Singleton.ButtonShakeOffests[2] = ButtonShakeOffests[2];
                }
                else
                {
                    QTE_main.Singleton.ButtonShaking[2] = false;
                }
            }

            if (QTEtype == OPTIONS.Quad)
            {
                if (ButtonShaking[3])
                {
                    QTE_main.Singleton.ButtonShaking[3] = true;
                    QTE_main.Singleton.ButtonShakeOffests[3] = ButtonShakeOffests[3];
                }
                else
                {
                    QTE_main.Singleton.ButtonShaking[3] = false;
                }
            }
        }

        //turn off this trigger
        if (!Repeatable && Bcoll != null || Bcoll == null)
        {
            TriggerEnabled = false;
        }
    }

    //functions for returing a random number, while exculding values
    public int GenerateRandomExculsive(List<Sprite> thing, int exclude1)
    {
        int newDir;

        newDir = Random.Range(0, thing.Count);
        while (newDir == exclude1)
        {
            newDir = Random.Range(0, thing.Count);
        }

        return newDir;

    }

    public int GenerateRandomExculsive(List<Sprite> thing, int exclude1, int exclude2)
    {
        int newDir;

        newDir = Random.Range(0, thing.Count);
        while (newDir == exclude1 || newDir == exclude2)
        {
            newDir = Random.Range(0, thing.Count);
        }

        return newDir;

    }

    public int GenerateRandomExculsive(List<Sprite> thing, int exclude1, int exclude2, int exclude3)
    {
        int newDir;

        newDir = Random.Range(0, thing.Count);
        while (newDir == exclude1 || newDir == exclude2 || newDir == exclude2)
        {
            newDir = Random.Range(0, thing.Count);
        }

        return newDir;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsCollider)
        {
            if (!collision.CompareTag("Player"))
            {
                return;
            }
            QTE();
        }
    }

    //if Attached to a Collider, trigger the QTE when he enters it
    void OnTriggerEnter(Collider collision)
    {
        if (IsCollider)
        {
            if (!collision.CompareTag("Player"))
            {
                return;
            }
            QTE();
        }
    }

    //if not attached to a Collider, then it will be triggered manually by the user.
    void Update()
    {
        if (!IsCollider)
        {
            QTE();
        }
    }
}
