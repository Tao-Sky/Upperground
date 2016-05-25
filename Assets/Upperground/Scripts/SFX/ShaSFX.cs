using UnityEngine;
using System.Collections;

public class ShaSFX : MonoBehaviour {
	public AudioSource eclair;
	public AudioSource fire;
	public AudioSource acid;
	public AudioSource ice;
	public AudioSource scream;
	public AudioSource powerEclair;



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

	public void Fire()
	{
		int rand = Random.Range (85, 120);
		float pitch = (float)rand / 100.0f;
		fire.pitch = pitch;
		fire.Play ();
	}

	public void Acid()
	{
		int rand = Random.Range (85, 120);
		float pitch = (float)rand / 100.0f;
		acid.pitch = pitch;
		acid.Play ();
	}

	public void Ice()
	{
		int rand = Random.Range (85, 120);
		float pitch = (float)rand / 100.0f;
		ice.pitch = pitch;
		ice.Play ();
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

	public void PowerEclair()
	{
		int rand = Random.Range (85, 120);
		float pitch = (float)rand / 100.0f;
		powerEclair.pitch = pitch;
		powerEclair.Play ();	
	}
}
