using UnityEngine;
using System.Collections;

public class ShaSFX : MonoBehaviour {
	public AudioSource eclair;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Thunder()
	{
		Debug.Log ("Jessie Volt attaque éclair !");
		int rand = Random.Range (85, 120);
		float pitch = (float)rand / 100.0f;
		eclair.pitch = pitch;
		eclair.Play ();
	}

}
