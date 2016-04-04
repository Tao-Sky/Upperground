using UnityEngine;
using System.Collections;

public class TriggerCheckpoint : MonoBehaviour {

    public bool isActivated = false;
    //public bool firstTimeChecked = false;

    public bool getIsActivated(){return isActivated;}
    public void setIsActivated(bool b) {isActivated = b;}
    //public bool getFirstTimeChecked() { return firstTimeChecked; }
    //public void setFirstTimeChecked(bool b) { firstTimeChecked = b; }

    void OnTriggerEnter2D(Collider2D other)
    {
        foreach(Transform t in transform.parent)
        {
            t.GetComponent<TriggerCheckpoint>().setIsActivated(false);
        }

        //if (!getFirstTimeChecked())
        //{
        //  setFirstTimeChecked(true);
            isActivated = true;
        //}

        //Debug.Log(isActivated);
    }

}
