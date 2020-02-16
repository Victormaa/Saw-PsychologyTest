using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronChange : MonoBehaviour
{
    private bool _APlayer = false;

    [SerializeField]
    GameObject Alocker;

    [SerializeField]
    GameObject Blocker;

    [SerializeField]
    private float _TimetoFall = 1.5f;

    private float TimeToappear = 0;

    private float timepassed = 0;

    Rigidbody2D myrig;

    Collider2D mycollider;

    float LockY = 0;

    private void Start()
    {
        mycollider = this.GetComponent<Collider2D>();

        myrig = this.GetComponent<Rigidbody2D>();

        FindObjectOfType<PlayerController>().unlockevent += onPlayerUnlocked;

        LockY = Alocker.transform.position.y;
    }

    private void Update()
    {
        if (timepassed < TimeToappear && _APlayer)
        {
            timepassed += Time.deltaTime;
            TurnLightOn(timepassed, TimeToappear,Alocker);
        }else if(timepassed < TimeToappear && !_APlayer)
        {
            timepassed += Time.deltaTime;
            TurnLightOn(timepassed, TimeToappear, Blocker);
        }
    }

    void onPlayerUnlocked()
    {
        
        mycollider.isTrigger = false;
        myrig.mass = 3;
        myrig.gravityScale = 0.3f;
        FindObjectOfType<PlayerController>().unlockevent -= onPlayerUnlocked;
    }

    public void LockerEvent(bool APlayer)
    {
        if (APlayer)
        {
            Alocker.transform.SetParent(GameManager.Instance.background.transform);

            TimeToappear = _TimetoFall;
            timepassed = 0;
            _APlayer = APlayer;
            //Alocker.transform.position = Alocker.transform.position - new Vector3(0, 0.5f, 0);
        }
        else
        {
            Blocker.transform.SetParent(GameManager.Instance.background.transform);

            TimeToappear = _TimetoFall;
            timepassed = 0;
            _APlayer = APlayer;
            //Blocker.transform.position = Blocker.transform.position - new Vector3(0, 0.5f, 0);
        }    
    }

    void TurnLightOn(float Timer, float FinishedTime, GameObject Thelock)
    {
        var a = Thelock.transform.position;
        
        Thelock.transform.position = new Vector3(a.x, Mathf.Lerp(a.y, (LockY - 0.8f), Timer / FinishedTime), a.z);
    }
}
