﻿using UnityEngine;
using System.Collections;

public class ShaEnergyActivation : MonoBehaviour {
    private bool Sprite = true;
	private bool Powers = false;
    public Sprite oppendoor;
    private int power;
    public GameObject centremachine;
    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.tag == "Player" && Powers)
        {
            if (Sprite)
            {
                GetComponentInChildren<SpriteRenderer>().enabled = true;
            }
        }
           
    }

    void OnTriggerExit2D(Collider2D other)
    {
		if (other.gameObject.tag == "Player" && Powers)
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    void Update()
    {
        GameObject player = GameObject.Find("Player");
        power = player.GetComponent<PlayerController>().power;
		Powers = GameObject.Find ("Sha").GetComponent<FollowPlayer> ().PowersAvailable;
        //Debug.Log(power);
    }

        void OnTriggerStay2D(Collider2D other)
    {
		if (Input.GetButtonDown("Y button") && Powers && Sprite && other.gameObject.tag == "Player")
        {
            GameObject sha = GameObject.Find("Sha");
            if (power == 0 && sha.GetComponent<FollowPlayer>().PowerUnlocked > 0)
            {
                GameObject Door = GameObject.Find("Doorendlvl");
               
                if (sha.GetComponent<FollowPlayer>().playerFound)
                {
                    sha.GetComponent<FollowPlayer>().goToMachine(centremachine.transform.position, 1.0f,2);//sha va la machine situé en centremachine et y reste 1.0 seconde et il ferra l'action 2
					//sha.GetComponent<FollowPlayer>().LaunchPower(0, centremachine.transform);// version ou sha fait une attaque

                }

                Door.GetComponent<BoxCollider2D>().enabled = false;
                if (Door.GetComponent<SpriteRenderer>().sprite.name == "Door1")
                {
                    Door.GetComponent<Animator>().Play("Open", -1, -1.0f);
                }
                Door.GetComponent<SpriteRenderer>().sprite = oppendoor;
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                Sprite = false;
            }
        }
    }
}
