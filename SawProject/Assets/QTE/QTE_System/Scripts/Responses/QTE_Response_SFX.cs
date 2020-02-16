/* This is a template response script, for you to write your own responses to QTEs, right now the script does nothing, fill in the sections commneted below with your own code*/

using UnityEngine;
using System.Collections;

public class QTE_Response_SFX : MonoBehaviour
{

    //the sound effect to play
    public AudioSource Source;
    public AudioClip SoundFX;
    //if Repeatable is true plays every time the QTE is triggered, false only plays the first time.
    public bool Repeatable;
    private bool HasBeenTriggered;

    //when the sfx should play
    public bool DuringActive;
    public bool OnCompleted1;
    public bool OnCompleted2;
    public bool OnCompleted3;
    public bool OnCompleted4;
    public bool OnFail;

    // Use this for initialization
    void Start()
    {

    }

    //function that actually plays the SFX
    void PlaySFX()
    {
        //check to see if the SFX has already been trigged.
        if (!HasBeenTriggered)
        {
            if(Source == null)
            {
                this.gameObject.GetComponent< AudioSource>().PlayOneShot(SoundFX);
            }

            //if it hasn't play it
           // this.gameObject.GetComponent<AUdio.PlayOneShot(SoundFX);

            //if the sound effect is not repeatable, set HasBeenTriggered to true so the sound effect will never play again.
            if (!Repeatable)
            {
                HasBeenTriggered = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (QTE_main.Singleton.TriggeringObject == this.gameObject)
        {


            if (QTE_main.Singleton.QTEactive && DuringActive)
            {
                //while the QTE is happening
                PlaySFX();
            }


            if (QTE_main.Singleton.QTECompleted && !QTE_main.Singleton.QTEactive)
            {

                if (QTE_main.Singleton.succeeded && OnCompleted1)
                {
                    //if the QTE completed, and he succedded with option 1
                    PlaySFX();
                }


                if (QTE_main.Singleton.succeeded2 && OnCompleted2)
                {
                    //if the QTE completed, and he succedded with option 2 (Dual QTE only)
                    PlaySFX();
                }


                if (QTE_main.Singleton.succeeded3 && OnCompleted3)
                {
                    //if the QTE completed, and he succedded with option 3 (Tri QTE only)
                    PlaySFX();
                }


                if (QTE_main.Singleton.succeeded4 && OnCompleted4)
                {
                    //if the QTE completed, and he succedded with option 4 (Quad QTE only)
                    PlaySFX();
                }

                if (QTE_main.Singleton.QTE_Failed_WrongButton && OnFail)
                {
                    //If failed due to pressing the wrong button
                    PlaySFX();
                }

                if (QTE_main.Singleton.QTE_Failed_timer && OnFail)
                {
                    //if failed due to the timer finishing.
                    PlaySFX();
                }

            }
        }

    }
}
