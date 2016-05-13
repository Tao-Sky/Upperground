using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{

    public static FollowPlayer uniqueSha;//creation d'un singleton pour la persistance (voir le Awake)

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

    bool inPlayerRadius = false;
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

    public GameObject ps;

    //les particules de pouvoir de sha
    public GameObject spShaEnergie;
    public GameObject spShaFeu;
    public GameObject spShaGlace;
    public GameObject spShaAcide;

    //les attaques de sha
    public GameObject speclair;
    public GameObject speclairlong;

    //pour le jeu global
    public int PowerUnlocked = 0;

    void Awake()
    {
        // DECOMMENTER POUR TEST DIRECT DANS NIVEAU 2
        //Invoke("startingCanalisation", 3);
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
			boolRencontre = false;
			nocoroutine = true;
			GameObject.Find("Player").GetComponent<PlayerController>().canmove = true;
			GameObject.Find("Dialogue").GetComponent<Animator>().SetBool("isPlaying", false);
			GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("zoom", false);
			playerFound = true;
		}
		
        GameObject manager = GameObject.Find("GameManager");
        manager.hideFlags = HideFlags.HideInHierarchy;

        //Debug.Log(manager.GetComponent<GameManager>().getLevel());

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
                        playerFound = true;
                    }

                    transform.LookAt(new Vector3(nextWayPoint.position.x, nextWayPoint.position.y, transform.position.z));
                    transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                }

                if (Time.timeScale == 1f)
                    transform.Translate(lastDirection);
                else
                    transform.Translate(direction);
            }
        }

        else
        {
            dist = Vector3.Distance(transform.position, target.position);

            if (!inPlayerRadius && dist < 10 && nocoroutine && playerFound)
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

        if (dist > maxDist)
        {
            speed *= 1.01f;
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }

        if (dist > minDistanceFromPlayer && dist <= maxDist)
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

    public void goToMachine(Vector3 cible, float time, int appel)
    {
        StartCoroutine(MachineCoroutine(transform, cible, time, appel));
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
			GameObject.Find("Player").GetComponent<PlayerController>().canmove = false;
            ps.SetActive(true);
            Machine.GetComponent<Animator>().SetBool("run", true);
            GameObject.Find("Machine").GetComponent<MachineSFX>().Machine();
            GameObject.Find("Main Camera").GetComponent<ChangeColor>().violet = true;
            GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("unzoom", true);
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
            //GameObject.Find("Player").GetComponent<PlayerController>().canmove = true;
            GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("unzoom", false);
            PowersAvailable = true;

			//Déclanchement de la couroutine de cinématique d'attaque

			GameObject sha = GameObject.Find("Sha");
			GameObject player = GameObject.Find("Player");
			sha.GetComponent<FollowPlayer>().nocoroutine = false;

			if (sha.transform.position.x > player.transform.position.x && player.transform.localScale.x < 0)
			{
				player.GetComponent<PlayerController> ().Flip ();
			}
			else if(sha.transform.position.x < player.transform.position.x && player.transform.localScale.x > 0)
			{
				player.GetComponent<PlayerController> ().Flip ();
			}

			GameObject.Find ("Main Camera").GetComponent<Animator> ().SetBool ("zoom", true);
			StartCoroutine(sha.GetComponent<FollowPlayer>().CinematicAttaque());


        }
        else if (appel == 2)
        {
            speclairlong.GetComponent<ParticleSystem>().Stop();
            speclairlong.SetActive(false);
            //areter de lancer un rayon
        }

        nocoroutine = true;
        playerFound = true;
        //Debug.Log("je suis enfin fini");
    }

    public void LaunchPower(int nbP, Transform T)
    {
        GetComponent<ShaSFX>().Scream();
        GetComponent<ShaSFX>().Thunder();
        speclair.transform.LookAt(T);
        speclair.GetComponent<ParticleSystem>().startSize = Mathf.Sqrt(Vector2.Distance(T.position, this.transform.position)) * 0.8f;//la taille du rayon reste a definir avec un fontion propre
        speclair.GetComponent<ParticleSystem>().Play();
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
        if (power == 1 && PowerUnlocked > 0)// power unlock sert a savoir si le pouvoir a deja ete decouvert on peut donc decier dans quel ordre on decouvre les pouvoir en changenant a partir de quand on affiche les anims
        {
            A.SetInteger("state", 1);
            spShaEnergie.SetActive(false);
            spShaGlace.SetActive(false);
            spShaAcide.SetActive(false);
            spShaFeu.SetActive(true);
        }
        if (power == 2 && PowerUnlocked > 0)// power unlock sert a savoir si le pouvoir a deja ete decouvert on peut donc decier dans quel ordre on decouvre les pouvoir en changenant a partir de quand on affiche les anims
        {
            A.SetInteger("state", 2);
            spShaFeu.SetActive(false);
            spShaGlace.SetActive(false);
            spShaEnergie.SetActive(false);
            spShaAcide.SetActive(true);
        }
        if (power == 3 && PowerUnlocked > 0)// power unlock sert a savoir si le pouvoir a deja ete decouvert on peut donc decier dans quel ordre on decouvre les pouvoir en changenant a partir de quand on affiche les anims
        {
            A.SetInteger("state", 3);
            spShaFeu.SetActive(false);
            spShaAcide.SetActive(false);
            spShaEnergie.SetActive(false);
            spShaGlace.SetActive(true);
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

		boolRencontre = false;
        sha.GetComponent<FollowPlayer>().nocoroutine = true;
        player.GetComponent<PlayerController>().canmove = true;
        A.SetBool("isPlaying", false);
        GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("zoom", false);
        playerFound = true;

    }

    public IEnumerator CinematicAttaque()
    {
		boolAttaque = true;
        Animator A = GameObject.Find("Dialogue").GetComponent<Animator>();
        A.SetBool("attaque", true);
        GameObject sha = GameObject.Find("Sha");
        GameObject player = GameObject.Find("Player");
        Animator APlayer = player.GetComponent<Animator>();
        APlayer.SetBool("grounded", true);
        APlayer.SetBool("up", true);
        APlayer.SetFloat("speed", 0.0f);

        yield return new WaitForSeconds(12.5f);

		boolAttaque = false;
        sha.GetComponent<FollowPlayer>().nocoroutine = true;
        player.GetComponent<PlayerController>().canmove = true;
        A.SetBool("attaque", false);
        GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("zoom", false);
        playerFound = true;
    }

    bool isOnNextWayPoint(int indiceNextWayPoint)
    {
        float ecart = 0.05f;

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
        GameObject sha = GameObject.Find("Sha");
        if (other.gameObject.tag == "Enemy")
        {
            if (Input.GetButtonDown("B button") && sha.GetComponent<FollowPlayer>().playerFound && PowersAvailable)
            {
                other.gameObject.GetComponent<EnemyFight>().takingDamage();
                LaunchPower(0, other.transform);
                if (other.gameObject.GetComponent<EnemyFight>().getHealthPoints() == 0)
                    other.gameObject.GetComponent<EnemyFight>().EnemyDie();
            }
        }
    }
}
