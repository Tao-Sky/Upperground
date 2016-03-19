using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

	public float speed;
	public float jumpHeight;
	public bool touch;
	public bool facingR=true;

	Animator anim;

	// Use this for initialization
	void Start () {
		touch = true;
		anim = GetComponent<Animator> ();
	}

	void Rotate(){
		transform.Rotate (Vector3.up, 180);
	}

	// Update is called once per frame
	void Update () {
	
		anim.SetFloat ("speed", Mathf.Abs (Input.GetAxis ("Horizontal")));

		if (Input.GetKey (KeyCode.Space) && touch) {
			GetComponent<Rigidbody2D>().velocity = new Vector2 (0, jumpHeight);
			touch = false;
		}

		if (GetComponent<Rigidbody2D>().IsTouchingLayers()) {
			touch = true;
		}

		if (Input.GetKey (KeyCode.D)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2 (speed, 0);
			if (!facingR) {
				this.Rotate();
				facingR = true;
			}
		}

		if (Input.GetKey (KeyCode.Q)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2 (-speed, 0);
			if (facingR) {
				this.Rotate ();
				facingR = false;
			}
		}


	}
}
