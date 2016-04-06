﻿using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public GameObject Player;

	public AudioSource Drum1 ;
	public AudioSource Drum2 ;
	public AudioSource Synth ;
	public AudioSource Bass ;
	public AudioSource Lead1 ;
	public AudioSource Lead2 ;
	public AudioSource Bridge ;

	public AudioMixerSnapshot SnapDrum1, SnapDrum2;
	public AudioMixerSnapshot SnapSynth1, SnapSynth2;
	public AudioMixerSnapshot SnapBass1, SnapBass2;
	public AudioMixerSnapshot SnapLead1, SnapLead2;
	public AudioMixerSnapshot SnapBridge1, SnapBridge2;

	public AudioMixer Music;

	private double xBass = -30.0f;
	private double xDrum = -2.3f;
	private double xLead = 53.0f;

	private int level;
	private int nextTheme = 0;
	private bool boolBass=false;
	private bool boolDrum=false;
	private bool boolLead=false;

		void Start () 
	{
		LoadSnaps ();
	}

	void Update () 
	{
		ControlPosition();
		Effects ();
		chooseTheme ();
	}

	void chooseTheme()
	{
		if(!Bridge.isPlaying && !Synth.isPlaying)
		{
			if(nextTheme == 0)
			{
				CallBridge (false);
				nextTheme = 1;
				Synth.Play ();
				Drum1.Play ();
				Bass.Play ();
				Lead1.Play ();
			}
			else if(nextTheme == 1)
			{
				CallBridge (true);
				nextTheme = 2;
				Bridge.Play ();
			}
			else if(nextTheme == 2)
			{
				CallBridge (false);
				nextTheme = 3;
				Synth.Play ();
				Drum2.Play ();
				Bass.Play ();
				Lead2.Play ();
			}
			else if(nextTheme == 3)
			{
				CallBridge (true);
				nextTheme = 0;
				Bridge.Play ();				
			}
		}
	}

	void CallBridge(bool b)
	{
		if(b)
		{
			SnapBridge1.TransitionTo (0);
			SnapSynth2.TransitionTo (0);
			if(boolDrum)
			{
				SnapDrum2.TransitionTo (0);
			}
			if(boolBass) 
			{	
				SnapBass2.TransitionTo (0);
			}
			if(boolLead)
			{
				SnapLead2.TransitionTo (0);
			}
		}
		else
		{
			SnapBridge2.TransitionTo (0);
			SnapSynth1.TransitionTo (0);
			if(boolDrum)
			{
				SnapDrum1.TransitionTo (0);
			}
			if(boolBass) 
			{	
				SnapBass1.TransitionTo (0);
			}
			if(boolLead)
			{
				SnapLead1.TransitionTo (0);
			}
		}
	}


	void Effects()
	{
		level = FindObjectOfType<GameManager> ().level;
		if(level == 0)
		{
			Music.SetFloat ("lowPassVal", 22000.0f);
			Music.SetFloat ("volumeVal", 0.00f);
		}
		else if(level == 1)
		{
			Music.SetFloat ("lowPassVal", 2800.0f);
			Music.SetFloat ("volumeVal", 0.00f);
		}

		if(FindObjectOfType<GameManager>().IsPaused)
		{
			Music.SetFloat ("lowPassVal", 1600.0f);
			Music.SetFloat ("volumeVal", -8.00f);
		}
	}

	void LoadSnaps()
	{
		SnapSynth1.TransitionTo (0);
		SnapBass2.TransitionTo (0);
		SnapBridge2.TransitionTo (0);
		SnapDrum2.TransitionTo (0);
		SnapLead2.TransitionTo (0);
	}

	void ControlPosition()
	{
		if(Player.transform.position.x > xBass)
		{
			boolBass = true;
		}
		if(boolBass)
		{
			SnapBass1.TransitionTo (2);
		}

		if(Player.transform.position.x > xDrum)
		{
			boolDrum = true;
		}
		if(boolDrum)
		{
			SnapDrum1.TransitionTo (2);
		}
		if(Player.transform.position.x > xLead)
		{
			boolLead = true;
		}
		if(boolLead)
		{
			SnapLead1.TransitionTo (2);
		}		
	}
}
