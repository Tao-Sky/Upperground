using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class MusicLvl1 : MonoBehaviour {

	public Transform target;

	public AudioSource Sound1;
	public AudioSource Sound2;
	public AudioSource Sound3;

	public AudioMixerSnapshot Snap1;
	public AudioMixerSnapshot Snap2;

	public AudioMixerSnapshot Snap3;
	public AudioMixerSnapshot Snap4;

	public bool entree1;
	public bool entree2;


	// Use this for initialization
	void Start () {
		entree1 = true;
		entree2 = true;
		Sound1.Play ();
		Sound2.Play ();
		Sound3.Play ();

	}

	// Update is called once per frame
	void Update () {
		if ((GameObject.Find ("Player").GetComponent<Rigidbody2D> ().position.x > -30) & (entree1) ){
			entree1 = false;
			Snap2.TransitionTo (2);
		}

		if ((GameObject.Find ("Player").GetComponent<Rigidbody2D> ().position.x > -10) & (entree2)) {
			entree2 = false;
			Snap4.TransitionTo (2);
		}
	}
}
