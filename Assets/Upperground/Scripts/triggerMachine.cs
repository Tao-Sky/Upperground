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
        Debug.Log(allumeM);

		if (Input.GetButtonDown("X button") && !used)
        {
			if (allumeM)
			{
				GameObject sha = GameObject.Find ("Sha");
				if (sha.GetComponent<FollowPlayer> ().playerFound) 
				{
                    Vector3 centremachine = new Vector3(-12.18f, -6.85f, -1.50f);
				
                    sha.GetComponent<FollowPlayer> ().goToMachine (centremachine,5.0f,1);//sha va la machine situé en centremachine et y reste 5.0 seconde et fera l'action 1
					GameObject.Find("Player").GetComponent<PlayerController>().canmove = false;
					GameObject.Find("Player").GetComponent<PlayerController>().anim.SetFloat("speed",0f);
					GameObject.Find("Player").GetComponent<PlayerController>().anim.SetBool("grounded",true);
					GameObject.Find("Player").GetComponent<PlayerController>().anim.SetBool("up",false);


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
