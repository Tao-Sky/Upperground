using UnityEngine;
using System.Collections;

public class triggerPlateformMove : MonoBehaviour {
    public GameObject plateforme;
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
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sha" && Input.GetButtonDown("Y button") && GameObject.Find("Player").GetComponent<PlayerController>().power==0)
        {
            plateforme.GetComponent<platerformeMove>().go();
            gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sha")
        {
            GetComponentInChildren<ParticleSystem>().Stop();
        }
    }
}
