using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject FootLock;

    public Transform Adefaultpos;
    public Transform Bdefaultpos;
    public Transform Mashring;
    public Transform Mashcircle;

    public bool Amovable = true;
    public bool Bmovable = true;

    private Animator PlayerAnimator;

    #region Character parametre

    [SerializeField]
    private CharactorParametre Parametre;

    public float runSpeed = 2.0f;
    
    private float moveInput;

    private Rigidbody2D myrigidbody;

    private bool facingRight = true;
    #endregion

// these bool is about dead which could be use to judge about the success and also should dead happens.
    [SerializeField]
    private bool IsPlayerA = true;
    public bool isdead = false;
    private static bool Onedead = false;

    private static int LockNum = 2;

    public int _locknum => LockNum;   

    private void Awake()
    {
        myrigidbody = GetComponent<Rigidbody2D>();

        LockNum = 2;

        Parametre.MoveSpeed = 2;

        isfootlock = true;
        IronLock = true;
        Onedead = false;

        PlayerAnimator = gameObject.GetComponent<Animator>();
    }

    public delegate void Unlock();

    public Unlock unlockevent;

    public Unlock OneguyDie;

    public int unlock()
    {
        return LockNum--;
    }

    public bool Amoved = false;
    public bool Bmoved = false;

    //value for checking about the time to execute action
    float damagerate = 0;
    float ChangeSpeedRate = 0;

    // this two lock for checking is player got a footlock or ironlock
    public bool isfootlock = true;
    [SerializeField]
    public static bool IronLock = true;

    public float Damage()
    {
        bool a = false;
        damagerate += Time.deltaTime;

        //if(damagerate > 0.5f)
            
        if (damagerate > 0.7f)
        {
            a = true;
            PlayerAnimator.SetBool("Hurt", true);
            this.GetComponent<SpriteRenderer>().color = new Color(1, 100.0f / 255.5f, 100.0f / 255.0f);
            Parametre.HealthPoint -= 2.5f;

            if (IsPlayerA)
            {
                GameManager.Instance.getAEha().Play();
                
            }
            else
            {
                GameManager.Instance.getBEha().Play();
            }
            
            if (a)
            {
                Invoke("StopDamageEffect", 0.4f);
                a = false;
            }
            
            damagerate = 0.0f;
        }
        return Parametre.HealthPoint;
    }

    void StopDamageEffect()
    {      
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            PlayerAnimator.SetBool("Hurt", false);       
    }

    // Update is called once per frame
    void Update()
    {       
        if(_locknum == 0)
        {
            if (this.gameObject.GetComponent<DistanceJoint2D>()&& this.gameObject.GetComponent<FixedJoint2D>())
            {
                unlockevent += UnlockIron;
            }           
        }

        if (unlockevent != null)
        {
            unlockevent();
        }

        if(OneguyDie != null)
        {
            OneguyDie();
        }

        if (Parametre.HealthPoint <= 0)
        {
            Parametre.HealthPoint = 0;
            isdead = true;
            PlayerAnimator.SetBool("Isdead", true);
            unlockevent += UnlockIron;
            OneguyDie += DeadHappen;
        }

        if (60 < Parametre.HealthPoint && Parametre.HealthPoint <= 80)
        {
            //damage shape
            //Debug.Log(this.name + "damage");
        }
        else if (30 < Parametre.HealthPoint && Parametre.HealthPoint <= 60)
        {
            //damage more
            //Debug.Log(this.name + "damage more");
        }
        else if (10 < Parametre.HealthPoint && Parametre.HealthPoint <= 30)
        {
            //dying
            //Debug.Log(this.name + "dying");
        }
        else if (10 > Parametre.HealthPoint && Parametre.HealthPoint > 0)
        {
            //Debug.Log(this.name + "unmovable");
        }

        
        if (isfootlock)
        {
            ChangeSpeedRate += Time.deltaTime;
            if (ChangeSpeedRate >= 0.5f)
            {
                float RandomSpeed = UnityEngine.Random.Range(1.0f, 3.0f);

                this.Parametre.MoveSpeed = RandomSpeed;

                ChangeSpeedRate = 0;
            }
        }
        else
        {
            this.Parametre.MoveSpeed = 5.0f;
        }
       
        

        //if (Amoved || Bmoved)
        //{
        //QTE_main.Singleton.InteruptQTE();
        //}

        if (!Amovable)
        {
            // show something about player could not move
        }
        else
        {
            //cancel
        }
        if (!Bmovable)
        {
            // show something about player could not move
        }
        else
        {
            //cancel
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GameBegin)
        {
            if (isdead)
                return;

            if (IsPlayerA)
            {
                if (Amovable)
                {
                    moveInput = Input.GetAxis("HorizontalAD");
                    if (moveInput > 0.1f || moveInput < -.1f)
                    {
                        Amoved = true;
                    }
                    else
                    {
                        Amoved = false;
                    }
                    myrigidbody.velocity = new Vector2(moveInput * this.Parametre.MoveSpeed, myrigidbody.velocity.y);
                }
                else
                {
                    Amoved = false;
                }
            }
            else
            {
                if (Bmovable)
                {
                    moveInput = Input.GetAxis("HorizontalLR");
                    if (moveInput > 0.1f || moveInput < -.1f)
                    {
                        Bmoved = true;
                    }
                    else
                    {
                        Bmoved = false;
                    }
                    myrigidbody.velocity = new Vector2(moveInput * this.Parametre.MoveSpeed, myrigidbody.velocity.y);
                }
                else
                {
                    Bmoved = false;
                }

            }
        }
       
    }

    public void PlayerUnlocked()
    {
        isfootlock = false;
        this.FootLock.transform.SetParent(GameManager.Instance.background.transform);
        GameManager.Instance.Iron.GetComponent<IronChange>().LockerEvent(IsPlayerA);
        unlock();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Saw" && Parametre.HealthPoint > 0)
        {
            Damage();
        }
    }

// only this event happens could some one get out of the room
    private void UnlockIron()
    {
        if (IronLock)
        {
            GameManager.Instance.player1.GetComponent<DistanceJoint2D>().enabled = false;
            GameManager.Instance.player1.GetComponent<FixedJoint2D>().enabled = false;     
            QTE_main.Singleton.InteruptQTE();
            unlockevent -= UnlockIron;

            //in case this function be called second time 
            IronLock = false;
        }
    }

    private void DeadHappen()
    {
        if (!Onedead)
        {
            if (this.CompareTag("Player1"))
            {

                GameManager.Instance.player2.PlayerUnlocked();
                GameManager.Instance.player2.Bmovable = true;
                Amovable = false;
            }
            if (this.CompareTag("Player2"))
            {
                Debug.Log(this.name);

                GameManager.Instance.player1.PlayerUnlocked();
                GameManager.Instance.player1.Amovable = true;
                Bmovable = false;
            }

            QTE_main.Singleton.InteruptQTE();
            Mashring.gameObject.SetActive(false);
            Mashcircle.gameObject.SetActive(false);

            Destroy(FindObjectsOfType<QTE_Trigger>()[0]);
            Destroy(FindObjectsOfType<QTE_Trigger>()[1]);
            GameManager.AliveNum -= 1;
            OneguyDie -= DeadHappen;

            UnlockIron();
            //in case this function be called second time; 
            Onedead = true;
        }
        OneguyDie -= DeadHappen;
    }

}
