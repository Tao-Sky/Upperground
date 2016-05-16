using UnityEngine;
using System.Collections;

public class TriggerCheckpoint : MonoBehaviour {

    public bool isActivated = false;
    //public bool firstTimeChecked = false;

    public bool getIsActivated(){return isActivated;}
    public void setIsActivated(bool b) {isActivated = b;}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            foreach (Transform t in transform.parent)
            {
                t.GetComponent<TriggerCheckpoint>().setIsActivated(false);
            }

            isActivated = true;
            
        }
    }

}
