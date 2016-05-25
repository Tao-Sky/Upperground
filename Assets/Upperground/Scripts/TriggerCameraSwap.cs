using UnityEngine;
using System.Collections;

public class TriggerCameraSwap : MonoBehaviour {
	public int Screen;
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			GameObject.Find ("Main Camera").GetComponent<CameraController> ().SwapLvl3 (Screen);
		}
	}
}
