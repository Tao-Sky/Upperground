using UnityEngine;
using System.Collections;

public class FlyingSFX : MonoBehaviour {
	public AudioSource BlueMove;

	// Use this for initialization

	// Update is called once per frame
	void Update()
	{
		if(!BlueMove.isPlaying)
		{
			int randP = Random.Range (85, 115);
			float pitch = (float)randP / 100.0f;
			BlueMove.pitch = pitch;
			BlueMove.Play ();
		}
	}
}
