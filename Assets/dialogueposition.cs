using UnityEngine;
using System.Collections;

public class dialogueposition : MonoBehaviour {

    public GameObject target;
	void Update () {
        transform.position = new Vector2(target.transform.position.x, target.transform.position.y+1f);
	}
}
