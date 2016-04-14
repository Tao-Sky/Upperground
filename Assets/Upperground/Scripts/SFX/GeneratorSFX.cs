using UnityEngine;
using System.Collections;

public class GeneratorSFX : MonoBehaviour {
	public AudioSource SFX;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
		GameObject Generator = GameObject.Find("Generator");
		int rand = Random.Range (85, 120);
		float pitch = (float)rand / 100.0f;
		if(Generator.GetComponent<SpriteRenderer>().sprite.name == "broken")
		{
			SFX.pitch = pitch;
			SFX.Play ();
		}
	}
}
