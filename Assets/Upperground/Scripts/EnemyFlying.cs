using UnityEngine;
using System.Collections;

public class EnemyFlying : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Transform leftTrigger;
    private Transform rightTrigger;
    private int nbWayPoints = 5;

    private GameObject[] tabWayPoints;
    private GameObject nextWayPoint;
    private int indiceNextWayPoint;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        leftTrigger = transform.root.Find("TriggerEnemyPath").FindChild("TriggerL1");
        rightTrigger = transform.root.Find("TriggerEnemyPath").FindChild("TriggerR1");

        tabWayPoints = new GameObject[nbWayPoints];
        calculateWayPoints();
        determineNextWayPoint();
    }

    void Update()
    {/*
        transform.Translate(new Vector3(nextWayPoint.transform.position.x - transform.position.x, nextWayPoint.transform.position.y - transform.position.y,0));

        if (indiceNextWayPoint == nbWayPoints - 1)
        {
            indiceNextWayPoint--;
            nextWayPoint = tabWayPoints[indiceNextWayPoint];
        }

        else if(indiceNextWayPoint == 0)
        {
            indiceNextWayPoint++;
            nextWayPoint = tabWayPoints[indiceNextWayPoint];
        }

        else
        {
            if (GetComponent<EnemyPathing>().getWalkingDirection() > 0)
            {
                indiceNextWayPoint++;
                nextWayPoint = tabWayPoints[indiceNextWayPoint];
            }
            
            else
            {
                indiceNextWayPoint--;
                nextWayPoint = tabWayPoints[indiceNextWayPoint];
            }
        }*/
    }

    void calculateWayPoints()
    {
        float dist = rightTrigger.position.x - leftTrigger.position.x;
        GameObject gFirst = new GameObject();
        gFirst.transform.Translate(leftTrigger.position.x, transform.position.y, transform.position.z);
        tabWayPoints[0] = gFirst;

        for(int i=1; i<tabWayPoints.Length-1; i++)
        {
            GameObject g = new GameObject();
            float x = leftTrigger.position.x + i * (float)(dist / (nbWayPoints-1));
            float y;
            if (i % 2 == 0)
                y = transform.position.y + 0.5f;
            else
                y = transform.position.y - 0.5f;

            float z = transform.position.z;

            g.transform.Translate(x, y, z);
            tabWayPoints[i] = g;
        }

        GameObject gLast = new GameObject();
        gLast.transform.Translate(rightTrigger.position.x, transform.position.y, transform.position.z);
        tabWayPoints[nbWayPoints - 1] = gLast;
        
        for(int i=0; i<nbWayPoints; i++)
        {
            Debug.Log("Valeur x du point " + i + ": " + tabWayPoints[i].transform.position.y);
        }
        
    }

    void determineNextWayPoint()
    {
        nextWayPoint = tabWayPoints[0];
        indiceNextWayPoint = 0;

        for (int i = 0; i < nbWayPoints; i++)
        {
            float dist = tabWayPoints[i].transform.position.x - transform.position.x;
            if (dist < Mathf.Abs(nextWayPoint.transform.position.x - transform.position.x) && dist > 0)
            {
                nextWayPoint = tabWayPoints[i];
                indiceNextWayPoint = i;
            }
        }
    }


}
