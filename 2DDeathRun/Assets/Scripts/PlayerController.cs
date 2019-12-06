using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : PhysicsObject
{
    //player movement
    public float jumpTakeOffSpeed = 8;
    public float maxMovementSpeed = 8;

    //player animation
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    //death and respawn and audio
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Transform respawnPoint2;
    private int respawnCounter = 0;
    public float delay = .46f;
    AudioSource deathSound;

    private float thanosDelay = 2.3531f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        delay = .46f;
        deathSound = GetComponent<AudioSource>();
    }

    protected override void ComputeVelocity()
    {
        //base.ComputeVelocity();
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            if(velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        //sprite flipping
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        //parameter is named move, so setfloat move. 
        animator.SetFloat("move", Mathf.Abs(velocity.x) / maxMovementSpeed);
        //jump
        animator.SetBool("grounded", isGrounded);
        //crouching
        if (Input.GetKeyDown("down"))
        {
            animator.SetBool("isCrouching", true);
        }
        else if (Input.GetKeyUp("down"))
        {
            animator.SetBool("isCrouching", false);
        }

        targetVelocity = move * maxMovementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Danger") && respawnCounter == 0)
        {
            animator.SetBool("isDead", true);
            deathSound.Play(0);
            Invoke("Respawn", delay);
        }
        else if (collision.CompareTag("Danger") && respawnCounter == 1)
        {
            animator.SetBool("isDead", true);
            deathSound.Play(0);
            Invoke("Respawn2", delay);
        }
        else if (collision.CompareTag("ChangeRespawn"))
        {
            respawnCounter = 1;
        }
        else if (collision.CompareTag("omaewa"))
        {
            Invoke("SpecialDeath", 3);
        }
        else if (collision.CompareTag("Thanos"))
        {
            Invoke("SpecialDeath", thanosDelay);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "BigCrate" || collision.gameObject.name == "LongCrate" || collision.gameObject.name == "BigCrate (1)"){
            animator.SetBool("isDead", true);
            deathSound.Play(0);
            Invoke("Respawn", delay);
        }
        else if (collision.gameObject.name == "Savior"){
            animator.SetBool("isDead", true);
            deathSound.Play(0);
            Invoke("Respawn2", delay);
        }
    }

    private void Respawn()
    {
        player.transform.position = respawnPoint.transform.position;
        animator.SetBool("isDead", false);
    }

    private void Respawn2()
    {
        player.transform.position = respawnPoint2.transform.position;
        animator.SetBool("isDead", false);
    }

    private void SpecialDeath()
    {
        animator.SetBool("isDead", true);
        deathSound.Play(0);
        Invoke("Respawn2", delay);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("House") && Input.GetKeyDown("up"))
        {
            SceneManager.LoadScene("End");
        }
    }
}