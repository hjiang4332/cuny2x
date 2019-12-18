using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewPlayerController1 : PhysicsObject
{
    //jumping and animations
    private float jumpTakeOffSpeed = 10;
    private float maxSpeed = 6;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    //private bool canDoubleJump;

    //audio
    private bool soilWalkIsPlaying = false;
    AudioSource soilWalk;
    private GameObject jump;
    AudioSource jumpSound;
    private GameObject tweet;
    AudioSource tweetSound;

    //tweets
    private GameObject protest1;
    private GameObject protest2;
    private GameObject pr1;
    private GameObject pr2;

    void Awake()
    { 
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //audio
        soilWalk = GetComponent<AudioSource>();
        jump = GameObject.Find("Jump");
        jumpSound = jump.GetComponent<AudioSource>();
        tweet = GameObject.Find("TweetSound");
        tweetSound = tweet.GetComponent<AudioSource>();

        //tweets instantiation
        protest1 = GameObject.Find("protest1");
        protest2 = GameObject.Find("protest2");
        pr1 = GameObject.Find("pr1");
        pr2 = GameObject.Find("pr2");
        protest1.gameObject.SetActive(false);
        protest2.gameObject.SetActive(false);
        pr1.gameObject.SetActive(false);
        pr2.gameObject.SetActive(false);
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal1");

        //use items
        if (Input.GetKeyDown("s"))  //items
        {
            //use item
        }

        //player jump
        if (Input.GetKeyDown("w") && grounded)
        {
            jumpSound.Play();
            velocity.y = jumpTakeOffSpeed;
            //canDoubleJump = true;

            if (Input.GetKeyDown("w") && !grounded)
            {
                Debug.Log("doublejump");
                jumpSound.Play();
                velocity.y = jumpTakeOffSpeed;
                //canDoubleJump == false;
            }
        }
        else if (Input.GetKeyUp("w")) //cancel jump
        {
            if (velocity.y > 0) //going up
            {
                velocity.y = velocity.y * .5f;
            }
        }

        //play walking sound
        if (move.x != 0 && grounded)
        {
            if (soilWalkIsPlaying == false)
            {
                soilWalkIsPlaying = true;
                soilWalk.Play();
                StartCoroutine(AudioWait());

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Win"))
        {
            SceneManager.LoadScene("ProtestWin");
        }
        else if (collision.CompareTag("tweetTrigger1"))
        {
            protest1.gameObject.SetActive(true);
            Destroy(pr1);
            tweetSound.Play(0);
        }
        else if (collision.CompareTag("tweetTrigger2"))
        {
            protest2.gameObject.SetActive(true);
            Destroy(pr2);
            tweetSound.Play(0);
        }
    }

    IEnumerator AudioWait()
    {
        yield return new WaitForSeconds(2);
        soilWalkIsPlaying = false;
    }
}
