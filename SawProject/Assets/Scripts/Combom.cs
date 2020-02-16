using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combom : MonoBehaviour
{

    public bool timeup = true;

    [SerializeField]
    GameObject Saws;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (GameManager.Instance.GameBegin)
        {
            if (timeup)
            {
                StartCoroutine(WaitAndGenerate(3.5f));
                timeup = false;
            }
        }
        
    }

    private IEnumerator WaitAndGenerate(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject.Instantiate(Saws);
        timeup = true;
    }
}
