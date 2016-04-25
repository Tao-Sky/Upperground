using UnityEngine;
using System.Collections;

public class EnemyFight : MonoBehaviour
{
    public SpriteRenderer healthBar;
    public GameObject particuleDetection;

    public float totalHealth = 3;
    private float currentHealth;

    void Awake()
    {
        healthBar.enabled = false;
        currentHealth = totalHealth;
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

        healthBar.transform.localScale = new Vector3(currentHealth / totalHealth, 1, 1);
    }

    public float getHealthPoints()
    {
        return currentHealth;
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
