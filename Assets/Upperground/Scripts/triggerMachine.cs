using UnityEngine;
using System.Collections;

public class triggerMachine : MonoBehaviour
{
	public GameObject Generator;
	private GameObject MessageEteint;
	private GameObject MessageSha;
	private bool allumeM;
	private bool used = false;

    void OnTriggerEnter2D(Collider2D other)
    {
		if(other.gameObject.tag == "Player" && !used)
        {
            GetComponentInChildren<SpriteRenderer> ().enabled = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
		MessageEteint = GameObject.Find ("Message_eteint");

        if (other.gameObject.tag == "Player" && !used)
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

		if (Input.GetButtonDown("X button") && !used)
        {
			if (allumeM)
			{
				GameObject sha = GameObject.Find ("Sha");
				if (sha.GetComponent<FollowPlayer> ().playerFound) 
				{
                    Vector3 centremachine = new Vector3(-12.18f, -6.85f, -1.50f);
				
					GameObject player = GameObject.Find("Player");

					player.GetComponent<PlayerController>().canmove = false;
					player.GetComponent<PlayerController>().anim.SetFloat("speed",0f);
					player.GetComponent<PlayerController>().anim.SetBool("grounded",true);
					player.GetComponent<PlayerController>().anim.SetBool("up",false);
		
					sha.GetComponent<FollowPlayer> ().goToMachine (centremachine,5.0f,1);//sha va la machine situé en centremachine et y reste 5.0 seconde et fera l'action 1

					//sha.GetComponent<FollowPlayer>().PowerUnlocked = 1;
                    GetComponentInChildren<SpriteRenderer> ().enabled = false;
					used = true;
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
