using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody2D rb;
    private float dashSpeed = 20f;
    private float dashTime;
    private float startDashTime = .1f;
    private int direction;
    private bool canDash;
    //private float cooldown = 2;
    //private float timeStamp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        //timeStamp = Time.time + cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == 0)  //player no dash
        {
            if (Input.GetKeyDown("t"))    //up
            {
                direction = 1;
            }
            else if (Input.GetKeyDown("f"))  //up left
            {
                direction = 2;
            }
            else if (Input.GetKeyDown("h")) //up right
            {
                direction = 3;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
               // if(timeStamp <= Time.time)
                //{
                    dashTime -= Time.deltaTime;
                    if (direction == 1)
                    {
                        rb.velocity = Vector2.up * (2 * dashSpeed); //up
                    }
                    else if (direction == 2)
                    {
                        rb.velocity = new Vector2(-1, 1) * dashSpeed;  //up left
                    }
                    else if (direction == 3)
                    {
                        rb.velocity = Vector2.one * dashSpeed;    //up right
                    }
                    //timeStamp = Time.time + cooldown;
                //}
            }
        }
    }
}
