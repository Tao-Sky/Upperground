using UnityEngine;
using System.Collections;

public class ShaSFX : MonoBehaviour {
	public AudioSource eclair;
	public AudioSource scream;


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
		int rand = Random.Range (85, 120);
		float pitch = (float)rand / 100.0f;
		eclair.pitch = pitch;
		eclair.Play ();
	}

	public void Scream()
	{
		int rand = Random.Range (85, 120);
		if (rand > 100) 
		{
			float pitch = (float)rand / 100.0f;
			scream.pitch = pitch;
			scream.Play ();	
		}
	}
}
