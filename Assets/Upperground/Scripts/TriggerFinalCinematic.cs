using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggerFinalCinematic : MonoBehaviour {
	public AudioSource BossSFX;
	public AudioSource Impact;
	private bool unique = false;
	private bool impactUnique = false;
	private int nbBossScreams = 0;
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && !unique)
		{
			unique = false;
			if(!BossSFX.isPlaying && nbBossScreams < 1)
			{
				BossSFX.Play ();
				nbBossScreams++;
			}
			GameObject.Find("Player").GetComponent<PlayerController>().canmove = false;
			Animator A = GameObject.Find("Player").GetComponent<PlayerController> ().anim;
			A.SetBool ("grounded", true);
			A.SetBool ("up", false);
			A.SetFloat ("speed", 0f);
			GameObject.Find ("Main Camera").GetComponent<Animator> ().SetBool ("unzoomBoss", true);
			StartCoroutine (Boss());
		}
	}

	public IEnumerator Boss()
	{
		yield return new WaitForSeconds (4.5f);

		GameObject.Find ("Boss").GetComponent<Rigidbody2D> ().gravityScale = 1.0f;

		yield return new WaitForSeconds (1.5f);

		GameObject.Find ("Main Camera").GetComponent<CameraController> ().shaketime = 1.8f;
		if(!Impact.isPlaying & !impactUnique)
		{
			Impact.Play ();
			impactUnique = true;
		}

		yield return new WaitForSeconds (9.0f);

		GameObject.Find ("SoundManager").GetComponent<SoundManager> ().StopBossTheme (2.0f);

		yield return new WaitForSeconds (2.0f);

		SceneManager.LoadScene ("End_Scene");
	}
}
