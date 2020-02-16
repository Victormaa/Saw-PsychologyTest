using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player1") && !PlayerController.IronLock && !collision.GetComponent<PlayerController>().isdead)
        {
            GameManager.Instance.EndScene();
        }
        if (collision.CompareTag("Player2") && !PlayerController.IronLock && !collision.GetComponent<PlayerController>().isdead)
        {
            GameManager.Instance.EndScene();
        }
    }
}
