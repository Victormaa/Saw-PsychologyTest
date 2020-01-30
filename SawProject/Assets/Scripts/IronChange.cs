using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronChange : MonoBehaviour
{

    Rigidbody2D myrig;

    Collider2D mycollider;

    private void Start()
    {
        mycollider = this.GetComponent<Collider2D>();

        myrig = this.GetComponent<Rigidbody2D>();

        FindObjectOfType<PlayerController>().unlockevent += onPlayerUnlocked;
    }

    void onPlayerUnlocked()
    {
        Debug.Log("this is in iron");
        mycollider.isTrigger = false;
        myrig.mass = 3;
        myrig.gravityScale = 0.3f;
        FindObjectOfType<PlayerController>().unlockevent -= onPlayerUnlocked;
    }
}
