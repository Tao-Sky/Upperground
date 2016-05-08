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
			if(sha.transform.position.x > player.transform.position.x && player.transform.localScale.x < 0)
			{
				player.GetComponent<PlayerController> ().Flip ();
			}
			else if(sha.transform.position.x < player.transform.position.x && player.transform.localScale.x > 0)
			{
				player.GetComponent<PlayerController> ().Flip ();
			}
            StartCoroutine(sha.GetComponent<FollowPlayer>().CinematicRencontre());
        }
    }
}
