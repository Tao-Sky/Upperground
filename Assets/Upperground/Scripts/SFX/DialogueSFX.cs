using UnityEngine;
using System.Collections;

public class DialogueSFX : MonoBehaviour {

	public AudioSource[] Sounds;
	public bool Play = false;
	public bool Reset = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameObject.Find("Dialogue").GetComponent<SpriteRenderer>().color.a >= 1 && Reset)
		{
			Play = true;
			Reset = false;
		}
		else if(GameObject.Find("Dialogue").GetComponent<SpriteRenderer>().color.a <= 0.01)
		{
			Reset = true;
		}

		if(Play)
		{
			int rand = Random.Range (0, 7);
			int randP = Random.Range (95, 115);
			float pitch = (float)randP / 100.0f;
			Sounds [rand].pitch = pitch;
			Sounds [rand].Play ();
			Play = false;
		}

	}
}
