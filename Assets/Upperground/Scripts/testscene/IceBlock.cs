using UnityEngine;
using System.Collections;

public class IceBlock : MonoBehaviour {
    public bool iced = false;
    void Update()
    {
     
    }

    public void getfrozzen()
    {
        iced = true;
        Debug.Log("je suis glacé");
        GetComponent<SpriteRenderer>().color = new Color(0.2f,0.5f,1f);
    }

    public void getwarmed()
    {
        iced = false;
        Debug.Log("je suis chaud");
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
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
