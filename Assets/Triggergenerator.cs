﻿using UnityEngine;
using System.Collections;

public class Triggergenerator : MonoBehaviour {
    public Sprite oppendoor;
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
            GameObject door = GameObject.Find("door");
            door.GetComponent<BoxCollider2D>().enabled = false;
            door.GetComponent<SpriteRenderer>().sprite = oppendoor;
            Debug.Log("putain ça marche");
        }
    }
}
