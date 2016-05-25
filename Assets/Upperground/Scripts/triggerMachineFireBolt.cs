using UnityEngine;
using System.Collections;

public class triggerMachineFireBolt : MonoBehaviour {
    public GameObject fireboltlauncher;
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
            GetComponentInChildren<ParticleSystem>().Pause();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sha" && Input.GetButtonDown("Y button") && GameObject.Find("Player").GetComponent<PlayerController>().power == 1)
        {
            fireboltlauncher.GetComponent<fire>().go();
			GameObject.Find ("Sha").GetComponent<FollowPlayer> ().LaunchPower (1, this.transform);
			GameObject.Find ("SFXManager").GetComponent<MachinesSFX>().PlayFire ();
            GetComponentInChildren<ParticleSystem>().Pause();
        }
    }
    }
