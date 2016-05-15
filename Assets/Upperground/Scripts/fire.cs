using UnityEngine;
using System.Collections;

public class fire : MonoBehaviour {

    public GameObject destination;

    void Start()
    {
        gameObject.GetComponent<ParticleSystem>().Pause();
    }

    public void go()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (Vector3.Distance(destination.transform.position, transform.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, destination.transform.position, 2.0f * Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
        destination.GetComponent<EnemyFight>().takingDamage();
        yield return null;
        
    }
}
