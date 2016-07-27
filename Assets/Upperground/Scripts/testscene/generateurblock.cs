using UnityEngine;
using System.Collections;

public class generateurblock : MonoBehaviour {

    public GameObject myblock;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Input.GetButtonDown("Y button") && GameObject.Find("Player").GetComponent<PlayerController>().power == 0 )
        {
            myblock.transform.position = new Vector3(transform.position.x, transform.position.y - (GetComponent<BoxCollider2D>().size.y/2), myblock.transform.position.z);
        }

    }
}
