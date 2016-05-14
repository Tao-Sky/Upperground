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
			GameObject.Find ("Main Camera").GetComponent<Animator> ().SetBool ("zoom", true);
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
			sha.transform.LookAt(new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z));
			sha.transform.Rotate(new Vector3(0, -90, 0), Space.Self);
			sha.GetComponent<FollowPlayer>().launchCinematicRencontre();
        }
    }
}
