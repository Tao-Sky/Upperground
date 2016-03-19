using UnityEngine;
using System.Collections;

public class triggerMachine : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }
}
