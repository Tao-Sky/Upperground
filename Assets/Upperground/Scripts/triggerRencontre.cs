using UnityEngine;
using System.Collections;

public class triggerRencontre : MonoBehaviour {

    private bool first = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(first && other.gameObject.tag == "Player")
        {
            first = false;
            GameObject sha = GameObject.Find("Sha");
            GameObject player = GameObject.Find("Player");
            sha.GetComponent<FollowPlayer>().nocoroutine = false;
            player.GetComponent<PlayerController>().canmove = false;
            StartCoroutine(sha.GetComponent<FollowPlayer>().CinematicRencontre());
        }
    }
}
