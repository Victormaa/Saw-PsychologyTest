using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    int ExtendedRate = 0;
    Vector3 v = new Vector3(0, 0.2f, 0);
    // Update is called once per frame
    void Update()
    {
        ExtendedRate++;
        if (ExtendedRate > 100)
        {
            this.gameObject.transform.localScale += v;
            ExtendedRate = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.NeverSleep;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().Damage();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.StartAwake;
        }
    }

}
