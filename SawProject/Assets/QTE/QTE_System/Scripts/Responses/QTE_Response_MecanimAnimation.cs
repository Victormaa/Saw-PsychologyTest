/*This response script changes values of a Mecanim animator component, causing different animations to play based upon the considtions setup in the animator*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QTE_Response_MecanimAnimation : MonoBehaviour
{

    public GameObject ObjectToAnimate;
    private Animator animator;

    [System.Serializable]
    public class MyClass
    {
        public string type;
        public string name;
        public float myFloat;
        public bool myBool;
        public int myInt;
    }
    public bool activeGroup;
    public bool sucessGroup1;
    public bool sucessGroup2;
    public bool sucessGroup3;
    public bool sucessGroup4;
    public bool failGroup;

    public List<MyClass> ListOfAnimValues = new List<MyClass>();
    public List<string> ListOfParameterNames = new List<string>();
    public List<MyClass> ActiveParameters = new List<MyClass>();
    public List<int> ActiveIndex = new List<int>();
    public List<MyClass> Sucess1Parameters = new List<MyClass>();
    public List<int> Sucess1Index = new List<int>();
    public List<MyClass> Sucess2Parameters = new List<MyClass>();
    public List<int> Sucess2Index = new List<int>();
    public List<MyClass> Sucess3Parameters = new List<MyClass>();
    public List<int> Sucess3Index = new List<int>();
    public List<MyClass> Sucess4Parameters = new List<MyClass>();
    public List<int> Sucess4Index = new List<int>();
    public List<MyClass> FailParameters = new List<MyClass>();
    public List<int> FailIndex = new List<int>();

    // Use this for initialization
    void Start()
    {

        if (ObjectToAnimate == null)
        {
            animator = GetComponent<Animator>();
        }
        else
        {
            animator = ObjectToAnimate.GetComponent<Animator>();
        }
    }

    void SetParameters(List<MyClass> List)
    {

        if (List.Count > 0)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].type == "float")
                {
                    animator.SetFloat(List[i].name, List[i].myFloat);
                }
                if (List[i].type == "int")
                {
                    animator.SetInteger(List[i].name, List[i].myInt);
                }
                if (List[i].type == "bool")
                {
                    animator.SetBool(List[i].name, List[i].myBool);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (QTE_main.Singleton.TriggeringObject == this.gameObject)
        {

            //while the QTE is happening
            if (QTE_main.Singleton.QTEactive)
            {
                SetParameters(ActiveParameters);
            }

            if (QTE_main.Singleton.QTECompleted && !QTE_main.Singleton.QTEactive)
            {
                // Debug.Log(Sucess1Parameters[0]);
                SetParameters(Sucess1Parameters);
            }

            //if the QTE completed, and he succedded with option 2 (Dual QTE only)
            if (QTE_main.Singleton.succeeded2)
            {
                SetParameters(Sucess2Parameters);
            }

            //if the QTE completed, and he succedded with option 3 (Tri QTE only)
            if (QTE_main.Singleton.succeeded3)
            {
                SetParameters(Sucess3Parameters);
            }

            //if the QTE completed, and he succedded with option 4 (Quad QTE only)
            if (QTE_main.Singleton.succeeded4)
            {
                SetParameters(Sucess4Parameters);
            }

            //if the QTE completed, and he failed
            if (QTE_main.Singleton.QTE_Failed_WrongButton || QTE_main.Singleton.QTE_Failed_timer)
            {
                SetParameters(FailParameters);
            }
        }
    }

}



