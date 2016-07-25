using UnityEngine;
using System.Collections;

public class SpeedTransfer : MonoBehaviour {
    private bool first=true;

    void OnTriggerEnter2D(Collider2D other)
    {
       if(first && other.gameObject.tag == "Player")
        {
            //first = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(other.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
        }
    }
}
