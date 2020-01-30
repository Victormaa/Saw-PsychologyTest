using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonTrigger : MonoBehaviour
{
    private Image image;

    private Button button;

    void Init()
    {
        
    }

    private IEnumerator coroutine;

    bool isRunning = false;

    GameObject ThePlayer;

    private void Awake()
    {
        coroutine = WaitAndPrint(2.0f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print(isRunning);
        if(!isRunning)
            StartCoroutine(WaitAndPrint(2.0f));

        ThePlayer = collision.gameObject;
        print(ThePlayer.name);
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        isRunning = true;
        yield return new WaitForSeconds(waitTime);
        Debug.Log("before unlock locknum: " + ThePlayer.GetComponent<PlayerController>()._locknum);
        print("Chain is unlocked");
        ThePlayer.GetComponent<PlayerController>().runSpeed = 5;
        ThePlayer.GetComponent<PlayerController>().unlock();
        Debug.Log("after unlock locknum: " + ThePlayer.GetComponent<PlayerController>()._locknum);
    }
}
