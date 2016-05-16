using UnityEngine;
using System.Collections;

public class triggerSpeed : MonoBehaviour {

    public float newspeed;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sha" || other.gameObject.tag == "Player")
        {
            GameObject.Find("Sha").GetComponent<FollowPlayer>().speed = newspeed;
        }   
     }
}
