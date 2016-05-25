using UnityEngine;
using System.Collections;

public class triggerMachineAcide : MonoBehaviour {
    public GameObject acide;
    void Start()
    {
        GetComponentInChildren<ParticleSystem>().Stop();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sha")
        {
            GetComponentInChildren<ParticleSystem>().Play();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sha")
        {
            GetComponentInChildren<ParticleSystem>().Stop();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sha" && Input.GetButtonDown("Y button") && GameObject.Find("Player").GetComponent<PlayerController>().power == 2)
        {
            acide.GetComponent<dropacide>().go();
			GameObject.Find ("Sha").GetComponent<FollowPlayer> ().LaunchPower (2, this.transform);
			GameObject.Find ("SFXManager").GetComponent<MachinesSFX>().PlayAcid ();
            gameObject.SetActive(false);
        }
    }
}
