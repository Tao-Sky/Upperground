using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;               // Condition for whether the player should jump.


    public float moveForce = 365f;          // Amount of force added to move the player left and right.
    public float maxSpeed = 5f;             // The fastest the player can travel in the x axis.
    public float jumpForce = 1000f;         // Amount of force added when the player jumps.
    public GameObject playerobject;

    private Transform groundCheck;          // A position marking where to check if the player is grounded.
    public bool grounded = false;          // Whether or not the player is grounded.
    public LayerMask ground_layer;
    public Animator anim;                  // Reference to the player's animator component.


    void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        Vector2 feet = new Vector2(transform.position.x, transform.position.y - 1f/*- GetComponent<BoxCollider2D>().bounds.extents.y*/);
        grounded = Physics2D.OverlapCircle(feet, 0.2f, ground_layer);

        //Debug.Log(grounded + " " + feet);
        if (Input.GetButtonDown("Jump") && grounded)
			jump = true;
		
		if(grounded) 
		{
			anim.SetBool("grounded", true);
		}
		else 
		{
			anim.SetBool("grounded", false);
			if (GetComponent<Rigidbody2D> ().velocity.y > 0) 
			{
				anim.SetBool ("up", true);
			}
			else 
			{
				anim.SetBool("up", false);
			}
		}
    }


    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        //Debug.Log(h);
        // The Speed animator parameter is set to the absolute value of the horizontal input.
        anim.SetFloat("speed", Mathf.Abs(h));

        // If the player is changing direction (h has a different sign to velocity.x) or not max speed
        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
            //flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
            //flip the player.
            Flip();


        if (jump)
        {
            // Set the Jump animator trigger parameter.
            // anim.SetTrigger("Jump");


            // Add a vertical force to the player.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
    }


    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}
