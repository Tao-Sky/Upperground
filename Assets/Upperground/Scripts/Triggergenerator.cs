﻿using UnityEngine;
using System.Collections;

public class Triggergenerator : MonoBehaviour {
    public Sprite oppendoor;
	public Sprite restored;
	private bool Sprite = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Sprite)
            {
                GetComponentInChildren<SpriteRenderer>().enabled = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetButtonDown("X button"))
        {
			GameObject Door = GameObject.Find("Door");
			GameObject Generator = GameObject.Find("Generator");

            Door.GetComponent<BoxCollider2D>().enabled = false;
			if(Door.GetComponent<SpriteRenderer>().sprite.name == "Door1")
			{
				Door.GetComponent<Animator> ().Play ("Open",-1,-1.0f);
				Generator.GetComponent<Animator> ().Stop ();
				Generator.GetComponent<SpriteRenderer> ().sprite = restored;				
			}
            Door.GetComponent<SpriteRenderer>().sprite = oppendoor;
			GetComponentInChildren<SpriteRenderer>().enabled = false;

			Sprite = false;
        }
    }
}
