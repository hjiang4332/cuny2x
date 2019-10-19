using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public float jumpTakeOffSpeed = 6;
    public float maxSpeed = 6;

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
        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded) //jump 
        {
            velocity.y = jumpTakeOffSpeed;

        }
        else if (Input.GetButtonUp("Jump")) //cancel jump
        {
            if(velocity.y > 0) //going up
            {
                velocity.y = velocity.y * .5f;  //reduce velocity by half
            }
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
