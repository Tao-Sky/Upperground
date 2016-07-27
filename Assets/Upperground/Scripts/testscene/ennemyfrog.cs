using UnityEngine;
using System.Collections;

public class ennemyfrog : MonoBehaviour {

    public LayerMask ground_layer;
    private bool grounded = false;
    private Animator anim;
    private bool facingRight = true;
    private float timer;
    public Vector2 jumpforce;
    public float timerBeforeJump;
    public float tonguedelay;
    private bool launched = false;
    private bool skipnextflip = false;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        timer = timerBeforeJump;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 feet = new Vector2(transform.position.x, transform.position.y-(GetComponent<BoxCollider2D>().size.y));
        grounded = Physics2D.OverlapCircle(feet, 0.2f, ground_layer);
        anim.SetBool("jump", !grounded);
        anim.SetBool("up", GetComponent<Rigidbody2D>().velocity.y > 0);
        if (!launched)
        {
            StartCoroutine(tongue());
        }       
        if (timer < 0.1)
        {
            if (Random.Range(0, 2) == 1 && !skipnextflip)
            {
                Flip();
            }
            skipnextflip = false;
            anim.SetTrigger("go");
            timer = timerBeforeJump;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    IEnumerator tongue()
    {
        launched = true;
        yield return new WaitForSeconds(tonguedelay);
        if (Random.Range(0, 7) > 4 )
        {
            GetComponentsInChildren<Animator>()[1].SetTrigger("shoot");
        }
        launched = false;
    }

    public void Flip()
    {

        facingRight = !facingRight;
        jumpforce.x *= -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void jump()
    {
        GetComponent<Rigidbody2D>().AddForce(jumpforce * 1000f);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

        }
        else
        {
            Flip();
            skipnextflip = true;
        }
    }
}
