using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToLvl2 : MonoBehaviour {
	public GameObject door;
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
			if(door.GetComponent<SpriteRenderer>().sprite.name == "Door24")
			{
				SceneManager.LoadScene ("Scene_2");
				GameObject.Find ("GameManager").GetComponent<GameManager> ().level++;
			}
        }
    }
}
