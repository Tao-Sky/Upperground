using UnityEngine;
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

	private int level;

	private bool boolBass=false;
	private bool boolDrum=false;
	private bool boolLead=false;


	// Use this for initialization

	void Start () {
		LoadSnaps ();
		Drum1.Play ();
		Synth.Play ();
		Bass.Play ();
		Lead1.Play ();
		Bridge.Play ();
	}

	// Update is called once per frame
	void Update () 
	{
		ControlPosition();

		if(CurrentTime > TimeLvl1 )
		{
			if(IsNotBridge)
			{
				CallBridge (true);
				IsNotBridge = false;
				CurrentTime = 0.0;
				if(facteurBridge%4 == 0)
				{
					SwitchTheme (true);
				}
				else
				{
					SwitchTheme (false);
				}
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
			Lead1.Stop ();
			Drum1.Stop ();
			Lead2.Play ();
			Drum2.Play ();
		}
		else
		{
			Lead2.Stop ();
			Drum2.Stop ();
			Lead1.Play ();
			Drum1.Play ();
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
		if(boolBass && IsNotBridge)
		{
			SnapBass1.TransitionTo (2);
		}

		if(Player.transform.position.x > xDrum)
		{
			boolDrum = true;
		}
		if(boolDrum && IsNotBridge)
		{
			SnapDrum1.TransitionTo (2);
		}
		if(Player.transform.position.x > xLead)
		{
			boolLead = true;
		}
		if(boolLead && IsNotBridge)
		{
			SnapLead1.TransitionTo (2);
		}		
	}
}
