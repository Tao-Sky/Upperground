using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggerFinalCinematic : MonoBehaviour {
	public AudioSource BossSFX;
	private bool unique = false;
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && !unique)
		{
			unique = false;
			if(!BossSFX.isPlaying)
			{
				BossSFX.Play ();
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

		GameObject.Find ("Boss").GetComponent<Rigidbody2D> ().gravityScale = 1f;

		yield return new WaitForSeconds (11.5f);

		GameObject.Find ("SoundManager").GetComponent<SoundManager> ().StopBossTheme (2f);

		yield return new WaitForSeconds (2.0f);

		SceneManager.LoadScene ("End_Scene");
	}
}
