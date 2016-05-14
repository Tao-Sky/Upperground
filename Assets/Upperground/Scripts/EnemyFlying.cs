using UnityEngine;
using System.Collections;

public class EnemyFlying : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Transform leftTrigger;
    private Transform rightTrigger;
    private float walkSpeed;

    private int nbCurvePoints = 8;
    private GameObject[] tabCurvePoints;
    private GameObject nextCurvePoint;
    private int indiceNextCurvePoint;

    private int nbWayPoints = 5;
    private GameObject[] tabWayPoints;
    private GameObject nextWayPoint;
    private int indiceNextWayPoint;

    Vector3 direction;
    Vector3 lastDirection;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        leftTrigger = transform.root.Find("TriggerEnemyPath").FindChild("TriggerL1");
        rightTrigger = transform.root.Find("TriggerEnemyPath").FindChild("TriggerR1");

        walkSpeed = GetComponent<EnemyPathing>().getWalkSpeed();
        tabWayPoints = new GameObject[nbWayPoints];
        calculateWayPoints();
        determineBezierCurve();
        determineNextCurvePoint();
    }

    public void setDirection(Vector3 v)
    {
        direction = v;
    }

    public Vector3 getLastDirection()
    {
        return lastDirection;
    }

    void Update()
    {
        GameObject manager = GameObject.Find("GameManager");
        manager.hideFlags = HideFlags.HideInHierarchy;

        if (manager.GetComponent<GameManager>().IsPaused == true)
        {
            direction = lastDirection;
        }

        else
        {
            if (isOnNextCurvePoint(indiceNextCurvePoint))
            {
                if (indiceNextCurvePoint == tabCurvePoints.Length - 1)
                {
                    indiceNextCurvePoint--;
                    nextCurvePoint = tabCurvePoints[indiceNextCurvePoint];
                }

                else if (indiceNextCurvePoint == 0)
                {
                    indiceNextCurvePoint++;
                    nextCurvePoint = tabCurvePoints[indiceNextCurvePoint];
                }

                else
                {
                    if (GetComponent<EnemyPathing>().getWalkingDirection() > 0)
                    {
                        indiceNextCurvePoint++;
                        nextCurvePoint = tabCurvePoints[indiceNextCurvePoint];
                    }

                    else
                    {
                        indiceNextCurvePoint--;
                        nextCurvePoint = tabCurvePoints[indiceNextCurvePoint];
                    }
                }

                direction.x = (nextCurvePoint.transform.position.x - transform.position.x) * walkSpeed * Time.deltaTime;
                direction.y = (nextCurvePoint.transform.position.y - transform.position.y) * walkSpeed * Time.deltaTime;

                lastDirection = direction;

                //Debug.Log("Prochain point curve: " + indiceNextCurvePoint);
            }

            if (Time.timeScale == 1f)
                transform.Translate(lastDirection);
            else
                transform.Translate(direction);
        }
    }

    void calculateWayPoints()
    {
        float dist = rightTrigger.position.x - leftTrigger.position.x;

        GameObject gFirst = new GameObject();
        gFirst.hideFlags = HideFlags.HideInHierarchy;

        gFirst.transform.Translate(leftTrigger.position.x, transform.position.y, transform.position.z);
        tabWayPoints[0] = gFirst;

        for (int i = 1; i < tabWayPoints.Length - 1; i++)
        {
            GameObject g = new GameObject();
            g.hideFlags = HideFlags.HideInHierarchy;

            float x = leftTrigger.position.x + i * (float)(dist / (nbWayPoints - 1));
            float y;
            if (i % 2 == 0)
                y = transform.position.y + 4.0f;
            else
                y = transform.position.y - 2.0f;

            float z = transform.position.z;

            g.transform.Translate(x, y, z);
            tabWayPoints[i] = g;
        }

        GameObject gLast = new GameObject();
        gLast.hideFlags = HideFlags.HideInHierarchy;

        gLast.transform.Translate(rightTrigger.position.x, transform.position.y, transform.position.z);
        tabWayPoints[nbWayPoints - 1] = gLast;

        for (int i = 0; i < nbWayPoints; i++)
        {
            Debug.Log("Valeur y du point " + i + ": " + tabWayPoints[i].transform.position.y);
        }

    }
    /*
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

        direction.x = (nextWayPoint.transform.position.x - transform.position.x) * walkSpeed * Time.deltaTime;
        direction.y = (nextWayPoint.transform.position.y - transform.position.y) * walkSpeed * Time.deltaTime;

    }

    bool isOnNextWayPoint(int indiceNextWayPoint)
    {
        float ecart = 0.05f;

        if(Mathf.Abs(transform.position.x - tabWayPoints[indiceNextWayPoint].transform.position.x) < ecart
                && Mathf.Abs(transform.position.y - tabWayPoints[indiceNextWayPoint].transform.position.y) < ecart)
        {
            return true;
        }

        else
            return false;
    }
    */

    void determineNextCurvePoint()
    {
        nextCurvePoint = tabCurvePoints[0];
        indiceNextCurvePoint = 0;

        for (int i = 0; i < nbCurvePoints; i++)
        {
            float dist = tabCurvePoints[i].transform.position.x - transform.position.x;
            if (dist < Mathf.Abs(nextCurvePoint.transform.position.x - transform.position.x) && dist > 0)
            {
                nextCurvePoint = tabCurvePoints[i];
                indiceNextCurvePoint = i;
            }
        }

        direction.x = (nextCurvePoint.transform.position.x - transform.position.x) * walkSpeed * Time.deltaTime;
        direction.y = (nextCurvePoint.transform.position.y - transform.position.y) * walkSpeed * Time.deltaTime;

        lastDirection = direction;
    }

    bool isOnNextCurvePoint(int indiceNextCurvePoint)
    {
        float ecart = 0.03f;

        if (Mathf.Abs(transform.position.x - tabCurvePoints[indiceNextCurvePoint].transform.position.x) < ecart)
        // && Mathf.Abs(transform.position.y - tabCurvePoints[indiceNextCurvePoint].transform.position.y) < ecart)
        {
            return true;
        }

        else
            return false;
    }

    void determineBezierCurve()
    {
        tabCurvePoints = new GameObject[nbCurvePoints + 1];
        int degre = nbWayPoints - 1;

        for (int i = 0; i < nbCurvePoints + 1; i++)
        {
            float u = (float)i / nbCurvePoints;
            float resX = 0f;
            float resY = 0f;

            for (int j = 0; j < nbWayPoints; j++)
            {
                resX += Facto(degre) / (Facto(j) * Facto(degre - j)) * Mathf.Pow(u, j) * Mathf.Pow(1 - u, degre - j) * tabWayPoints[j].transform.position.x;
                resY += Facto(degre) / (Facto(j) * Facto(degre - j)) * Mathf.Pow(u, j) * Mathf.Pow(1 - u, degre - j) * tabWayPoints[j].transform.position.y;
            }

            GameObject g = new GameObject();
            //g.hideFlags = HideFlags.HideInHierarchy;
            g.transform.Translate(new Vector3(resX, resY, transform.position.z));
            tabCurvePoints[i] = g;
        }
    }

    float Facto(float d)
    {
        if (d <= 0)
            return 1;
        else
            return d * Facto(d - 1);
    }
}
