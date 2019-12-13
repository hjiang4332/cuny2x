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
    //private bool p1CanDash;

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
            //if (grounded)
            //{
            //    p1CanDash = true;
            //}

            //player jump
            if (Input.GetKeyDown("g") && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
            }

            if (dashDirection == 0)  //player not dashing
            {
                if (Input.GetKeyDown("w"))    //dashDirection up
                {
                    dashDirection = 1;
                }
                else if (Input.GetKeyDown("a"))  //dashDirection up left
                {
                    dashDirection = 2;
                }
                else if (Input.GetKeyDown("d")) //dashDirection up right
                {
                    dashDirection = 3;
                }
            }
            else if (dashDirection != 0)
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
                    if (dashDirection == 1)
                    {
                        rb.velocity = Vector2.up * (2 * dashSpeed); //up
                    }
                    else if (dashDirection == 2)
                    {
                        rb.velocity = new Vector2(-1, 1) * dashSpeed;  //up left
                    }
                    else if (dashDirection == 3)
                    {
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
    }//end compute velocity
}
