﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FollowPlayer : MonoBehaviour
{

    public static FollowPlayer uniqueSha;//creation d'un singleton pour la persistance (voir le Awake)

    private Vector3 positionDepart;

    public Transform target;//set target from inspector instead of looking in Update
    public float speed = 4.0f;
    public float basicSpeed = 4.0f;
    public float minDistanceFromPlayer = 1;

    private float lastDist;
    private float dist;
    private float maxDist = 4.0f;

    private Vector3 lastDirection;
    private Vector3 direction;

    public bool PowersAvailable = false;
    public bool playerFound = false;

    bool inCanalisation = false;
    bool canalisationCalled = false;
    Transform[] tabWayPoints;
    int nbWayPoints;
    int indiceNextWayPoint;

    //pour la coroutine
    public bool nocoroutine = true;
	private bool boolRencontre = false;
	private bool boolAttaque = false;
	private bool coroutineAttaque = false;

    public GameObject ps;

    //les particules de pouvoir de sha
    public GameObject spShaEnergie;
    public GameObject spShaFeu;
    public GameObject spShaGlace;
    public GameObject spShaAcide;

    //les attaques de sha
    public GameObject speclair;
	public GameObject sfeu;
	public GameObject sglace;
	public GameObject sacide;
    public GameObject speclairlong;

    //pour le jeu global
    public int PowerUnlocked = 0;

    void Start()
    {
        positionDepart = transform.position;
		if (SceneManager.GetActiveScene ().buildIndex == 2) {
			PowerUnlocked = 3;
		}
    }

    void Awake()
    {
        // DECOMMENTER POUR TEST DIRECT DANS NIVEAU 2
        //Invoke("startingCanalisation", 3);
        //PowersAvailable = true;
        //PowerUnlocked = 3;

        /*if (GameObject.Find("GameManager").GetComponent<GameManager>().level > 1)
        {
            PowersAvailable = true;
            PowerUnlocked = 3;
        }*/
    }

    void startingCanalisation()
    {
        inCanalisation = true;
        playerFound = false;
    
        GameObject g = GameObject.Find("ShaWayPoints");
        nbWayPoints = g.transform.childCount;
        tabWayPoints = new Transform[nbWayPoints];

        for (int i = 0; i < nbWayPoints; i++)
        {
            tabWayPoints[i] = g.transform.GetChild(i);
        }

        indiceNextWayPoint = 0;
        direction = new Vector3(speed * Time.deltaTime, 0, 0);
        lastDirection = direction;
        transform.LookAt(new Vector3(tabWayPoints[0].position.x, tabWayPoints[0].position.y, transform.position.z));
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
    }

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
		if(coroutineAttaque)
		{
			GameObject.Find ("Main Camera").GetComponent<Animator> ().SetBool ("zoom", true);
			coroutineAttaque = false;
			StartCoroutine(CinematicAttaque());
		}

		if(Input.GetButtonDown("X button") && boolRencontre)
		{
			StopCoroutine (CinematicRencontre());

			GameObject.Find ("Passer").GetComponent<SpriteRenderer> ().enabled = false;
			boolRencontre = false;
			nocoroutine = true;
			GameObject.Find("Player").GetComponent<PlayerController>().canmove = true;
			GameObject.Find("Dialogue").GetComponent<Animator>().SetBool("isPlaying", false);
			GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("zoom", false);
			playerFound = true;
		}

		else if(Input.GetButtonDown("X button") && boolAttaque)
		{
			StopCoroutine (CinematicAttaque());

			GameObject.Find ("Passer").GetComponent<SpriteRenderer> ().enabled = false;
			boolAttaque = false;
			nocoroutine = true;
			GameObject.Find("Player").GetComponent<PlayerController>().canmove = true;
			GameObject.Find("Dialogue").GetComponent<Animator>().SetBool("attaque", false);
			GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("zoom", false);
		}
		
        GameObject manager = GameObject.Find("GameManager");
        manager.hideFlags = HideFlags.HideInHierarchy;

        if (manager.GetComponent<GameManager>().getLevel() == 2 && !canalisationCalled)
        {
            Invoke("startingCanalisation", 3);
            canalisationCalled = true;
        }

        if (inCanalisation)
        {
            if (manager.GetComponent<GameManager>().IsPaused == true)
            {
                direction = lastDirection;
            }

            else
            {
                if (isOnNextWayPoint(indiceNextWayPoint))
                {
                    Transform nextWayPoint = tabWayPoints[indiceNextWayPoint];

                    if (indiceNextWayPoint < tabWayPoints.Length - 1)
                    {
                        indiceNextWayPoint++;
                        nextWayPoint = tabWayPoints[indiceNextWayPoint];
                    }

                    else
                    {
                        inCanalisation = false;
                        //playerFound = true;
                    }

                    transform.LookAt(new Vector3(nextWayPoint.position.x, nextWayPoint.position.y, transform.position.z));
                    transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                }

                if (Time.timeScale == 0f)
                    transform.Translate(lastDirection);
                else
                {
                    Debug.Log(speed);
                    direction = new Vector3(speed * Time.deltaTime, 0, 0);
                    transform.Translate(direction);
                    lastDirection = direction;
                }                    
            }
        }

        else
        {
            dist = Vector3.Distance(transform.position, target.position);

            if (/*dist < 10 &&*/ nocoroutine && playerFound)
            {
                MoveTowardsPlayer();
                //playerFound = true;
            }

            lastDist = dist;
        }
    }
    /*
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
    */
    public void MoveTowardsPlayer()
    {
        transform.LookAt(new Vector3(target.position.x, target.position.y, transform.position.z));
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        //ajout par arthur pour corriger le system de particule
        if (target.position.x > transform.position.x)
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

        if (dist > minDistanceFromPlayer)
        {
            speed = dist + dist / 5;

            if (speed < basicSpeed)
                speed = basicSpeed;

            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
    }

    public void goToMachine(Vector3 cible, float time, int appel)
    {
        StartCoroutine(MachineCoroutine(transform, cible, time, appel));
    }

	public void launchCinematicAttaque()
	{
		StartCoroutine (CinematicAttaque ());
	}

	public void launchCinematicRencontre()
	{
		StartCoroutine (CinematicRencontre ());
	}

    IEnumerator MachineCoroutine(Transform target, Vector3 centremachine, float time, int appel)
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
            Machine.GetComponent<Animator>().SetBool("run", true);
            GameObject.Find("Machine").GetComponent<MachineSFX>().Machine();
            GameObject.Find("Main Camera").GetComponent<ChangeColor>().violet = true;
            GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("unzoom", true);

			GameObject player = GameObject.Find ("Player");
			player.GetComponent<PlayerController>().canmove = false;

			if(player.transform.position.x > -10.55f && player.transform.localScale.x > 0)
			{
				player.GetComponent<PlayerController> ().Flip ();
			}
			else if(player.transform.position.x < -10.55f && player.transform.localScale.x < 0)
			{
				player.GetComponent<PlayerController> ().Flip ();
			}


        }

        else if (appel == 2)
        {/*
            transform.LookAt(centremachine);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            */
            GetComponent<ShaSFX>().PowerEclair();
            speclairlong.transform.LookAt(centremachine);
            speclairlong.GetComponent<ParticleSystem>().startSize = Mathf.Sqrt(Vector2.Distance(centremachine, target.position)) * 0.8f;//la taille du rayon reste a definir avec un fontion propre
            speclairlong.SetActive(true);
            speclairlong.GetComponent<ParticleSystem>().Play();
            //lui faire lancer un rayon
        }

        //Debug.Log("je suis a destination");
        yield return new WaitForSeconds(time);
        if (appel == 1)
        {
            ps.SetActive(false);
            PowerUnlocked = 1;
            spShaEnergie.SetActive(true);
            Machine.GetComponent<Animator>().SetBool("run", false);
            GameObject.Find("Main Camera").GetComponent<ChangeColor>().violet = false;
            GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("unzoom", false);
            PowersAvailable = true;
			coroutineAttaque = true;

        }
        else if (appel == 2)
        {
            speclairlong.GetComponent<ParticleSystem>().Stop();
            speclairlong.SetActive(false);
			nocoroutine = true;

            //areter de lancer un rayon
        }
        playerFound = true;
        //Debug.Log("je suis enfin fini");
    }

    public void LaunchPower(int nbP, Transform T)
    {
        GetComponent<ShaSFX>().Scream();

		switch (nbP)
		{
			case 0:
			{
				speclair.transform.LookAt(T);
				speclair.GetComponent<ParticleSystem>().startSize = Mathf.Sqrt(Vector2.Distance(T.position, this.transform.position)) * 0.8f;//la taille du rayon reste a definir avec un fontion propre
				speclair.GetComponent<ParticleSystem>().Play();
				GetComponent<ShaSFX>().Thunder();

				break;
			}

			case 1:
			{
				sfeu.transform.LookAt(T);
				sfeu.GetComponent<ParticleSystem>().startSize = Mathf.Sqrt(Vector2.Distance(T.position, this.transform.position)) * 0.8f;//la taille du rayon reste a definir avec un fontion propre
				sfeu.GetComponent<ParticleSystem>().Play();
				GetComponent<ShaSFX>().Fire();

				break;
			}

			case 2:
			{
				sacide.transform.LookAt(T);
				sacide.GetComponent<ParticleSystem>().startSize = Mathf.Sqrt(Vector2.Distance(T.position, this.transform.position)) * 0.8f;//la taille du rayon reste a definir avec un fontion propre
				sacide.GetComponent<ParticleSystem>().Play();
				GetComponent<ShaSFX>().Acid();

				break;
			}

			case 3:
			{
				sglace.transform.LookAt(T);
				sglace.GetComponent<ParticleSystem>().startSize = Mathf.Sqrt(Vector2.Distance(T.position, this.transform.position)) * 0.8f;//la taille du rayon reste a definir avec un fontion propre
				sglace.GetComponent<ParticleSystem>().Play();
				GetComponent<ShaSFX>().Ice();

				break;
			}


		}


    }

    public void powerParticule(int power)
    {
        Animator A = GameObject.Find("Sha").GetComponent<Animator>();
        if (power == 0 && PowerUnlocked > 0)// power unlock sert a savoir si le pouvoir a deja ete decouvert on peut donc decier dans quel ordre on decouvre les pouvoir en changenant a partir de quand on affiche les anims
        {
            A.SetInteger("state", 0);
            spShaFeu.SetActive(false);
            spShaGlace.SetActive(false);
            spShaAcide.SetActive(false);
            spShaEnergie.SetActive(true);
        }
        if (power == 1 && PowerUnlocked > 1)// power unlock sert a savoir si le pouvoir a deja ete decouvert on peut donc decier dans quel ordre on decouvre les pouvoir en changenant a partir de quand on affiche les anims
        {
            A.SetInteger("state", 1);
            spShaEnergie.SetActive(false);
            spShaGlace.SetActive(false);
            spShaAcide.SetActive(false);
            spShaFeu.SetActive(true);
        }
        if (power == 2 && PowerUnlocked > 2)// power unlock sert a savoir si le pouvoir a deja ete decouvert on peut donc decier dans quel ordre on decouvre les pouvoir en changenant a partir de quand on affiche les anims
        {
            A.SetInteger("state", 2);
            spShaFeu.SetActive(false);
            spShaGlace.SetActive(false);
            spShaEnergie.SetActive(false);
            spShaAcide.SetActive(true);
        }
        if (power == 3 && PowerUnlocked > 3)// power unlock sert a savoir si le pouvoir a deja ete decouvert on peut donc decier dans quel ordre on decouvre les pouvoir en changenant a partir de quand on affiche les anims
        {
            A.SetInteger("state", 3);
            spShaFeu.SetActive(false);
            spShaAcide.SetActive(false);
            spShaEnergie.SetActive(false);
            spShaGlace.SetActive(true);
        }

        if(power == 3 && PowerUnlocked < 4)
        {
            spShaFeu.SetActive(false);
            spShaAcide.SetActive(false);
            spShaEnergie.SetActive(false);
            spShaGlace.SetActive(false);
        }
        if (power == 2 && PowerUnlocked < 3)
        {
            spShaFeu.SetActive(false);
            spShaAcide.SetActive(false);
            spShaEnergie.SetActive(false);
            spShaGlace.SetActive(false);
        }
        if (power == 1 && PowerUnlocked < 2)
        {
            spShaFeu.SetActive(false);
            spShaAcide.SetActive(false);
            spShaEnergie.SetActive(false);
            spShaGlace.SetActive(false);
        }
        if (power == 0 && PowerUnlocked < 1)
        {
            spShaFeu.SetActive(false);
            spShaAcide.SetActive(false);
            spShaEnergie.SetActive(false);
            spShaGlace.SetActive(false);
        }


    }

    public IEnumerator CinematicRencontre()
    {
		boolRencontre = true;
        Animator A = GameObject.Find("Dialogue").GetComponent<Animator>();
        A.SetBool("isPlaying", true);
        GameObject sha = GameObject.Find("Sha");
        GameObject player = GameObject.Find("Player");
        Animator APlayer = player.GetComponent<Animator>();
        APlayer.SetBool("grounded", true);
        APlayer.SetBool("up", true);
        APlayer.SetFloat("speed", 0.0f);

        yield return new WaitForSeconds(12.5f);

		if(boolRencontre) 
		{
			nocoroutine = true;
		}
        player.GetComponent<PlayerController>().canmove = true;
        A.SetBool("isPlaying", false);
        GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("zoom", false);
        playerFound = true;
		boolRencontre = false;
    }

    public IEnumerator CinematicAttaque()
    {
		nocoroutine = false;
		boolAttaque = true;
        Animator A = GameObject.Find("Dialogue").GetComponent<Animator>();
        A.SetBool("attaque", true);
        GameObject sha = GameObject.Find("Sha");
        GameObject player = GameObject.Find("Player");
        Animator APlayer = player.GetComponent<Animator>();
		player.GetComponent<PlayerController>().canmove = false;

        APlayer.SetBool("grounded", true);
        APlayer.SetBool("up", true);
        APlayer.SetFloat("speed", 0.0f);

        yield return new WaitForSeconds(12.5f);

		if(boolAttaque)
		{
			nocoroutine = true;
		}
        player.GetComponent<PlayerController>().canmove = true;
        A.SetBool("attaque", false);
        GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("zoom", false);
        playerFound = true;
		boolAttaque = false;

    }
    
    bool isOnNextWayPoint(int indiceNextWayPoint)
    {
        float ecart = 0.05f * speed / 2;

        if (Mathf.Abs(transform.position.x - tabWayPoints[indiceNextWayPoint].transform.position.x) < ecart
         && Mathf.Abs(transform.position.y - tabWayPoints[indiceNextWayPoint].transform.position.y) < ecart)
        {
            return true;
        }

        else
            return false;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !inCanalisation && !playerFound && GameObject.Find("GameManager").GetComponent<GameManager>().level > 1)
        {
            playerFound = true;
        }
        if (other.gameObject.tag == "Enemy")
        {
            if (PowersAvailable)
            {
                other.gameObject.GetComponent<EnemyFight>().CanBeAttacked(true);
                if (other.gameObject.GetComponent<EnemyFight>().getHealthPoints() == other.gameObject.GetComponent<EnemyFight>().totalHealth)
                    other.gameObject.GetComponent<EnemyFight>().showHealthBar(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
            if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyFight>().CanBeAttacked(false);
            if (other.gameObject.GetComponent<EnemyFight>().getHealthPoints() == other.gameObject.GetComponent<EnemyFight>().totalHealth)
                other.gameObject.GetComponent<EnemyFight>().showHealthBar(false);
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !inCanalisation && !playerFound &&  GameObject.Find("GameManager").GetComponent<GameManager>().level > 1)
        {
            playerFound = true;
        }
        GameObject sha = GameObject.Find("Sha");
        if (other.gameObject.tag == "Enemy")
        {
			int power = GameObject.Find("Player").GetComponent<PlayerController> ().power;

			if (Input.GetButtonDown("B button") && sha.GetComponent<FollowPlayer>().playerFound && PowersAvailable && power <= PowerUnlocked-1  )
            {
                other.gameObject.GetComponent<EnemyFight>().takingDamage();
                LaunchPower(power, other.transform);
            }
        }
    }

    public void Respawn()
    {
        transform.position = positionDepart;
        GameObject manager = GameObject.Find("GameManager");
        manager.hideFlags = HideFlags.HideInHierarchy;

        if (manager.GetComponent<GameManager>().getLevel() == 2)
        {
            indiceNextWayPoint = 0;
            playerFound = false;
            canalisationCalled = false;
            speed = basicSpeed;
            transform.LookAt(new Vector3(tabWayPoints[0].position.x, tabWayPoints[0].position.y, transform.position.z));
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        }
    }
}
