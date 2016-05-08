using UnityEngine;
using System.Collections;

public class MachineSFX : MonoBehaviour {
	public AudioSource machine;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Machine()
	{
		int rand = Random.Range (90, 110);
		float pitch = (float)rand / 100.0f;
			machine.pitch = pitch;
			machine.Play ();
	}
}
