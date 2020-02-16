using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField]
    CharactorParametre A;
    [SerializeField]
    CharactorParametre B;
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
    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (collision.tag == "Player1" && A.HealthPoint > 0)
        {
            collision.GetComponent<PlayerController>().Damage();
        }
        if (collision.tag == "Player2" && B.HealthPoint > 0)
        {
            collision.GetComponent<PlayerController>().Damage();
        }
    }
}
