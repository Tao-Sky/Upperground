using UnityEngine;
using System.Collections;

public class nextLevel : MonoBehaviour {
    public GameObject teleportationpoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButtonDown("X button"))
        {
            GameManager gamemanager = FindObjectOfType<GameManager>();
            GameObject camera = GameObject.Find("Main Camera");
            GameObject Player = GameObject.Find("Player");
            GameObject Sha = GameObject.Find("Sha");
            camera.GetComponent<CameraController>().IsFollowing=false;
            if (Sha.GetComponent<FollowPlayer>().playerFound)
            {
                Sha.transform.position = teleportationpoint.transform.position;                             
            }

            if (teleportationpoint.transform.position.x < 30)
            {
                gamemanager.GetComponent<GameManager>().level = 0;
            }
            else
            {
                gamemanager.GetComponent<GameManager>().level = 1;
            }
            Player.transform.position= teleportationpoint.transform.position/*new Vector3(47.02f,-7.58f,-1.55f)*/;
            
            camera.GetComponent<CameraController>().changeCadre();
            Debug.Log(gamemanager.GetComponent<GameManager>().level);
        }
    }
}
