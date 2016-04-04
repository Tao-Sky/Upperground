using UnityEngine;
using System.Collections;

public class Triggergenerator : MonoBehaviour {
    public Sprite oppendoor;
	public Sprite restored;
    void OnTriggerEnter2D(Collider2D other)
    {
       
    }

    void OnTriggerExit2D(Collider2D other)
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetButtonDown("X button"))
        {
			GameObject Door = GameObject.Find("Door");
			GameObject Generator = GameObject.Find("Generator");

            Door.GetComponent<BoxCollider2D>().enabled = false;
			Door.GetComponent<Animator> ().Play ("Open",-1,-1.0f);
			Generator.GetComponent<Animator> ().Stop ();
			Generator.GetComponent<SpriteRenderer> ().sprite = restored;
            Door.GetComponent<SpriteRenderer>().sprite = oppendoor;
        }
    }
}
