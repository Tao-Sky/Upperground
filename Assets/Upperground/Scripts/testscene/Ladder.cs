using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D other)
    {      
        if (other.gameObject.tag == "Player")
        {
            float v = Input.GetAxis("Vertical");
            if (v > 0 && other.GetComponent<Rigidbody2D>().velocity.y<0)
            {
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, 0);
            }
            if(v * other.GetComponent<Rigidbody2D>().velocity.y < other.GetComponent<PlayerController>().maxSpeed/2)
            other.GetComponent<Rigidbody2D>().AddForce(Vector2.up * v * other.GetComponent<PlayerController>().moveForce*2);
        }

    }

    /*void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }*/
}
