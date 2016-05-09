using UnityEngine;
using System.Collections;

public class EnemyPathing : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;

    private Animator anim;
    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;

    public float walkSpeed = 2.0f;
    public float walkingDirection = 1.0f;
    Vector3 walkAmount;


    void Awake()
    {
        anim = GetComponent<Animator>();
        bc2d = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
        transform.Translate(walkAmount);
    }

    public float getWalkingDirection()
    {
        return walkingDirection;
    }

    public float getWalkSpeed()
    {
        return walkSpeed;
    }

    public void setWalkSpeed(float f)
    {
        walkSpeed = f;
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
