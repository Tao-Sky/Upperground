using UnityEngine;
using System.Collections;

public class ShaEnergyActivation : MonoBehaviour {
    private bool Sprite = true;
    public Sprite oppendoor;
    private int power;
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

    void Update()
    {
        GameObject player = GameObject.Find("Player");
        power = player.GetComponent<PlayerController>().power;
        Debug.Log(power);
    }

        void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButtonDown("Y button"))
        {
            GameObject sha = GameObject.Find("Sha");
            GameObject player = GameObject.Find("Player");
            if (power == 0 && sha.GetComponent<FollowPlayer>().PowerUnlocked > 0)
            {
                GameObject Door = GameObject.Find("Doorendlvl");
               
                if (sha.GetComponent<FollowPlayer>().playerFound)
                {
                    Vector3 centremachine = new Vector3(75.41f, -4.85f, -1.50f);
                    sha.GetComponent<FollowPlayer>().appel = 2;
                    sha.GetComponent<FollowPlayer>().goToMachine(centremachine, 1.0f);//sha va la machine situé en centremachine et y reste 1.0 seconde
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
