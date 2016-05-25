using UnityEngine;
using System.Collections;

public class CanGoThrough : MonoBehaviour {


	void Start () {
		gameObject.GetComponent<EdgeCollider2D> ().enabled = false;
	}
	

	void Update () {
		float playery = GameObject.Find ("Player").transform.position.y;
		playery += GameObject.Find ("Player").GetComponent<BoxCollider2D> ().size.y;
		if (playery > gameObject.transform.position.y + 2.0f) {	//la vrai valeur a mettre les la largeur en y de la box collider au lieu de 2.0f (ne marche pas avec edgecollider)		
			gameObject.GetComponent<EdgeCollider2D> ().enabled = true;
		} else {
			gameObject.GetComponent<EdgeCollider2D> ().enabled = false;
		}
	}
}
