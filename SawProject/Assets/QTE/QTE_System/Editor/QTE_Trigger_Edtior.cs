/* CUstom Editor for QTE_Trigger.cs*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(QTE_Trigger))]
public class QTE_Trigger_Edtior : Editor
{

    bool MainSettings = true;
    bool InputOptions = true;
    bool TimerOptions = true;
    bool DisplayOptions = true;


    SerializedProperty QTEtype;

    string[] options;

    SerializedProperty[] tps = new SerializedProperty[4];


    void HoldTimerText(QTE_Trigger myTarget, int index)
    {
        int number = index + 1;
        myTarget.HoldActions[index] = EditorGUILayout.Toggle(new GUIContent("Hold Timer " + number, "If true, No On screen Buttons will appear"), myTarget.HoldActions[index]);

        if (myTarget.HoldActions[index])
        {
            myTarget.HoldTimes[index] = EditorGUILayout.FloatField(new GUIContent("\tButton " + number + " Hold Timer:", "The time in seconds the player has to press the button before failing."), myTarget.HoldTimes[index]);

        }
    }

    //funciton for drawing UI for individual button options
    void ButtonText(QTE_Trigger myTarget, int index)
    {
        int number = index + 1;

        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.LabelField("Button " + number, EditorStyles.boldLabel);
        myTarget.UseRandomButtons[index] = EditorGUILayout.Toggle(new GUIContent("\tRandomize", "Check true if you want to use an Randomly Generated button"), myTarget.UseRandomButtons[index]);

        serializedObject.ApplyModifiedProperties();

        if (!myTarget.UseRandomButtons[index])
        {

            if (myTarget.myInputOptions == QTE_BaseClass.InputOptions.UnityInputManager)
            {
                myTarget.ButtonIndexes[index] = EditorGUILayout.Popup("\tButton " + number + " Input To Use", myTarget.ButtonIndexes[index], myTarget.ListOfInputs.ToArray());
                myTarget.ButtonIsAxisCheck[index] = EditorGUILayout.Toggle(new GUIContent("\tButton " + number + " Is Axis", "Off -> Input is a button press On -> Input is an Axis"), myTarget.ButtonIsAxisCheck[index]);

                if (myTarget.ButtonIsAxisCheck[index])
                {
                    EditorGUIUtility.labelWidth = 100;
                    myTarget.ButtonAxisDetection[index] = EditorGUILayout.Popup("Direction", myTarget.ButtonAxisDetection[index], options);
                    myTarget.ButtonAxisThresholds[index] = EditorGUILayout.FloatField("Thresold", myTarget.ButtonAxisThresholds[index]);
                    EditorGUIUtility.labelWidth = 0;
                }
            }
            else
            {
                myTarget.ButtonKeyPresses[index] = EditorGUILayout.TextField(new GUIContent("\tButton " + number + " Keyboard Key"), myTarget.ButtonKeyPresses[index]);
            }
            serializedObject.ApplyModifiedProperties();
        }
        else
        {
            tps[index] = serializedObject.FindProperty("ArrayOfRandomButtons" + number);
            string newLabel = "";

            if (myTarget.myInputOptions == QTE_BaseClass.InputOptions.UnityInputManager)
            {
                newLabel = "\tList of Random Inputs " + number;
            }
            else
            {
                newLabel = "\tList of Random Keys " + number;
            }
            EditorGUILayout.PropertyField(tps[index], new GUIContent(newLabel, "If Array is left at 0, a random button will be chosen from all available buttons. If filled up with names of Keys or Inputs, a random one will be chosen from that list instead."), true);

            EditorGUILayout.Space();
        }
    }

    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        QTE_Trigger myTarget = (QTE_Trigger)target;
        myTarget.ListOfInputs = GetInputAxis();

        options = myTarget.myNewDirections;

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.Space();
        QTEtype = serializedObject.FindProperty("QTEtype");

        MainSettings = EditorGUILayout.Foldout(MainSettings, "Main Settings");

        SerializedProperty EditorEnableButtonFail = serializedObject.FindProperty("EnableButtonFail");

        if (MainSettings)
        {
            EditorGUILayout.BeginVertical("Box");

            myTarget.Canvas = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Overide Canvas:", "The Canvas To Use"), myTarget.Canvas, typeof(GameObject), true);

            EditorGUILayout.PropertyField(QTEtype, new GUIContent("QTE Type:", "'Collider' if attached to a gameobject with a Collider, 'Manual' otherwise"), true);

            if (myTarget.GetComponent<Collider>() != null && myTarget.GetComponent<Collider>().isTrigger)
            {

                EditorGUILayout.LabelField("Mode:", "Using Collider");
                myTarget.Repeatable = EditorGUILayout.Toggle(new GUIContent("Repeatable", "If true, the QTE will always fire every time the player enters the collider, if false, only fires the first time."), myTarget.Repeatable);
                serializedObject.ApplyModifiedProperties();
            }
            else
            {
                EditorGUILayout.LabelField("Mode:", "Manual");
            }
        }


        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
        InputOptions = EditorGUILayout.Foldout(InputOptions, "Input Options");

        if (InputOptions)
        {
            EditorGUILayout.BeginVertical("Box");

            // myTarget.myInputOptions = (myInputOptions)EditorGUILayout.EnumPopup(myTarget.myInputOptions);

            SerializedProperty myInputOptions = serializedObject.FindProperty("myInputOptions");
            EditorGUILayout.PropertyField(myInputOptions, new GUIContent("Input Source", "The type of Input you wisht to use"), true);


            // myTarget.EnableTouch = EditorGUILayout.Toggle(new GUIContent("Mobile Device Input", ""), myTarget.EnableTouch);


                if (QTEtype.enumValueIndex == 4)
                {
                    SerializedProperty Editortolerance = serializedObject.FindProperty("tolerance");
                    EditorGUILayout.PropertyField(Editortolerance, new GUIContent("Mashing Tolerance", "There is a float value that starts with 0, every time the player presses the button it goes up by 0.2, but every frame the value of tolerance is subtracted from it, hence the 'fighting' struggle. Player succeeds when value reaches 1.Increase tolerance to make the amount of button mashing harder to do, less to make it more easy."), true);
                }


                EditorGUILayout.PropertyField(EditorEnableButtonFail, new GUIContent("Allow Wrong Input Failure", "If true, when player Presses a button that's not the correct one, the QTE will fail. If false, the QTE persists until succeeded or times out."), true);

              // myTarget.UseInputs = EditorGUILayout.Toggle(new GUIContent("Use Unity Inputs", "Off -> standard keyboard input is used On -> the script will use Unity's buttons/Axis defined in the Input manager instead."), myTarget.UseInputs);

                
                ButtonText(myTarget, 0);

                if (QTEtype.enumValueIndex == 1 || QTEtype.enumValueIndex == 2 || QTEtype.enumValueIndex == 3)
                {          
                    
                    ButtonText(myTarget, 1);
                }
                if (QTEtype.enumValueIndex == 2 || QTEtype.enumValueIndex == 3)
                {
                   
                    ButtonText(myTarget, 2);
                }
                if (QTEtype.enumValueIndex == 3)
                {
                   
                    ButtonText(myTarget, 3);
                }
            

            EditorGUILayout.EndVertical();
        }


        EditorGUILayout.Space();
        TimerOptions = EditorGUILayout.Foldout(TimerOptions, "Timer Options");
        if (TimerOptions)
        {
            EditorGUILayout.BeginVertical("Box");
            if (QTEtype.enumValueIndex == 4)
            {
                SerializedProperty EditorNoTimer = serializedObject.FindProperty("NoTimer");
                EditorGUILayout.PropertyField(EditorNoTimer, new GUIContent("Disable Timer Fail", "If checked, the QTE will not automatically end after 'Count Down Time' expires, instead the button remains until he succeeds, but if he stops pressing the button after the length of 'Count Down Time' he will fail."), true);
            }



            myTarget.CountDownTimes[0] = EditorGUILayout.FloatField(new GUIContent("Timer:", "The time in seconds the player has to press the button before failing."), myTarget.CountDownTimes[0]);

            if (QTEtype.enumValueIndex == 1 || QTEtype.enumValueIndex == 2 || QTEtype.enumValueIndex == 3)
            {
                SerializedProperty EditorMultiTimer = serializedObject.FindProperty("MultiTimer");
                EditorGUILayout.PropertyField(EditorMultiTimer, new GUIContent("Timer Per Button"), true);
                if (EditorMultiTimer.boolValue)
                {

                    myTarget.CountDownTimes[1] = EditorGUILayout.FloatField(new GUIContent("\tButton 2 Timer:", "The time in seconds the player has to press the button before failing."), myTarget.CountDownTimes[1]);

                    if (QTEtype.enumValueIndex == 2 || QTEtype.enumValueIndex == 3)
                    {

                        myTarget.CountDownTimes[2] = EditorGUILayout.FloatField(new GUIContent("\tButton 3 Timer:", "The time in seconds the player has to press the button before failing."), myTarget.CountDownTimes[2]);


                    }
                    if (QTEtype.enumValueIndex == 3)
                    {
                        myTarget.CountDownTimes[3] = EditorGUILayout.FloatField(new GUIContent("\tButton 4 Timer:", "The time in seconds the player has to press the button before failing."), myTarget.CountDownTimes[3]);

                    }
                }
            }

            SerializedProperty EditorTimerDelayTime = serializedObject.FindProperty("TimerDelayTime");
            EditorGUILayout.PropertyField(EditorTimerDelayTime, new GUIContent("Delay Length:", "The time in seconds to Delay the QTE from happening after it's beein triggered"), true);


            SerializedProperty EditorDelayTime = serializedObject.FindProperty("DelayTime");

            EditorGUILayout.PropertyField(EditorDelayTime, new GUIContent("Input Delay Length:", "The time in seconds a Delay between when the QTE appears, and when Input  starts being detected."), true);

            // SerializedProperty EditorHold = serializedObject.FindProperty("DelayTime");

            //EditorGUILayout.PropertyField(EditorDelayTime, new GUIContent("Input Delay Length:", "The time in seconds a Delay between when the QTE appears, and when Input  starts being detected."), true);

            HoldTimerText(myTarget, 0);

            if (QTEtype.enumValueIndex == 1 || QTEtype.enumValueIndex == 2 || QTEtype.enumValueIndex == 3)
            {
                HoldTimerText(myTarget, 1);
            }

            if (QTEtype.enumValueIndex == 2 || QTEtype.enumValueIndex == 3)
            {
                HoldTimerText(myTarget, 2);
            }

            if (QTEtype.enumValueIndex == 3)
            {
                HoldTimerText(myTarget, 3);
            }


            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.Space();

        DisplayOptions = EditorGUILayout.Foldout(DisplayOptions, "Display Options");
        if (DisplayOptions)
        {
            EditorGUILayout.BeginVertical("Box");
            myTarget.Hidden = EditorGUILayout.Toggle(new GUIContent("Hidden", "If true, No On screen Buttons will appear"), myTarget.Hidden);
            myTarget.FadeInUI = EditorGUILayout.Toggle(new GUIContent("Fade In", "If true, UI buttons will fade in"), myTarget.FadeInUI);

            if (myTarget.FadeInUI)
            {
                myTarget.FadeInTime = EditorGUILayout.FloatField(new GUIContent("Fade In Time", "Time the buttons fade in"), myTarget.FadeInTime);
            }

            if (QTEtype.enumValueIndex == 4)
            {
                myTarget.PulsateSpeed = EditorGUILayout.FloatField(new GUIContent("Pulsating Speed", "How fast the button pulsates in and out, 0 will disable this effect."), myTarget.PulsateSpeed);
                myTarget.PulsateFrequency = EditorGUILayout.FloatField(new GUIContent("Pulsate Frequency:", "The Strength of the Pulsating"), myTarget.PulsateFrequency);
            }

            myTarget.ButtonShaking[0] = EditorGUILayout.Toggle(new GUIContent("Shake 1st Button", "Button shakes back and forth"), myTarget.ButtonShaking[0]);


            if (myTarget.ButtonShaking[0])
            {
                myTarget.ButtonShakeOffests[0] = EditorGUILayout.FloatField(new GUIContent("\tShake Offset"), myTarget.ButtonShakeOffests[0]);
            }

            if (QTEtype.enumValueIndex == 1 || QTEtype.enumValueIndex == 2 || QTEtype.enumValueIndex == 3)
            {
                myTarget.ButtonShaking[1] = EditorGUILayout.Toggle(new GUIContent("Shake 2nd Button", "Button shakes back and forth"), myTarget.ButtonShaking[1]);
                if (myTarget.ButtonShaking[1])
                {
                    myTarget.ButtonShakeOffests[1] = EditorGUILayout.FloatField(new GUIContent("\tShake Offset"), myTarget.ButtonShakeOffests[1]);
                }
            }

            if (QTEtype.enumValueIndex == 2 || QTEtype.enumValueIndex == 3)
            {

                myTarget.ButtonShaking[2] = EditorGUILayout.Toggle(new GUIContent("Shake 3rd Button", "Button shakes back and forth"), myTarget.ButtonShaking[2]);
                if (myTarget.ButtonShaking[2])
                {
                    myTarget.ButtonShakeOffests[2] = EditorGUILayout.FloatField(new GUIContent("\tShake Offset"), myTarget.ButtonShakeOffests[2]);
                }
            }
            if (QTEtype.enumValueIndex == 3)
            {

                myTarget.ButtonShaking[3] = EditorGUILayout.Toggle(new GUIContent("Shake 4th Button", "Button shakes back and forth"), myTarget.ButtonShaking[3]);
                if (myTarget.ButtonShaking[3])
                {


                    myTarget.ButtonShakeOffests[3] = EditorGUILayout.FloatField(new GUIContent("\tShake Offset"), myTarget.ButtonShakeOffests[3]);
                }
            }

            myTarget.OverideButtonPosition = EditorGUILayout.Toggle(new GUIContent("Overide Button Position", "Overide the position of the buttons, buttons will be moved to the new positions"), myTarget.OverideButtonPosition);
            if (myTarget.OverideButtonPosition)
            {

                myTarget.ButtonPositions[0] = EditorGUILayout.Vector3Field(new GUIContent("	Button 1 Position:", "Distance in Units away from the center of the Canvas"), myTarget.ButtonPositions[0]);

                if (QTEtype.enumValueIndex == 1 || QTEtype.enumValueIndex == 2 || QTEtype.enumValueIndex == 3)
                {

                    myTarget.ButtonPositions[1] = EditorGUILayout.Vector3Field(new GUIContent("	Button 2 Position:", "Distance in Units away from the center of the Canvas"), myTarget.ButtonPositions[1]);
                }
                if (QTEtype.enumValueIndex == 2 || QTEtype.enumValueIndex == 3)
                {

                    myTarget.ButtonPositions[2] = EditorGUILayout.Vector3Field(new GUIContent("	Button 3 Position:", "Distance in Units away from the center of the Canvas"), myTarget.ButtonPositions[2]);

                }
                if (QTEtype.enumValueIndex == 3)
                {
                    myTarget.ButtonPositions[3] = EditorGUILayout.Vector3Field(new GUIContent("	Button 4 Position:", "Distance in Units away from the center of the Canvas"), myTarget.ButtonPositions[3]);
                }
            }
            myTarget.RandomizeButtonPositions[0] = EditorGUILayout.Toggle(new GUIContent("Randomize Button 1 Position", "Button position onscreen will be randomized, but will never appear off screen"), myTarget.RandomizeButtonPositions[0]);
            if (QTEtype.enumValueIndex == 1 || QTEtype.enumValueIndex == 2 || QTEtype.enumValueIndex == 3)
            {
                myTarget.RandomizeButtonPositions[1] = EditorGUILayout.Toggle(new GUIContent("Randomize Button 2 Position", ""), myTarget.RandomizeButtonPositions[1]);
            }
            if (QTEtype.enumValueIndex == 2 || QTEtype.enumValueIndex == 3)
            {
                myTarget.RandomizeButtonPositions[2] = EditorGUILayout.Toggle(new GUIContent("Randomize Button 3 Position", ""), myTarget.RandomizeButtonPositions[2]);
            }
            if (QTEtype.enumValueIndex == 3)
            {
                myTarget.RandomizeButtonPositions[3] = EditorGUILayout.Toggle(new GUIContent("Randomize Button 4 Position", ""), myTarget.RandomizeButtonPositions[3]);
            }
            if (myTarget.RandomizeButtonPositions[0] || myTarget.RandomizeButtonPositions[1] || myTarget.RandomizeButtonPositions[2] || myTarget.RandomizeButtonPositions[3])
            {
                myTarget.CanvasPadding = EditorGUILayout.Vector2Field(new GUIContent("Canvas Padding", ""), myTarget.CanvasPadding);
            }
            EditorGUILayout.EndVertical();
        }

        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(myTarget);
    }


    public static List<string> GetInputAxis()
    {
        var allAxis = new List<string>();
        var serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
        var axesProperty = serializedObject.FindProperty("m_Axes");
        //var NegProperty = negativeButton
        axesProperty.Next(true);
        axesProperty.Next(true);
        while (axesProperty.Next(false))
        {
            SerializedProperty axis = axesProperty.Copy();
            axis.Next(true);
            allAxis.Add(axis.stringValue);
        }
        return allAxis;
    }

}