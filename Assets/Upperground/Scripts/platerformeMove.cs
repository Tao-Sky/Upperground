using UnityEngine;
using System.Collections;

public class platerformeMove : MonoBehaviour {

    public Transform origine;
    public Transform destination;
    public float timeoppen=2.0f;
    private bool avance=true;

  
    public void go()
    {
        Debug.Log("je recoit");
        StartCoroutine(Move());
    }

    IEnumerator Move() {
        while (Vector3.Distance(destination.position, transform.position) > 0.1f)
            {
                Debug.Log("j'avance");
                transform.position = Vector3.Lerp(transform.position, destination.position, 1.0f * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(timeoppen);
        while (Vector3.Distance(origine.position, transform.position) > 0.1f)
        {
            Debug.Log("je recule");
            transform.position = Vector3.Lerp(transform.position, origine.position, 1.0f * Time.deltaTime);
            yield return null;
        }

    }
    

}
