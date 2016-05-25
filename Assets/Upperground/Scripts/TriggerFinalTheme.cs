using UnityEngine;
using System.Collections;

public class TriggerFinalTheme : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			GameObject.Find("SoundManager").GetComponent<SoundManager>().boolBoss = true;
		}
	}
}
