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

    void Start()
    {

    }

    void Update()
    {
        dist = Vector3.Distance(transform.position, target.position);

        if (!inPlayerRadius && dist < 10)
        {
            MoveTowardsPlayer();
            playerFound = true;
            Debug.Log(playerFound);
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
        transform.LookAt(target.position);
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
}
