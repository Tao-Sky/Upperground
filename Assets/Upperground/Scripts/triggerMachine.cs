using UnityEngine;
using System.Collections;

public class triggerMachine : MonoBehaviour
{
 
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            GetComponentInChildren<SpriteRenderer>().enabled = true;
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
        if (Input.GetButtonDown("X button"))
        {
            Debug.Log("je rentre la");
            GameObject sha = GameObject.Find("Sha");
            if (sha.GetComponent<FollowPlayer>().playerFound)
            {
                sha.GetComponent<FollowPlayer>().goToMachine();
            }
            Debug.Log("je sort la");
        }
    }
 

}
