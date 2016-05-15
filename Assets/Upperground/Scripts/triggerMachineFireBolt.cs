using UnityEngine;
using System.Collections;

public class triggerMachineFireBolt : MonoBehaviour {
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
        if (other.gameObject.tag == "Sha" && Input.GetButtonDown("Y button") && GameObject.Find("Player").GetComponent<PlayerController>().power == 1)
        {
            GameObject.Find("fireBolt").GetComponent<fire>().go();
            gameObject.SetActive(false);
        }
    }
    }
