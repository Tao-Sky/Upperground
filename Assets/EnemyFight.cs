using UnityEngine;
using System.Collections;

public class EnemyFight : MonoBehaviour
{

    public SpriteRenderer healthBar;
    public GameObject particuleDetection;
    public Sprite fullHealth;
    public Sprite OneHitHealth;
    public Sprite TwoHitHealth;

    private int healthPoints = 3;


    void Awake()
    {
        // HealthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
        healthBar.enabled = false;
        healthBar.sprite = fullHealth;
    }


    void Update()
    {

    }

    public void takingDamage()
    {
        if (healthPoints > 0)
            healthPoints--;

        if (healthPoints == 2)
            healthBar.sprite = OneHitHealth;
        else if (healthPoints == 1)
            healthBar.sprite = TwoHitHealth;
    }

    public int getHealthPoints()
    {
        return healthPoints;
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
        //Faire clignoter le sprite
        Destroy(this.gameObject);
    }
}
