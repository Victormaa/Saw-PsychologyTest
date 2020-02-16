using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Replay : MonoBehaviour
{
    [SerializeField]
    private float _Time = 1.5f;

    private float TimeToappear = 0;

    private float timepassed = 0;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        TimeToappear = _Time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.Instance.Replay_1();
        }


        if(timepassed < TimeToappear)
        {
            timepassed += Time.deltaTime;
            TurnLightOn(timepassed, TimeToappear);
        }
        
    }

    public void TurnLightOn(float Timer, float FinishedTime)
    {
        this.GetComponentInChildren<Image>().color = new Color(1, 1, 1,Mathf.Lerp(0,(240.0f/255.0f),Timer/FinishedTime));
        //this.GetComponent<SpriteRenderer>().color = new Color(188.0f / 255, 68.0f / 255, 48.0f / 255, Mathf.Lerp(0.2f, 1, Timer / FinishedTime));
    }
}
