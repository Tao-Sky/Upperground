using UnityEngine;
using System.Collections;

public class EnemyFight : MonoBehaviour
{
    public SpriteRenderer healthBar;
    public GameObject particuleDetection;

    public Sprite FullHealth;
    public Sprite OneHitHealth;
    public Sprite TwoHitHealth;

    private BoxCollider2D box2D;
    private Animator anim;

    private SpriteRenderer[] sr;
    public GameObject deadEnemy;
    public int enemyType;

    private bool noCoroutine = true;

    public float totalHealth = 3;
    private float currentHealth;

    void Awake()
    {
        healthBar.enabled = false;
        currentHealth = totalHealth;

        anim = GetComponent<Animator>();
        box2D = GetComponent<BoxCollider2D>();
        sr = GetComponentsInChildren<SpriteRenderer>();

        if (enemyType != 2)
        {
            deadEnemy = null;
        }

        else if (enemyType == 2)
        {
            deadEnemy.GetComponent<SpriteRenderer>().enabled = false;
            deadEnemy.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        if (currentHealth < totalHealth)
            healthBar.enabled = true;
    }

    public void updateHealthBar(float h)
    {
        healthBar.enabled = true;
        if (h == 3)
            healthBar.sprite = FullHealth;
        else if (h == 2)
            healthBar.sprite = OneHitHealth;
        else if (h == 1)
            healthBar.sprite = TwoHitHealth;
		else if(h==0)
		{
			if(enemyType != 2)
			{
				DestroyObject (this.gameObject);
			}
			else if(enemyType == 2)
			{
				StartCoroutine(deathCoroutine(5.0f));
			}
		}
    }

    public void takingDamage()
    {
        if (currentHealth > 0)
            currentHealth--;
		
		updateHealthBar(currentHealth);
    }

    public float getHealthPoints()
    {
        return currentHealth;
    }

    public int getEnemyType()
    {
        return enemyType;
    }

    public void showHealthBar(bool b)
    {
        if (b == true)
            updateHealthBar(currentHealth);
        else
            healthBar.enabled = false;
    }

    public void CanBeAttacked(bool attack)
    {
        particuleDetection.SetActive(attack);
    }
		
    IEnumerator deathCoroutine(float time)
    {
        noCoroutine = false;

        anim.enabled = false;
        box2D.enabled = false;

        //sr.enabled = false;
        for (int i = 0; i < sr.Length; i++)
        {
            if (sr[i].gameObject.name != "DeadSprite")
                sr[i].enabled = false;
        }

        particuleDetection.SetActive(false);
        healthBar.sprite = null;

        deadEnemy.GetComponent<SpriteRenderer>().enabled = true;
        deadEnemy.GetComponent<BoxCollider2D>().enabled = true;
        transform.tag = "Untagged";

        yield return new WaitForSeconds(time);

        deadEnemy.GetComponent<SpriteRenderer>().enabled = false;
        deadEnemy.GetComponent<BoxCollider2D>().enabled = false;

        box2D.enabled = true;

        //sr.enabled = true;
        for (int i = 0; i < sr.Length; i++)
        {
            if (sr[i].gameObject.name != "DeadSprite")
                sr[i].enabled = true;
        }

        anim.enabled = true;
        transform.tag = "Enemy";

        currentHealth = totalHealth;
        healthBar.sprite = FullHealth;
        healthBar.enabled = false;

        noCoroutine = true;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && transform.tag == "Enemy")
        {
            GameObject[] listeCP = GameObject.FindGameObjectsWithTag("Checkpoint");
            GetComponent<Rigidbody2D>().isKinematic = true;

            if (enemyType == 3)
            {
                GetComponent<EnemyPathing>().setDirection(GetComponent<EnemyPathing>().getLastDirection());
            }

            foreach (GameObject g in listeCP)
            {
                if (g.GetComponent<TriggerCheckpoint>().getIsActivated() == true)
                {
                    coll.gameObject.GetComponent<PlayerController>().isRespawning = true;
                    coll.gameObject.GetComponent<PlayerController>().getRigidbody2D().velocity = new Vector2(0, 0);
                    coll.transform.position = new Vector3(g.transform.position.x, g.transform.position.y - 4.0f, coll.transform.position.z);

                    GameObject s = GameObject.Find("Sha");

                    if (s.GetComponent<FollowPlayer>().playerFound == false)
                    {
                        s.transform.position = s.GetComponent<FollowPlayer>().getPositionDepart();
                    }
                    if (s.GetComponent<FollowPlayer>().playerFound == true && s.GetComponent<FollowPlayer>().nocoroutine == true)
                        s.transform.position = new Vector3(g.transform.position.x, g.transform.position.y - 4.0f, s.transform.position.z);

                    break;
                }
            }
        }

        else if (coll.gameObject.tag == "Player" && transform.tag == "Untagged")
        {
            coll.gameObject.GetComponent<PlayerController>().Bounce();
        }
    }
}
