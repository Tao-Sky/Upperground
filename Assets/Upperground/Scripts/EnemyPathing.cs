using UnityEngine;
using System.Collections;

public class EnemyPathing : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;
    private Transform leftTrigger;
    private Transform rightTrigger;

    private Animator anim;
    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;

    public float walkSpeed = 2.0f;
    public float walkingDirection = 1.0f;
    public GameObject listeWayPoints;
    Vector3 walkAmount;

    private int nbCurvePoints = 8;
    private GameObject[] tabCurvePoints;
    private GameObject nextCurvePoint;
    private int indiceNextCurvePoint;

    private int nbWayPoints;// = 5;
    private Transform[] tabWayPoints;
    private Transform nextWayPoint;
    private int indiceNextWayPoint;

    Vector3 direction;
    Vector3 lastDirection;

    void Awake()
    {
        anim = GetComponent<Animator>();
        bc2d = GetComponent<BoxCollider2D>();

        if (GetComponent<EnemyFight>().getEnemyType() == 3)
        {
            rb2d = GetComponent<Rigidbody2D>();
            rb2d.gravityScale = 0;
            /*
            leftTrigger = transform.root.Find("TriggerEnemyPath").FindChild("TriggerL1");
            rightTrigger = transform.root.Find("TriggerEnemyPath").FindChild("TriggerR1");
            */

            walkSpeed = GetComponent<EnemyPathing>().getWalkSpeed();
            //calculateWayPoints();
            
            nbWayPoints = listeWayPoints.transform.childCount;
            tabWayPoints = new Transform[nbWayPoints];

            for (int i = 0; i < nbWayPoints; i++)
            {
                tabWayPoints[i] = listeWayPoints.transform.GetChild(i);
            }

            determineBezierCurve();
			Invoke("determineNextCurvePoint",0.5f);
        }
    }

    void Update()
    {
        GameObject manager = GameObject.Find("GameManager");
        manager.hideFlags = HideFlags.HideInHierarchy;

        if (GetComponent<EnemyFight>().getNoCoroutine() == true)
        {
            if (manager.GetComponent<GameManager>().IsPaused == true ||Time.deltaTime == 0)
            {
                direction = lastDirection;
            }

			else
			{
				if (GetComponent<EnemyFight>().getEnemyType() != 3)
				{
					walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
					transform.Translate(walkAmount);
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

					}
					lastDirection = direction;

					if (Time.timeScale == 0.0f)
						transform.Translate(lastDirection);
					else
						transform.Translate(direction);
				}				
			}

        }
    }

    public float getWalkingDirection()
    {
        return walkingDirection;
    }

    public float getWalkSpeed()
    {
        return walkSpeed;
    }

    public void setWalkSpeed(float f)
    {
        walkSpeed = f;
    }

    public void setDirection(Vector3 v)
    {
        direction = v;
    }

    public Vector3 getLastDirection()
    {
        return lastDirection;
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

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
        float ecart = 0.05f * walkSpeed;

        if (Mathf.Abs(transform.position.x - tabCurvePoints[indiceNextCurvePoint].transform.position.x) < ecart
         && Mathf.Abs(transform.position.y - tabCurvePoints[indiceNextCurvePoint].transform.position.y) < ecart)
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
                resX += Facto(degre) / (Facto(j) * Facto(degre - j)) * Mathf.Pow(u, j) * Mathf.Pow(1 - u, degre - j) * tabWayPoints[j].position.x;
                resY += Facto(degre) / (Facto(j) * Facto(degre - j)) * Mathf.Pow(u, j) * Mathf.Pow(1 - u, degre - j) * tabWayPoints[j].position.y;
            }

            GameObject g = new GameObject();
            g.hideFlags = HideFlags.HideInHierarchy;
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
