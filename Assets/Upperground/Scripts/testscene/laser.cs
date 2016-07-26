using UnityEngine;
using System.Collections;

public class laser : MonoBehaviour {

    private Vector3 savedscale;
    private Vector3 savedposition;
    private Vector2 savedboxsize;

    void Start()
    {
        savedscale = transform.localScale;
        savedposition = transform.position;
        savedboxsize = GetComponent<BoxCollider2D>().size;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Mouving Plateform")
        {
            float mysize= transform.localScale.x * GetComponent<BoxCollider2D>().size.x;
            float ysize = other.gameObject.transform.localScale.y * other.gameObject.GetComponent<BoxCollider2D>().size.y;
            float newsize = mysize - ysize;
            float tauxvariation = newsize / mysize;
            float diffsize = Mathf.Abs(newsize - mysize);

            
            transform.position = new Vector3(transform.position.x, transform.position.y +diffsize/2/** tauxvariation*/, transform.position.z);
            transform.localScale = new Vector3(newsize / GetComponent<BoxCollider2D>().size.x, transform.localScale.y, transform.localScale.z);
            GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<BoxCollider2D>().size.x / tauxvariation, GetComponent<BoxCollider2D>().size.y);
            GetComponent<BoxCollider2D>().offset = new Vector2(Mathf.Abs(savedposition.y -transform.localPosition.y),0);
        }

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("you died");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Mouving Plateform")
        {
            transform.localScale = savedscale;
            transform.position = savedposition;
            GetComponent<BoxCollider2D>().size = savedboxsize;
            GetComponent<BoxCollider2D>().offset = new Vector2(0,0);
        }
    }

}
