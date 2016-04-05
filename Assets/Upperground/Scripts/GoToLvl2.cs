using UnityEngine;
using System.Collections;

public class GoToLvl2 : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

               // GetComponentInChildren<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
           // GetComponentInChildren<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButtonDown("X button"))
        {
            Application.LoadLevel("Scene_1");
        }
    }
}
