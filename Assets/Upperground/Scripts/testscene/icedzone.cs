using UnityEngine;
using System.Collections;

public class icedzone : MonoBehaviour {
    private bool iced;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        iced = GetComponentInParent<IceBlock>().iced;
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && iced)
        {
            other.GetComponent<PlayerController>().iced = true;
        }
        if (other.gameObject.tag == "Player" && !iced)
        {

            other.GetComponent<PlayerController>().iced = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().iced = false;
        }
    }
}
