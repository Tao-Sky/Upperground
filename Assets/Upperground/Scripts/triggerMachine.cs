using UnityEngine;
using System.Collections;

public class TriggerMachine : MonoBehaviour
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
