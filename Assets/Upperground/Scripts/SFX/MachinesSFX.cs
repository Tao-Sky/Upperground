using UnityEngine;
using System.Collections;

public class MachinesSFX : MonoBehaviour {
	public AudioSource Fire;
	public AudioSource Acid;
	public AudioSource Electric;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayFire()
	{
		Fire.Play ();
	}

	public void PlayAcid()
	{
		Acid.Play ();
	}

	public void PlayElectric()
	{
		Electric.Play ();
	}

}
