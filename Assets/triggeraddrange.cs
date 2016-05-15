using UnityEngine;
using System.Collections;

public class triggeraddrange : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sha")
        {
            GameObject.Find("Sha").GetComponent<CircleCollider2D>().radius = 28;
        }
    }
}
