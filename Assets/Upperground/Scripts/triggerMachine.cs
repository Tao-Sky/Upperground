using UnityEngine;
using System.Collections;

public class triggerMachine : MonoBehaviour
{
	public GameObject Generator;
	private GameObject MessageEteint;
	private GameObject MessageSha;
	private bool allumeM;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            GetComponentInChildren<SpriteRenderer> ().enabled = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
		MessageEteint = GameObject.Find ("Message_eteint");

        if (other.gameObject.tag == "Player")
        {
            GetComponentInChildren<SpriteRenderer> ().enabled = false;
			MessageEteint.GetComponent<SpriteRenderer> ().enabled = false;
			MessageSha.GetComponent<SpriteRenderer> ().enabled = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
		MessageEteint = GameObject.Find ("Message_eteint");
		MessageSha = GameObject.Find ("Message_Sha_Missing");

		allumeM = Generator.GetComponent<Triggergenerator> ().allume;

		if (Input.GetButtonDown("X button"))
        {
			if (allumeM)
			{
				GameObject sha = GameObject.Find ("Sha");
				if (sha.GetComponent<FollowPlayer> ().playerFound) 
				{
					sha.GetComponent<FollowPlayer> ().goToMachine ();
					GetComponentInChildren<SpriteRenderer> ().enabled = false;
				}
				else
				{
					MessageSha.GetComponent<SpriteRenderer> ().enabled = true;
					GetComponentInChildren<SpriteRenderer> ().enabled = false;
				}
			}
			else
			{
				MessageEteint.GetComponent<SpriteRenderer> ().enabled = true;
				GetComponentInChildren<SpriteRenderer> ().enabled = false;
			}

        }
    }
 

}
