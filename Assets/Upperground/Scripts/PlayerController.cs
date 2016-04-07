using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController uniquePlayer;//creation d'un singleton pour la persistance (voir le Awake)
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;               // Condition for whether the player should jump.
    public bool isRespawning = false;

    public float moveForce = 365f;          // Amount of force added to move the player left and right.
    public float maxSpeed = 5f;             // The fastest the player can travel in the x axis.
    public float jumpForce = 1000f;         // Amount of force added when the player jumps.
    public GameObject playerobject;
    private Rigidbody2D rb2d;

    private Transform groundCheck;          // A position marking where to check if the player is grounded.
    public bool grounded = false;          // Whether or not the player is grounded.
    public LayerMask ground_layer;
    public Animator anim;                  // Reference to the player's animator component.

    //pour l'hud
    public GameObject rouepouvoir;
    private bool swap = true;

    //pouvoirs
    public int power = 0;

    void Awake()
    {
        /*if (uniquePlayer == null)
        {
            DontDestroyOnLoad(gameObject);
            uniquePlayer = this;
        }
        else if (uniquePlayer != this)
        {
            Destroy(gameObject);
        }*/
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyPathing>().CanBeAttacked(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyPathing>().CanBeAttacked(false);
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        GameObject sha = GameObject.Find("Sha");
        if (other.gameObject.tag == "Enemy")
        {
            if (Input.GetButtonDown("B button") && sha.GetComponent<FollowPlayer>().playerFound)
            {

                Debug.Log("j'attaque");
            }
        }
    }


        void Update()
    {
        GameObject sha = GameObject.Find("Sha");//pour aciver le bon pouvoir sur sha
        Vector2 feet = new Vector2(transform.position.x, transform.position.y - 1f/*- GetComponent<BoxCollider2D>().bounds.extents.y*/);
        grounded = Physics2D.OverlapCircle(feet, 0.2f, ground_layer);

        //Debug.Log(grounded + " " + feet);
        if ((Input.GetButtonDown("A button") /*|| Input.GetButtonDown("Jump")*/) && grounded)
            jump = true;

        if (grounded)
        {
            anim.SetBool("grounded", true);
            isRespawning = false;
        }
        else
        {
            anim.SetBool("grounded", false);
            if (rb2d.velocity.y > 0)
            {
                anim.SetBool("up", true);
            }
            else
            {
                anim.SetBool("up", false);
            }
        }

        //pour l'hud
        if (Input.GetAxis("gachette gauche") < 0.2 && Input.GetAxis("gachette droite") < 0.2)
        {
            swap = true;
            
        }

        if (Input.GetAxis("gachette droite") > 0.2 && swap)
        {
            if (power == 0)
            {
                power = 3;
            }
            else
            {
                power = power - 1;
            }
            //Debug.Log("test pouvoir " + power);
            swap = false;
            rouepouvoir.transform.Rotate(0, 0, -90);
            sha.GetComponent<FollowPlayer>().powerParticule(power);
        }

        if (Input.GetAxis("gachette gauche") > 0.2 && swap)
        {
            power = Mathf.Abs((power + 1) % 4);
            //Debug.Log("test pouvoir " + power);
            swap = false;
            rouepouvoir.transform.Rotate(0, 0, 90);
            sha.GetComponent<FollowPlayer>().powerParticule(power);
        }
    }


    void FixedUpdate()
    {
        if (!isRespawning)
        {
            //rb2d.WakeUp();
            float h = Input.GetAxis("Horizontal");
            //Debug.Log(h);
            // The Speed animator parameter is set to the absolute value of the horizontal input.
            anim.SetFloat("speed", Mathf.Abs(h));

            // If the player is changing direction (h has a different sign to velocity.x) or not max speed
            if (h * rb2d.velocity.x < maxSpeed)
                rb2d.AddForce(Vector2.right * h * moveForce);

            // If the player's horizontal velocity is greater than the maxSpeed...
            if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
                rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

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
                rb2d.AddForce(new Vector2(0f, jumpForce));

                // Make sure the player can't jump again until the jump conditions from Update are satisfied.
                jump = false;
            }
        }

        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
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
