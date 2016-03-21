using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {

	public Material M;
	public GameObject Player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Player.GetComponent<Rigidbody2D> ().position.x > -35.0) 
		{
			Debug.Log("coucou");
		}
	}
}
