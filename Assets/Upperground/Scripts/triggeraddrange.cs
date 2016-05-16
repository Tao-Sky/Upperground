using UnityEngine;
using System.Collections;

public class triggeraddrange : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sha" || other.gameObject.tag == "Player")
        {
            GameObject.Find("Sha").GetComponent<CircleCollider2D>().radius = 28;
        }
    }
}
