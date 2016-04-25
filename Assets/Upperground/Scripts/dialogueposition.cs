using UnityEngine;
using System.Collections;

public class dialogueposition : MonoBehaviour {

    public GameObject target;
	void Update () {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y+1f,-1.5f);
	}
}
