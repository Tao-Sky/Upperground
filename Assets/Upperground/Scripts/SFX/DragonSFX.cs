using UnityEngine;
using System.Collections;

public class DragonSFX : MonoBehaviour {
	public AudioSource RedMove;

	// Use this for initialization

	// Update is called once per frame
	void Update()
	{
		if(!RedMove.isPlaying)
		{
			int randP = Random.Range (85, 115);
			float pitch = (float)randP / 100.0f;
			RedMove.pitch = pitch;
			RedMove.Play ();
		}
	}
}
