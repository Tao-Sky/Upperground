using UnityEngine;
using System.Collections;

public class dropacide : MonoBehaviour {

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
        while (Vector3.Distance(destination.transform.position, transform.position) > 2.0f)
        {
            transform.position = Vector3.Lerp(transform.position, destination.transform.position, 1.0f * Time.deltaTime);
            yield return null;
        }
        destination.GetComponent<Animator>().SetBool("destroy", true);
        gameObject.SetActive(false);
        
        yield return null;

    }
}
