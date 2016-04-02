using UnityEngine;
using System.Collections;

public class nextLevel : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButtonDown("X button"))
        {
            Application.LoadLevel("Scene_1");
        }
    }
}
