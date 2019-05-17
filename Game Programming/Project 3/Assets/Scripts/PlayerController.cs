using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : PhysicsObject
{
    //player movement
    public float jumpTakeOffSpeed = 10;
    public float maxMovementSpeed = 10;

    //player animation
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    //death and respawn and audio
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    public float delay = .46f;
    AudioSource deathSound;
    private bool faceR = true;

    public GameObject BulletToRight;
    public GameObject BulletToLeft;
    Vector2 bulletPos;
    public float fireRate = 0.3f;
    public float nextFire = 0;

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
        //if ((Input.GetButtonDown("Jump") && isGrounded) || Input.GetKeyDown("up") && isGrounded)
        if ((Input.GetKeyDown("w") && isGrounded) || Input.GetKeyDown("up") && isGrounded)
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
        /*bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }*/

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.1f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            faceR = !faceR;
        }

        //parameter is named move, so setfloat move. 
        animator.SetFloat("move", Mathf.Abs(velocity.x) / maxMovementSpeed);
        targetVelocity = move * maxMovementSpeed;
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
        else if (Input.GetKeyDown("space") && Time.time > nextFire)
        {
            animator.SetBool("isShooting", true);
            nextFire = Time.time + fireRate;
            Fire();
        }
        else if (Input.GetKeyUp("space"))
        {
            animator.SetBool("isShooting", false);
        }
        else if (Input.GetKeyDown("q")){
            transform.position = new Vector2(-12, 0);
        }
        else if (Input.GetKeyDown("e")){
            SceneManager.LoadScene("MainScene");
        }
        else if (Input.GetKeyDown("r")){
            SceneManager.LoadScene("Stage2");

        }
        else if (Input.GetKeyDown("t")){
            SceneManager.LoadScene("End");

        }
        else if (Input.GetKeyDown("y")){
            SceneManager.LoadScene("BadEnd");

        }
    }

    /*public void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Fire();
        }
    }*/

    private void Fire()
    {
        bulletPos = transform.position;
        if (faceR)
        {
            bulletPos += new Vector2(+2f, -.5f);
            Instantiate(BulletToRight, bulletPos, Quaternion.identity);
        }
        else{
            bulletPos += new Vector2(-1.5f, 0f);
            Instantiate(BulletToLeft, bulletPos, Quaternion.identity);
        }
    }

   /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Danger"))
        {
            animator.SetBool("isDead", true);
            deathSound.Play(0);
            Invoke("Respawn", delay);
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Bear"){
            animator.SetBool("isDead", true);
            deathSound.Play(0);
            Invoke("Respawn", delay);
        }
    }

    private void Respawn()
    {
        player.transform.position = respawnPoint.transform.position;
        animator.SetBool("isDead", false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Door") && Input.GetKeyDown("up"))
        {
            SceneManager.LoadScene("Stage2");
        }
        if (collision.CompareTag("End") && Input.GetKeyDown("up"))
        {
            SceneManager.LoadScene("End");
        }
    }
}