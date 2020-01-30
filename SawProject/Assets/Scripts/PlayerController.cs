using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Character parametre

    [SerializeField]
    private CharactorParametre Parametre;

    public float runSpeed = 2.0f;
    
    private float moveInput;

    private Rigidbody2D myrigidbody;

    private bool facingRight = true;
    #endregion

    //********* For Charactor

    [SerializeField]
    private bool IsPlayerA = true;

    public bool isdead = false;

    private static int LockNum = 2;

    public int _locknum => LockNum;   

    private void Awake()
    {
        myrigidbody = GetComponent<Rigidbody2D>();

        LockNum = 2;
    }

    public delegate void Unlock();

    public Unlock unlockevent;

    public int unlock()
    {
        return LockNum--;
    }

    int damagerate = 0;

    public float Damage()
    {
        damagerate++;

        if(damagerate > 20)
        {
            Parametre.HealthPoint -= 1;
            damagerate = 0;
        }

        return Parametre.HealthPoint;
    }

    // Update is called once per frame
    void Update()
    {       
        if(_locknum == 0)
        {
            if (this.gameObject.GetComponent<DistanceJoint2D>())
            {
                this.gameObject.GetComponent<DistanceJoint2D>().enabled = false;
            }
            if (this.gameObject.GetComponent<FixedJoint2D>())
            {
                this.gameObject.GetComponent<FixedJoint2D>().enabled = false;
            }

            
            if(unlockevent != null)
            {
                unlockevent();
            }
        }
    }

    private void FixedUpdate()
    {
        if(!isdead)
            if (IsPlayerA)
            {
                moveInput = Input.GetAxis("HorizontalAD");
                myrigidbody.velocity = new Vector2(moveInput * runSpeed, myrigidbody.velocity.y);
            }
            else
            {
                moveInput = Input.GetAxis("HorizontalLR");
                myrigidbody.velocity = new Vector2(moveInput * runSpeed, myrigidbody.velocity.y);
            }

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }else if(facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }
    private void Flip()
    {
        /*
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        */
    }

    

}
