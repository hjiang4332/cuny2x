using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : PhysicsObject
{
    public float characterNumber;

    //jumping and animations
    private float jumpTakeOffSpeed = 5;
    private float maxSpeed = 5;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    //dashing
    private Rigidbody2D rb;
    private float dashSpeed = 10f;
    private float dashTime;
    private float startDashTime = .1f;
    private int dashDirection;
    private bool p1CanDash;
    private bool p2CanDash;

    void Awake()
    { 
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //dash
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal" + characterNumber);

        if(characterNumber == 1)
        {
            //use items
            if (Input.GetKeyDown("space"))  //items
            {
                //use item
            }

            //player jump
            if (Input.GetKeyDown("g") && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
            }
            else if (Input.GetKeyUp("g")) //cancel jump
            {
                if (velocity.y > 0) //going up
                {
                    velocity.y = velocity.y * .5f;
                }
            }

            /*if (Input.GetKeyDown("g") && !grounded) 
            {
                if (Input.GetKey(KeyCode.W)) velocity.y = jumpTakeOffSpeed;  //up
                else if (Input.GetKey(KeyCode.A)) velocity.x = -(jumpTakeOffSpeed);  //left
                else if (Input.GetKey(KeyCode.S)) velocity.y = -(jumpTakeOffSpeed);  //down
                else if (Input.GetKey(KeyCode.D)) velocity.x = jumpTakeOffSpeed;  //right     
            }*/

            if (dashDirection == 0)  //player not dashing
            {
                if (Input.GetKeyDown("g"))    //dashDirection up
                {
                    dashDirection = 1;
                }
                else if (Input.GetKeyDown("f"))  //dashDirection up left
                {
                    dashDirection = 2;
                }
                else if (Input.GetKeyDown("h")) //dashDirection up right
                {
                    dashDirection = 3;
                }
            }
            else
            {
                if (dashTime <= 0)
                {
                    dashDirection = 0;
                    dashTime = startDashTime;
                    rb.velocity = Vector2.zero;
                }
                else
                {
                    dashTime -= Time.deltaTime;
                    if (dashDirection == 1 && p1CanDash)
                    {
                        p1CanDash = false;
                        rb.velocity = Vector2.up * (2 * dashSpeed); //up
                    }
                    else if (dashDirection == 2 && p1CanDash)
                    {
                        p1CanDash = false;
                        rb.velocity = new Vector2(-1, 1) * dashSpeed;  //up left
                    }
                    else if (dashDirection == 3 && p1CanDash)
                    {
                        p1CanDash = false;
                        rb.velocity = Vector2.one * dashSpeed;    //up right
                    }
                }
            }

        }


        if(characterNumber == 2)
        {
            if (Input.GetKeyDown("0"))
            {
                //use item
            }

            if (grounded)
            {
                p2CanDash = true;
            }

            if ((Input.GetKeyDown("[2]") || Input.GetKeyDown("k")) && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
                p2CanDash = true;
            }
            else if (Input.GetKeyUp("[2]") || Input.GetKeyUp("k"))
            {
                if (velocity.y > 0) //going up
                {
                    velocity.y = velocity.y * .5f;  //reduce velocity by half
                }
            }


            if (dashDirection == 0)  //player no dash
            {
                if ((Input.GetKeyDown("[5]") || Input.GetKeyDown("i")) && p2CanDash)    //up
                {
                    dashDirection = 1;
                }
                else if ((Input.GetKeyDown("[1]") || Input.GetKeyDown("j")) && p2CanDash)  //up left
                {
                    dashDirection = 2;
                }
                else if ((Input.GetKeyDown("[3]") || Input.GetKeyDown("l")) && p2CanDash) //up right
                {
                    dashDirection = 3;
                }
            }
            else
            {
                if (dashTime <= 0)
                {
                    dashDirection = 0;
                    dashTime = startDashTime;
                    rb.velocity = Vector2.zero;
                }
                else
                {
                    dashTime -= Time.deltaTime;
                    if (dashDirection == 1 && p2CanDash)
                    {
                        p2CanDash = false;
                        rb.velocity = Vector2.up * (2 * dashSpeed); //up
                    }
                    else if (dashDirection == 2 && p2CanDash)
                    {
                        p2CanDash = false;
                        rb.velocity = new Vector2(-1, 1) * dashSpeed;  //up left
                    }
                    else if (dashDirection == 3 && p2CanDash)
                    {
                        p2CanDash = false;
                        rb.velocity = Vector2.one * dashSpeed;    //up right
                    }
                }
            }
        }

        //sprite flipping
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f)); //flip sprite
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x / maxSpeed));

        targetVelocity = move * maxSpeed;
    }
}
