using UnityEngine;
using System.Collections;

public class TriggerSpikes : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Test mort joueur");

            GameObject[] listeCP = GameObject.FindGameObjectsWithTag("Checkpoint");

            foreach (GameObject g in listeCP)
            {
                if (g.GetComponent<TriggerCheckpoint>().getIsActivated() == true)
                {
                    coll.gameObject.GetComponent<PlayerController>().isRespawning = true;
                    coll.gameObject.GetComponent<PlayerController>().getRigidbody2D().velocity = new Vector2(0, 0);
                    coll.transform.position = new Vector3(g.transform.position.x, g.transform.position.y - 4.0f, coll.transform.position.z);

                    GameObject s = GameObject.Find("Sha");

                    if (s.GetComponent<FollowPlayer>().playerFound == false)
                    {
                        s.GetComponent<FollowPlayer>().Respawn();
                    }

                    if (s.GetComponent<FollowPlayer>().playerFound == true && s.GetComponent<FollowPlayer>().nocoroutine == true)
                        s.transform.position = new Vector3(g.transform.position.x, g.transform.position.y - 4.0f, s.transform.position.z);

                    break;
                }
            }
        }
    }
}
