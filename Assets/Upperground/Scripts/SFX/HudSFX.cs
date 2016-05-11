using UnityEngine;
using System.Collections;

public class HudSFX : MonoBehaviour {
	public AudioSource paused;
	public AudioSource unpaused;
	public AudioSource pauseMove;

	public void pauseSFX()
	{
		paused.Play ();
	}

	public void unpauseSFX()
	{
		unpaused.Play ();
	}

	public void pauseMoveSFX()
	{
		pauseMove.Play ();
	}

}
