using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

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
    public int appel =0;
    public GameObject systemeparticulesha;

    //pour le jeu global
    public int PowerUnlocked=0;

    void Start()
    {

    }

    void Update()
    {
        dist = Vector3.Distance(transform.position, target.position);

        if (!inPlayerRadius && dist < 10 && nocoroutine)
        {
            MoveTowardsPlayer();
            playerFound = true;
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

    public void goToMachine(Vector3 cible,float time)
    {
        StartCoroutine(MachineCoroutine(transform,cible,time));
    }


    IEnumerator MachineCoroutine(Transform target,Vector3 centremachine,float time)
    {
        nocoroutine = false;
		GameObject Machine = GameObject.Find("Machine");

        while (Vector3.Distance(centremachine, target.position) > 0.1f)
        {
            target.position = Vector3.Lerp(centremachine, target.position, 59.0f * Time.deltaTime);
            yield return null;
        }

        if (appel == 1)
        {
            ps.SetActive(true);
			Machine.GetComponent<Animator> ().SetBool("run",true);
        }
        if(appel == 2)
        {
            systemeparticulesha.transform.position = new Vector3(systemeparticulesha.transform.position.x, systemeparticulesha.transform.position.y, 1.0f);
        }

        //Debug.Log("je suis a destination");
        yield return new WaitForSeconds(time);
        if (appel == 1)
        {
            ps.SetActive(false);
            systemeparticulesha.SetActive(true);
			Machine.GetComponent<Animator> ().SetBool("run",false);

        }
        if (appel == 2)
        {
            systemeparticulesha.transform.position=new Vector3(systemeparticulesha.transform.position.x, systemeparticulesha.transform.position.y,0.5f);
        }
        nocoroutine = true;
        //Debug.Log("je suis enfin fini");
    }
}
