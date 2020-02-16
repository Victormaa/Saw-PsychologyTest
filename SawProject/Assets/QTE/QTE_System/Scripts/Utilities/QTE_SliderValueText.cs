using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE_SliderValueText : MonoBehaviour
{

    public string BaseLabel = "Number of Buttons: ";
    public Slider mySlider;
    private Text myText;

    public List<QTE_Trigger> ListOfQTEs = new List<QTE_Trigger>();

    // Use this for initialization
    void Start()
    {
        myText = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        myText.text = BaseLabel + mySlider.value;

    }

    public void SetValue()
    {
        foreach (QTE_Trigger t in ListOfQTEs)
        {
            if (mySlider.value == 1)
            {
                t.QTEtype = QTE_Trigger.OPTIONS.Single;
            }
            else if (mySlider.value == 2)
            {
                t.QTEtype = QTE_Trigger.OPTIONS.Dual;
            }
            else if (mySlider.value == 3)
            {
                t.QTEtype = QTE_Trigger.OPTIONS.Tri;
            }
            else
            {
                t.QTEtype = QTE_Trigger.OPTIONS.Quad;
            }

        }
    }
}