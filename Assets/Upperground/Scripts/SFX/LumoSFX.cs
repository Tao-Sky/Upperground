﻿using UnityEngine;
using System.Collections;

public class LumoSFX : MonoBehaviour {
	public AudioSource Run;
	public AudioSource Jump;
	public AudioSource Hit;
	// Use this for initialization

	private bool jump = false;

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

		if(Player.GetComponent<PlayerController>().jump && !jump && rand < 90)
		{
			Jump.pitch = pitch;
			Jump.Play ();
			jump = true;
		}

		if(Player.GetComponent<PlayerController>().anim.GetBool("grounded"))
		{
			jump = false;
		}
	}

	public void LumoHit()
	{
		int rand = Random.Range (75, 105);
		float pitch = (float)rand / 100.0f;
		Hit.pitch = pitch;
		Hit.Play ();
	}
}
