using UnityEngine;
using System.Collections;

public class nextLevel : MonoBehaviour {
    public GameObject teleportationpoint;

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
            GameObject Player = GameObject.Find("Player");
            GameObject Sha = GameObject.Find("Sha");
            if (Sha.GetComponent<FollowPlayer>().playerFound)
            {
                Sha.transform.position = teleportationpoint.transform.position;
            }
            Player.transform.position= teleportationpoint.transform.position/*new Vector3(47.02f,-7.58f,-1.55f)*/;
        }
    }
}
