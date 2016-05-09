using UnityEngine;
using System.Collections;

public class EnemyAcceleration : MonoBehaviour
{

    private float accTime = 1.0f;
    private bool canAccelerate;

    private Animator anim;

    void Start()
    {
        Invoke("MovementSpeedBoost", 1);
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (canAccelerate)
        {
            accTime -= Time.deltaTime;
            if (accTime <= 0)
            {
                canAccelerate = false;
                accTime = 1.0f;
                GetComponent<EnemyPathing>().setWalkSpeed(2.0f);
                anim.speed = 1f;
            }
        }
    }

    void MovementSpeedBoost()
    {
        float randomTime = Random.Range(3.0f, 5.0f);
        canAccelerate = true;

        GetComponent<EnemyPathing>().setWalkSpeed(4.5f);
        anim.speed = 2f;

        Invoke("MovementSpeedBoost", randomTime);
    }
}
