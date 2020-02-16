using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCheck : MonoBehaviour
{
    public GameObject Left;
    public GameObject Right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Left.GetComponent<PlayerController>().isdead && Right.GetComponent<PlayerController>().isdead) SceneManager.LoadScene("2Dead");
    }
}
