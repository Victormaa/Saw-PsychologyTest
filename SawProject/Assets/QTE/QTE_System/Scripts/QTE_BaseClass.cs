/* Both QTE_Main.cs and QTE_Trigger.cs inherit from this base class because they both need a copy of these values. So that the Trigger can store them per trigger, and pass it off to the main script */
using UnityEngine;
using System.Collections;

public class QTE_BaseClass : MonoBehaviour
{
    //The 4 Sprites that can be drawn onto the scene
    [HideInInspector]
    public Sprite[] UISprites = new Sprite[4];

    //Boolean check to see if the button is an axis instead of a regualr button
    [HideInInspector]
    public bool[] ButtonIsAxisCheck = new bool[4];

    //These int values are set to be 0 or 1, they are used to determine if the Axis input is Positive or Negative
    [HideInInspector]
    public int[] ButtonAxisDetection = new int[4];

    [HideInInspector]
    public int[] FailureAxis = new int[4];

    [HideInInspector]
    public bool[] AlternateAxisFailure = new bool[4];

    //These floats are for when an Axis is used, theses thresholds are what the AXis needs to pass to succeed.
    [HideInInspector]
    public float[] ButtonAxisThresholds = new float[4] { 0.7f, 0.7f, 0.7f, 0.7f };

    //Boolean checks to randomize the position of the buttons in the canvas.
    [HideInInspector]
    public bool[] RandomizeButtonPositions = new bool[4];

    //These values add padding to the canvas, used when the position of the button is randomized, so they don't appear slighty off screen
    [HideInInspector]
    public Vector2 CanvasPadding;

    //Vector3s that the user can input to manually place the buttons on screen
    [HideInInspector]
    public Vector3[] ButtonPositions = new Vector3[4];

    //Floats for the invidiual timers
    [HideInInspector]
    public float[] CountDownTimes = new float[4] { 2.0f, 2.0f, 2.0f, 2.0f };


    [HideInInspector]
    public bool[] HoldActions = new bool[4];

    [HideInInspector]
    public float[] HoldTimes = new float[4] { 2.0f, 2.0f, 2.0f, 2.0f };

    [HideInInspector]
    public bool NoTimer;

    [HideInInspector]
    public string[] FailureInputName = new string[4];

    //Values for mashable QTE's
    [HideInInspector]
    public float tolerance = 0.01f;
    [HideInInspector]
    public float PulsateSpeed = 24;
    [HideInInspector]
    public float PulsateFrequency = 0.05f;

    [HideInInspector]
    public bool Hidden;
    [HideInInspector]
    public float[] ButtonShakeOffests = new float[4] { 5.0f, 5.0f, 5.0f, 5.0f };

    [HideInInspector]
    public bool OverideButtonPosition;

   [HideInInspector]
    public bool EnableButtonFail;

    //[HideInInspector]
    [HideInInspector]
    public bool[] ButtonShaking = new bool[4];

    [HideInInspector]
    public bool FadeInUI;
    [HideInInspector]
    public float FadeInTime = 0.5f;

    public enum InputOptions
    {
        UnityInputManager,
        PCKeyboard,
    }

    [HideInInspector]
    public InputOptions myInputOptions;
    

}
