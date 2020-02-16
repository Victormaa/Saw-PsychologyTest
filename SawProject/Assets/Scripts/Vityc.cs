using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vityc : MonoBehaviour
{
    [SerializeField]
    Animator animation;

    bool trigger = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetFade()
    {
        if (trigger)
        {
            animation.SetBool("FadeOut", true);
            trigger = false;
        }
        else
        {
            animation.SetBool("FadeIn", true);
            trigger = true;
        }
        
    }
}
