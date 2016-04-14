using UnityEngine;
using System.Collections;

public class LumoSFX : MonoBehaviour {
	public AudioSource Run;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update()
	{
		GameObject Player = GameObject.Find("Player");
		int rand = Random.Range (75, 105);
		float pitch = (float)rand / 100.0f;
		if(Player.GetComponent<Animator>().GetFloat("speed")>0.2 && Player.GetComponent<Animator>().GetBool("grounded") && !Run.isPlaying)
		{
			Run.pitch = pitch;
			Run.Play ();
		}
		else if(Player.GetComponent<Animator>().GetFloat("speed")<0.2 || !Player.GetComponent<Animator>().GetBool("grounded") )
		{
			Run.Stop ();
		}
	}
}
