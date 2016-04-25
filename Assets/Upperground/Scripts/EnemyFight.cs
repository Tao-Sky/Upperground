using UnityEngine;
using System.Collections;

public class EnemyFight : MonoBehaviour
{
    public SpriteRenderer healthBar;
    public GameObject particuleDetection;

    private SpriteRenderer sr;
    private BoxCollider2D box2D;
    private Animator anim;
    
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
        sr = GetComponent<SpriteRenderer>();
        box2D = GetComponent<BoxCollider2D>();

        if (enemyType == 1)
            deadEnemy = null;
        else if (enemyType == 2)
        {
            deadEnemy.GetComponent<SpriteRenderer>().enabled = false;
            deadEnemy.GetComponent<BoxCollider2D>().enabled = false;
        }  
    }

    void Update()
    {
        if (currentHealth < totalHealth)
            healthBar.enabled = true;
    }

    public void takingDamage()
    {
        if (currentHealth > 0)
            currentHealth--;

        healthBar.transform.localScale = new Vector3(currentHealth / totalHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    public float getHealthPoints()
    {
        Debug.Log(currentHealth);
        return currentHealth;
    }

    public int getEnemyType()
    {
        return enemyType;
    }

    public void showHealthBar(bool b)
    {
        if (b == true)
            healthBar.enabled = true;
        else
            healthBar.enabled = false;
    }

    public void CanBeAttacked(bool attack)
    {
        particuleDetection.SetActive(attack);
    }

    public void EnemyDie()
    {
        if (enemyType == 1)
            Destroy(this.gameObject);

        else if (enemyType == 2)
        {
            StartCoroutine(deathCoroutine(5.0f));
        }
    }

    IEnumerator deathCoroutine(float time)
    {
        noCoroutine = false;

        anim.enabled = false;
        box2D.enabled = false;
        sr.enabled = false;
        particuleDetection.SetActive(false);
        
        deadEnemy.GetComponent<SpriteRenderer>().enabled = true;
        deadEnemy.GetComponent<BoxCollider2D>().enabled = true;
        transform.tag = "Untagged";
    
        yield return new WaitForSeconds(time);

        deadEnemy.GetComponent<SpriteRenderer>().enabled = false;
        deadEnemy.GetComponent<BoxCollider2D>().enabled = false;
    
        box2D.enabled = true;
        sr.enabled = true;
        anim.enabled = true;
        transform.tag = "Enemy";

        currentHealth = totalHealth;
        healthBar.transform.localScale = new Vector3(currentHealth/totalHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        healthBar.enabled = false;

        noCoroutine = true;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && transform.tag == "Enemy")
        {
            GameObject[] listeCP = GameObject.FindGameObjectsWithTag("Checkpoint");

            foreach (GameObject g in listeCP)
            {
                if (g.GetComponent<TriggerCheckpoint>().getIsActivated() == true)
                {
                    coll.gameObject.GetComponent<PlayerController>().isRespawning = true;
                    coll.gameObject.GetComponent<PlayerController>().getRigidbody2D().velocity = new Vector2(0, 0);
                    coll.transform.position = new Vector3(g.transform.position.x, g.transform.position.y - 4.0f, coll.transform.position.z);
                    GameObject s = GameObject.Find("Sha");
                    if (s.GetComponent<FollowPlayer>().playerFound == true && s.GetComponent<FollowPlayer>().nocoroutine == true)
                        s.transform.position = new Vector3(g.transform.position.x, g.transform.position.y - 4.0f, s.transform.position.z);

                    break;
                }
            }
        }
    }
}
