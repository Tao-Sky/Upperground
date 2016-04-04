using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public GameObject Player;

	public AudioSource Drum ;
	public AudioSource Synth ;
	public AudioSource Bass ;
	public AudioSource Lead ;
	public AudioSource Bridge ;

	private AudioClip[] track = new AudioClip[7];
	private double TimeLvl1 = 29.088;
	private double CurrentTime = 0.0;
	private bool IsNotBridge = true;
	private int facteurBridge = 0;

	public AudioMixerSnapshot SnapDrum1, SnapDrum2;
	public AudioMixerSnapshot SnapSynth1, SnapSynth2;
	public AudioMixerSnapshot SnapBass1, SnapBass2;
	public AudioMixerSnapshot SnapLead1, SnapLead2;
	public AudioMixerSnapshot SnapBridge1, SnapBridge2;

	private double xBass = -30.0f;
	private double xDrum = -2.3f;
	private double xLead = 53.0f;

	private bool boolBass=false;
	private bool boolDrum=false;
	private bool boolLead=false;



	// Use this for initialization

	void Start () {
		LoadTracksLvl1 ();
		Drum.Play ();
		Synth.Play ();
		Bass.Play ();
		Lead.Play ();
		Bridge.Play ();


	}

	// Update is called once per frame
	void Update () 
	{
		
		if(Player.transform.position.x > xBass && !boolBass && IsNotBridge)
		{
			boolBass = true;
			SnapBass1.TransitionTo (2);
		}
		if(Player.transform.position.x > xDrum && !boolDrum && IsNotBridge)
		{
			boolDrum = true;
			SnapDrum1.TransitionTo (2);
		}
		if(Player.transform.position.x > xLead && !boolLead && IsNotBridge)
		{
			boolLead = true;
			SnapLead1.TransitionTo (2);
		}
	

		if(CurrentTime > TimeLvl1 )
		{
			if(IsNotBridge)
			{
				CallBridge (true);
				IsNotBridge = false;
				CurrentTime = 0.0;
				/*if(facteurBridge%4 == 0)
				{
					SwitchTheme (true);
				}
				else
				{
					SwitchTheme (false);
				}*/
			}

			else
			{
				CallBridge (false);
				IsNotBridge = true;
				CurrentTime = 0.0;
			}
			facteurBridge++;
		}
		else
		{
			CurrentTime = Time.fixedTime - (facteurBridge*TimeLvl1);
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

	void SwitchTheme(bool b)
	{
		if(b)
		{
			Lead.Stop ();
			Lead.clip = ;
			Drum.Stop ();
			Drum.clip = track [3];
			Lead.Play ();
			Drum.Play ();
		}
		else
		{
			Lead.Stop ();
			Lead.clip = track [4];
			Drum.Stop ();
			Drum.clip = track [2];
			Lead.Play ();
			Drum.Play ();
		}

	}

	void LoadTracksLvl1()
	{/*
		track[0]=(AudioClip)Resources.Load("Assets/Upperground/Sound/Music/Lvl1/Synth");
		track[1]=(AudioClip)Resources.Load("Assets/Upperground/Sound/Music/Lvl1/Bass");
		track[2]=(AudioClip)Resources.Load("Assets/Upperground/Sound/Music/Lvl1/Drum1");
		track[3]=(AudioClip)Resources.Load("Assets/Upperground/Sound/Music/Lvl1/Drum2");
		track[4]=(AudioClip)Resources.Load("Assets/Upperground/Sound/Music/Lvl1/Lead1");
		track[5]=(AudioClip)Resources.Load("Assets/Upperground/Sound/Music/Lvl1/Lead2");
		track[6]=(AudioClip)Resources.Load("Assets/Upperground/Sound/Music/Lvl1/Bridge");
		*/
		SnapSynth1.TransitionTo (0);
		SnapBass2.TransitionTo (0);
		SnapBridge2.TransitionTo (0);
		SnapDrum2.TransitionTo (0);
		SnapLead2.TransitionTo (0);
	}
}
