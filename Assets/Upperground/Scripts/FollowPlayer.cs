﻿using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public static FollowPlayer uniqueSha;//creation d'un singleton pour la persistance (voir le Awake)

    public Transform target;//set target from inspector instead of looking in Update
    public float speed = 4.0f;
    public float basicSpeed = 4.0f;
    public float minDistanceFromPlayer = 1;

    private float lastDist;
    private float dist;
    private Vector3 lastDestination;

    bool inPlayerRadius = false;
    public bool playerFound = false;

    //pour la coroutine
    public bool nocoroutine = true;
    public GameObject ps;

    //les particules de pouvoir de sha
    public GameObject spShaEnergie;
    public GameObject spShaFeu;
    public GameObject spShaGlace;
    public GameObject spShaAcide;

    //les attaques de sha
    public GameObject speclair;

    //pour le jeu global
    public int PowerUnlocked=0;

    // mise en place de la persistance de Sha
    /*void awake()
    {
        if(uniqueSha == null)
        {
            DontDestroyOnLoad(gameObject);
            uniqueSha = this;
        }
        else if (uniqueSha !=this)
        {
            Destroy(gameObject);
        }


    }*/

    void Update()
    {
        dist = Vector3.Distance(transform.position, target.position);

        if (!inPlayerRadius && dist < 10 && nocoroutine)
        {
            MoveTowardsPlayer();
            playerFound = true;
        }
        else
        {
            playerFound = false;
        }

        lastDist = dist;
        lastDestination = transform.forward;
    }

    public void MoveRandom()
    {
        float f = Random.Range(0.1f, 0.2f);
        if (Random.value > 0.5)
            f *= -1;

        Debug.Log(target.position);
        transform.LookAt(new Vector3(lastDestination.x + f, lastDestination.y + f, 0));
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        //Vector3 newVector = new Vector3((float)(Random.value - 0.5) * speed * Time.deltaTime, 0, 0);
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }

    public void MoveTowardsPlayer()
    {
		transform.LookAt(new Vector3(target.position.x,target.position.y,transform.position.z));
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        //ajout par arthur pour corriger le system de particule
        if(target.position.x > transform.position.x)
        {
            spShaEnergie.transform.localPosition = new Vector3(0, 0, 0.5f);
            spShaFeu.transform.localPosition = new Vector3(0, 0, 0.5f);
            spShaGlace.transform.localPosition = new Vector3(0, 0, 0.5f);
            spShaAcide.transform.localPosition = new Vector3(0, 0, 0.5f);

        }
        else
        {
            spShaEnergie.transform.localPosition = new Vector3(0, 0, -0.5f);
            spShaFeu.transform.localPosition = new Vector3(0, 0, -0.5f);
            spShaGlace.transform.localPosition = new Vector3(0, 0, -0.5f);
            spShaAcide.transform.localPosition = new Vector3(0, 0, -0.5f);
        }
        //fin d'ajout

        if (dist > 5)
        {
            speed *= 1.01f;
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }

        if (dist > minDistanceFromPlayer && dist <= 5)
        {
            if (dist <= lastDist)
                speed *= 0.975f;

            if (speed < basicSpeed)
                speed = basicSpeed;
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
    }

    public void setInPlayerRadius(bool b)
    {
        inPlayerRadius = b;
    }

    public void goToMachine(Vector3 cible,float time,int appel)
    {
        StartCoroutine(MachineCoroutine(transform,cible,time,appel));
    }


    IEnumerator MachineCoroutine(Transform target,Vector3 centremachine,float time,int appel)
    {
        nocoroutine = false;
		GameObject Machine = GameObject.Find("Machine");
        if (appel != 2)
        {

            while (Vector3.Distance(centremachine, target.position) > 0.1f)
            {
                target.position = Vector3.Lerp(centremachine, target.position, 59.0f * Time.deltaTime);
                yield return null;
            }
        }
        if (appel == 1)
        {
            ps.SetActive(true);
			Machine.GetComponent<Animator> ().SetBool("run",true);
        }
        if(appel == 2)
        {
            transform.LookAt(centremachine);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            speclair.GetComponent<ParticleSystem>().startSize = Mathf.Sqrt( Vector2.Distance(centremachine, target.position)) * 0.8f;//la taille du rayon reste a definir avec un fontion propre
            speclair.SetActive(true);
            speclair.GetComponent<ParticleSystem>().Play();
            //lui faire lancer un rayon
        }

        //Debug.Log("je suis a destination");
        yield return new WaitForSeconds(time);
        if (appel == 1)
        {
            ps.SetActive(false);
            PowerUnlocked = 1;
            spShaEnergie.SetActive(true);
			Machine.GetComponent<Animator> ().SetBool("run",false);

        }
        if (appel == 2)
        {
            speclair.GetComponent<ParticleSystem>().Stop();
            speclair.SetActive(false);
            //areter de lancer un rayon
        }
        nocoroutine = true;
        //Debug.Log("je suis enfin fini");
    }

    public void powerParticule(int power)
    {
        if (power == 0 && PowerUnlocked>0)// power unlock sert a savoir si le pouvoir a deja ete decouvert on peut donc decier dans quel ordre on decouvre les pouvoir en changenant a partir de quand on affiche les anims
        {
            spShaFeu.SetActive(false);
            spShaGlace.SetActive(false);
            spShaAcide.SetActive(false);
            spShaEnergie.SetActive(true);
            
        }
        if (power == 1 && PowerUnlocked > 0)// power unlock sert a savoir si le pouvoir a deja ete decouvert on peut donc decier dans quel ordre on decouvre les pouvoir en changenant a partir de quand on affiche les anims
        {
            spShaEnergie.SetActive(false);
            spShaGlace.SetActive(false);
            spShaAcide.SetActive(false);
            spShaFeu.SetActive(true);

        }
        if (power == 2 && PowerUnlocked > 0)// power unlock sert a savoir si le pouvoir a deja ete decouvert on peut donc decier dans quel ordre on decouvre les pouvoir en changenant a partir de quand on affiche les anims
        {
            spShaFeu.SetActive(false);
            spShaGlace.SetActive(false);
            spShaEnergie.SetActive(false);
            spShaAcide.SetActive(true);
            

        }
        if (power == 3 && PowerUnlocked > 0)// power unlock sert a savoir si le pouvoir a deja ete decouvert on peut donc decier dans quel ordre on decouvre les pouvoir en changenant a partir de quand on affiche les anims
        {
            spShaFeu.SetActive(false);
            spShaAcide.SetActive(false);
            spShaEnergie.SetActive(false);
            spShaGlace.SetActive(true);


        }

    }
}
