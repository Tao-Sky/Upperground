using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gotoEnd : MonoBehaviour {
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
        if (Input.GetButtonDown("X button") && other.gameObject.tag == "Player")
        {
            //if (door.GetComponent<SpriteRenderer>().sprite.name == "Door24")
            //{
			if(GameObject.Find("GameManager").GetComponent<GameManager>().level == 2)
			{
				SceneManager.LoadScene("Scene_3");
				GameObject.Find ("GameManager").GetComponent<GameManager> ().level++;
			}
			else
			{
				SceneManager.LoadScene("End_Scene");
			}
                //GameObject.Find("GameManager").GetComponent<GameManager>().level++;
                //GameObject.Find("SoundManager").GetComponent<SoundManager>().endL = true;
            //}
        }
    }
}
