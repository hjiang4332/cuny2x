using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public float characterNumber;

    private float jumpTakeOffSpeed = 5;
    private float maxSpeed = 5;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Awake()
    { 
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal" + characterNumber);
        if(characterNumber == 1)
        {
            if (Input.GetKeyDown("space"))
            {
                //use item
            }


            if (Input.GetKeyDown("g") && grounded) //jump 
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
        }



        if(characterNumber == 2)
        {
            if (Input.GetKeyDown("0"))
            {
                //use item
            }
            if (Input.GetKeyDown("[2]") && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
            }
            else if (Input.GetKeyUp("[2]"))
            {
                if (velocity.y > 0) //going up
                {
                    velocity.y = velocity.y * .5f;  //reduce velocity by half
                }
            }
            //if (Input.GetKeyDown("[3]"))
            //{
            //    if(Input.GetKey(KeyCode.UpArrow))
            //    {
            //        velocity.y = jumpTakeOffSpeed * 1.10f;
            //    }
            //    else if(Input.GetKey(KeyCode.RightArrow))
            //    {
            //        velocity.x = maxSpeed * 1.10f;
            //        velocity.y = 1f;
            //    }
            //}
        }

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
