using UnityEngine;
using System.Collections;

public class TriggerEnemyWalking : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && other.GetComponent<EnemyPathing>().facingRight)
        {
            // Debug.Log("Test Trigger droit");
            other.GetComponent<EnemyPathing>().walkingDirection *= -1;
            other.GetComponent<EnemyPathing>().Flip();
        }

        else if (other.gameObject.tag == "Enemy" && !other.GetComponent<EnemyPathing>().facingRight)
        {
            //Debug.Log("Test Trigger gauche");
            other.GetComponent<EnemyPathing>().walkingDirection *= -1;
            other.GetComponent<EnemyPathing>().Flip();
        }
    }
}
