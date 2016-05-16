using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class End : MonoBehaviour {

	public AudioSource Music;
	private Animator A;
	// Use this for initialization
	void Start () 
	{
		A = gameObject.GetComponent<Animator> ();
		Music.Play ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if(A.GetCurrentAnimatorStateInfo(0).IsName("Over"))
		{
			SceneManager.LoadScene ("Main_Menu");
		}
	}
}
