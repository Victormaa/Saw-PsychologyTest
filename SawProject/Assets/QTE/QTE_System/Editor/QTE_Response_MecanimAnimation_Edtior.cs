/* CUstom Editor for QTE_Trigger.cs*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(QTE_Response_MecanimAnimation))]
public class QTE_Response_MecanimAnimation_Edtior : Editor
{
    //public QTE_Response_MecanimAnimation.MyClass TempClass;
    QTE_Response_MecanimAnimation.MyClass TempClass;
    //SerializedProperty QTEtype;
    private Animator animator;
    AnimatorControllerParameter param;
    QTE_Response_MecanimAnimation myTarget;

    SerializedProperty QTEtype;

    void OnEnable()
    {
        myTarget = (QTE_Response_MecanimAnimation)target;
        animator = myTarget.ObjectToAnimate.GetComponent<Animator>();
    }

    void GetAnimatorParameters()
    {
        //clear out any values that where there before.
        myTarget.ListOfAnimValues.Clear();
        myTarget.ListOfParameterNames.Clear();

        //loop though all the parameters in the animator
        for (int i = 0; i < animator.parameters.Length; i++)
        {
            param = animator.parameters[i];

            //find out what kind of parameter it is, and create a new entry in the ListOfAnimValues list to store them all.
            if (param.type == AnimatorControllerParameterType.Bool)
            {
                // Debug.Log(param.name);
                TempClass = new QTE_Response_MecanimAnimation.MyClass();
                TempClass.type = "bool";
                TempClass.name = param.name;
                TempClass.myBool = param.defaultBool;
                myTarget.ListOfAnimValues.Add(TempClass);
            }
            else if (param.type == AnimatorControllerParameterType.Float)
            {
                TempClass = new QTE_Response_MecanimAnimation.MyClass();
                TempClass.type = "float";
                TempClass.name = param.name;
                TempClass.myFloat = param.defaultFloat;
                myTarget.ListOfAnimValues.Add(TempClass);

            }
            else if (param.type == AnimatorControllerParameterType.Int)
            {
                TempClass = new QTE_Response_MecanimAnimation.MyClass();
                TempClass.type = "int";
                TempClass.name = param.name;
                TempClass.myInt = param.defaultInt;
                myTarget.ListOfAnimValues.Add(TempClass);
            }


        }

        //Build a list of just the names of all the pameters.
        for (int i = 0; i < myTarget.ListOfAnimValues.Count; i++)
        {
            myTarget.ListOfParameterNames.Add(myTarget.ListOfAnimValues[i].name);
        }

        // Debug.Log(myTarget.ListOfAnimValues.Count);
    }


    //function that draws the UI for each paramter to be changed.
    void ParameterUI(List<QTE_Response_MecanimAnimation.MyClass> ListOfParameters, List<int> ListIndex)
    {
        for (int i = 0; i < ListOfParameters.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            //Create a popup to select from the list of all parameters by name, the option is stored in a seperate int list
            ListIndex[i] = EditorGUILayout.Popup(ListIndex[i], myTarget.ListOfParameterNames.ToArray());

            //loop through all the avalible parameters and find what option they chose
            for (int a = 0; a < myTarget.ListOfAnimValues.Count; a++)
            {
                //find the parameter by name
                if (myTarget.ListOfAnimValues[a].name == myTarget.ListOfParameterNames.ToArray()[ListIndex[i]])
                {
                    //store the name and type they chose
                    ListOfParameters[i].type = myTarget.ListOfAnimValues[a].type;
                    ListOfParameters[i].name = myTarget.ListOfAnimValues[a].name;

                    //create appropriate fields based upon what data type it is.
                    if (ListOfParameters[i].type == "float")
                    {
                        ListOfParameters[i].myFloat = EditorGUILayout.FloatField(": ", ListOfParameters[i].myFloat);
                    }
                    if (ListOfParameters[i].type == "int")
                    {
                        ListOfParameters[i].myInt = EditorGUILayout.IntField(": ", ListOfParameters[i].myInt);
                    }
                    if (ListOfParameters[i].type == "bool")
                    {
                        ListOfParameters[i].myBool = EditorGUILayout.Toggle(": ", ListOfParameters[i].myBool);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        //DrawDefaultInspector();
        EditorGUILayout.Space();

        serializedObject.Update();
        myTarget.ObjectToAnimate = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Object To Animate", "The gameObject with attached Animator"), myTarget.ObjectToAnimate, typeof(GameObject), true);
        EditorGUILayout.Space();

        if (GUILayout.Button("Get All Parameters"))
        {
            GetAnimatorParameters();
        }
        if (GUILayout.Button("Clear"))
        {
            myTarget.ListOfAnimValues.Clear();
            myTarget.Sucess1Parameters.Clear();
            myTarget.Sucess1Index.Clear();
            myTarget.Sucess2Parameters.Clear();
            myTarget.Sucess2Index.Clear();
            myTarget.Sucess3Parameters.Clear();
            myTarget.Sucess3Index.Clear();
            myTarget.Sucess4Parameters.Clear();
            myTarget.Sucess4Index.Clear();
            myTarget.FailParameters.Clear();
            myTarget.FailIndex.Clear();
        }

        if (myTarget.ListOfAnimValues.Count > 0)
        {
            EditorGUILayout.Space();

            myTarget.activeGroup = EditorGUILayout.Foldout(myTarget.activeGroup, "Active: ");
            if (myTarget.activeGroup)
            {
                ParameterUI(myTarget.ActiveParameters, myTarget.ActiveIndex);

                if (GUILayout.Button("Add Value"))
                {
                    myTarget.ActiveParameters.Add(new QTE_Response_MecanimAnimation.MyClass());
                    myTarget.ActiveIndex.Add(new int());
                }
                if (GUILayout.Button("Clear"))
                {
                    myTarget.ActiveParameters.Clear();
                    myTarget.ActiveIndex.Clear();
                }
            }
            myTarget.sucessGroup1 = EditorGUILayout.Foldout(myTarget.sucessGroup1, "Success 1: ");
            if (myTarget.sucessGroup1)
            {
                ParameterUI(myTarget.Sucess1Parameters, myTarget.Sucess1Index);

                if (GUILayout.Button("Add Value"))
                {
                    myTarget.Sucess1Parameters.Add(new QTE_Response_MecanimAnimation.MyClass());
                    myTarget.Sucess1Index.Add(new int());
                }
                if (GUILayout.Button("Clear"))
                {
                    myTarget.Sucess1Parameters.Clear();
                    myTarget.Sucess1Index.Clear();
                }
            }
            myTarget.sucessGroup2 = EditorGUILayout.Foldout(myTarget.sucessGroup2, "Success 2: ");
            if (myTarget.sucessGroup2)
            {
                ParameterUI(myTarget.Sucess2Parameters, myTarget.Sucess2Index);

                if (GUILayout.Button("Add Value"))
                {
                    myTarget.Sucess2Parameters.Add(new QTE_Response_MecanimAnimation.MyClass());
                    myTarget.Sucess2Index.Add(new int());
                }
                if (GUILayout.Button("Clear"))
                {
                    myTarget.Sucess2Parameters.Clear();
                    myTarget.Sucess2Index.Clear();
                }
            }
            myTarget.sucessGroup3 = EditorGUILayout.Foldout(myTarget.sucessGroup3, "Success 3: ");
            if (myTarget.sucessGroup3)
            {
                ParameterUI(myTarget.Sucess3Parameters, myTarget.Sucess3Index);

                if (GUILayout.Button("Add Value"))
                {
                    myTarget.Sucess3Parameters.Add(new QTE_Response_MecanimAnimation.MyClass());
                    myTarget.Sucess3Index.Add(new int());
                }
                if (GUILayout.Button("Clear"))
                {
                    myTarget.Sucess3Parameters.Clear();
                    myTarget.Sucess3Index.Clear();
                }
            }
            myTarget.sucessGroup4 = EditorGUILayout.Foldout(myTarget.sucessGroup4, "Success 4: ");
            if (myTarget.sucessGroup4)
            {
                ParameterUI(myTarget.Sucess4Parameters, myTarget.Sucess4Index);

                if (GUILayout.Button("Add Value"))
                {
                    myTarget.Sucess4Parameters.Add(new QTE_Response_MecanimAnimation.MyClass());
                    myTarget.Sucess4Index.Add(new int());
                }
                if (GUILayout.Button("Clear"))
                {
                    myTarget.Sucess4Parameters.Clear();
                    myTarget.Sucess4Index.Clear();
                }
            }
            myTarget.failGroup = EditorGUILayout.Foldout(myTarget.failGroup, "Fail: ");
            if (myTarget.failGroup)
            {
                ParameterUI(myTarget.FailParameters, myTarget.FailIndex);

                if (GUILayout.Button("Add Value"))
                {
                    myTarget.FailParameters.Add(new QTE_Response_MecanimAnimation.MyClass());
                    myTarget.FailIndex.Add(new int());
                }
                if (GUILayout.Button("Clear"))
                {
                    myTarget.FailParameters.Clear();
                    myTarget.FailIndex.Clear();
                }
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Press get all Parameters", MessageType.Error);
        }

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(myTarget);
        }
    }


}