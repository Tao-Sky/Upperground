using UnityEngine;
using System.Collections;

public class BlobSFX : MonoBehaviour {
	public AudioSource GreenMove;
	public AudioSource GreenDead;

	// Use this for initialization

	// Update is called once per frame
	void Update()
	{
		if(!GreenMove.isPlaying)
		{
			int randP = Random.Range (85, 115);
			float pitch = (float)randP / 100.0f;
			GreenMove.pitch = pitch;
			GreenMove.Play ();
		}
	}

	public void Hit()
	{
		int randP = Random.Range (85, 115);
		float pitch = (float)randP / 100.0f;
		GreenDead.pitch = pitch;
		GreenDead.Play ();		
	}
}
