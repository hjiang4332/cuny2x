using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAndChase : MonoBehaviour
{
    public float speed;
    public GameObject[] patrolPoints;
    int whichPoint;
    float distToPatrolPoint;
    float distToPlayer;
    private bool faceR = false;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        whichPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distToPlayer <= 2f)  //if close to player walk to him
        {

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);

        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[whichPoint].transform.position, Time.deltaTime * speed);
            distToPatrolPoint = Vector2.Distance(transform.position, patrolPoints[whichPoint].transform.position);

            if (distToPatrolPoint < 0.02f)
            {
                whichPoint++;
                if (whichPoint >= patrolPoints.Length)
                {
                    whichPoint = 0;
                }
                Flip();
            }
        }
    }

    void Flip()
    {
        faceR = !faceR;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
