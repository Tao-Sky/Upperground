using UnityEngine;
using System.Collections;

public class EnemyPathing : MonoBehaviour {

    [HideInInspector]
    public bool facingRight = true;

    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public Transform groundCheck;          // Whether or not the player is grounded.
    public LayerMask ground_layer;

    public int trip = 10;

    public bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;

    public float walkSpeed = 2.0f;
    public float walkingDirection = 1.0f;
    Vector3 walkAmount;

    // pour la detection
    public GameObject particuleDetection;

    // Use this for initialization
    void Awake()
    {
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
        bc2d = GetComponent<BoxCollider2D>();
        //rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        //Vector2 feet = new Vector2(transform.position.x, transform.position.y - 1f/*- GetComponent<BoxCollider2D>().bounds.extents.y*/);
        Vector2 feet = new Vector2(transform.position.x, bc2d.bounds.min.y-0.5f);
        grounded = Physics2D.OverlapCircle(feet, 0.2f, ground_layer);

        //Debug.Log(grounded);

        walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
        transform.Translate(walkAmount);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameObject[] listeCP = GameObject.FindGameObjectsWithTag("Checkpoint");

            foreach (GameObject g in listeCP)
            {
                if (g.GetComponent<TriggerCheckpoint>().getIsActivated() == true)
                {
                    coll.gameObject.GetComponent<PlayerController>().isRespawning = true;
                    coll.transform.position = new Vector3(g.transform.position.x, g.transform.position.y - 4.0f, coll.transform.position.z);
                    GameObject s = GameObject.Find("Sha");
                    if (s.GetComponent<FollowPlayer>().playerFound == true && s.GetComponent<FollowPlayer>().nocoroutine == true)
                        s.transform.position = new Vector3(g.transform.position.x, g.transform.position.y - 4.0f, s.transform.position.z);

                    break;
                }
            }
        }
    }

    void FixedUpdate()
    {
        //anim.SetFloat("Speed", Mathf.Abs(walkAmount.x));
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void CanBeAttacked(bool attack)
    {
        particuleDetection.SetActive(attack);
    }
}
