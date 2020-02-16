using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _BGM;
    [SerializeField]
    private AudioSource _AEha;
    public AudioSource getAEha()
    {
        return _AEha;
    }
    [SerializeField]
    private AudioSource _BEha;
    public AudioSource getBEha()
    {
        return _BEha;
    }

    public GameObject Iron;

    public GameObject background;

    public static GameManager Instance;

    public PlayerController player1;

    public PlayerController player2;

    public GameObject EnterStart;

    public GameObject TwoDead;

    public GameObject TwoWin;

    public GameObject AWin;

    public GameObject BWin;

    public static int AliveNum = 2;

    public bool GameBegin = false;
    [SerializeField]
    float StartTime = 0;

    [SerializeField]
    Text TimerText;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        AliveNum = 2;
        t = 0;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float t = 0;

    // Update is called once per frame
    void Update()
    {
        if (GameBegin)
        {
            t += Time.deltaTime;
            string Minute = ((int)t / 60).ToString();
            string Second = (t % 60).ToString("f0");
            TimerText.text = Minute + ":" + Second;
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _AEha.Play();
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _BEha.Play();
        }

        


        if (Input.GetKeyDown(KeyCode.Return))
        {
            
            GameBegin = true;
            
            EnterStart.SetActive(false);
        }
        

        if(player1.isdead && player2.isdead)
        {
            if (GameBegin)
            {
                EndScene();
            }
        }
    }

    public void EndScene()
    {
        _BGM.Stop();
        if (player1.isdead && player2.isdead)
        {
            TwoDead.SetActive(true);
            GameBegin = false;
        }
        if (player1.isdead && !player2.isdead)
        {
            BWin.SetActive(true);
            GameBegin = false;
        }
        if (!player1.isdead && player2.isdead)
        {
            AWin.SetActive(true);
            GameBegin = false;
        }
        if (!player1.isdead && !player2.isdead)
        {
            TwoWin.SetActive(true);
            GameBegin = false;
        }
    }

    public void Replay_1()
    {
        SceneManager.LoadScene(0);
    }
}
