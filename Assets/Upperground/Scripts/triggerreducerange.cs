using UnityEngine;
using System.Collections;

public class triggerreducerange : MonoBehaviour {


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sha")
        {
            GameObject.Find("Sha").GetComponent<CircleCollider2D>().radius = 3;
        }
    }
}
