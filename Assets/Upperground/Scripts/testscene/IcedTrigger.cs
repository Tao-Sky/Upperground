using UnityEngine;
using System.Collections;

public class IcedTrigger : MonoBehaviour {

    private bool isfrozen = false;
    public GameObject iceblock;
    private float attente;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player est dedans");
        }
    }


        void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Input.GetButtonDown("Y button") /*&& GameObject.Find("Player").GetComponent<PlayerController>().power == 3*/ && !isfrozen && attente<0.1)
        {
            attente = 1;
            StartCoroutine(stop());
            Debug.Log("je tire pour glacer");
            isfrozen = true;
            GameObject.Find("Sha").GetComponent<FollowPlayer>().LaunchPower(3, this.transform);
                iceblock.GetComponent<IceBlock>().getfrozzen();

        }

        if (other.gameObject.tag == "Player" && Input.GetButtonDown("Y button") /*&& GameObject.Find("Player").GetComponent<PlayerController>().power == 1*/ && isfrozen && attente<0.1)
        {
            attente = 1;
            StartCoroutine(stop()); 
            isfrozen = false;
            Debug.Log("je tire pour chauffer");
            GameObject.Find("Sha").GetComponent<FollowPlayer>().LaunchPower(1, this.transform);
                iceblock.GetComponent<IceBlock>().getwarmed();

               
        }
    }

    IEnumerator stop()
    {
        yield return new WaitForSeconds(1);
        attente = 0;
    }
   
}
