using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewPlayerController3 : PhysicsObject
{
    //movement
    public float characterNumber;
    public float p1JumpTakeOffSpeed = 6;
    public float maxSpeed = 6;
    public float p2JumpTakeOffSpeed = 6;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool p1CanDoubleJump;
    private bool p2CanDoubleJump;

    //audio
    private bool soilWalkIsPlaying = false;
    AudioSource soilWalk;
    private GameObject jump;
    AudioSource jumpSound;
    private GameObject tweet;
    AudioSource tweetSound;
    private GameObject speed;
    AudioSource speedSound;

    //items
    private int randomNum;
    private int p1ItemNum;
    private int p2ItemNum;

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
        speed = GameObject.Find("SpeedUpSound");
        speedSound = speed.GetComponent<AudioSource>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal" + characterNumber);

        if (characterNumber == 1)
        {
            //use items
            if (Input.GetKeyDown("s"))  
            {
                UseItem(1, 2, p1ItemNum);
            }

            //player jump
            if (Input.GetKeyDown("w") && grounded)
            {
                jumpSound.Play();
                velocity.y = p1JumpTakeOffSpeed;
                p1CanDoubleJump = true;
            }
            else if (Input.GetKeyUp("w")) //cancel jump
            {
                if (velocity.y > 0) //going up
                {
                    velocity.y = velocity.y * .5f;
                }
            }
            else if (Input.GetKeyDown("w") && p1CanDoubleJump == true)
            {
                jumpSound.Play();
                velocity.y = p1JumpTakeOffSpeed;
                p1CanDoubleJump = false;
            }

            //player walking sound
            if (move.x != 0 && grounded)
            {
                if (soilWalkIsPlaying == false)
                {
                    soilWalkIsPlaying = true;
                    soilWalk.Play();
                    StartCoroutine(AudioWait());

                }
            }
        }
        if (characterNumber == 2)
        {
            //use items
            if (Input.GetKeyDown(KeyCode.DownArrow))  //items
            {
                UseItem(2, 1, p2ItemNum);
            }

            //player jump
            if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
            {
                jumpSound.Play();
                velocity.y = p2JumpTakeOffSpeed;
                p2CanDoubleJump = true;
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow)) //cancel jump
            {
                if (velocity.y > 0) //going up
                {
                    velocity.y = velocity.y * .5f;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && p2CanDoubleJump == true)
            {
                jumpSound.Play();
                velocity.y = p2JumpTakeOffSpeed;
                p2CanDoubleJump = false;
            }

            //player walking sound
            if (move.x != 0 && grounded)
            {
                if (soilWalkIsPlaying == false)
                {
                    soilWalkIsPlaying = true;
                    soilWalk.Play();
                    StartCoroutine(AudioWait());

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

        if (Input.GetKeyDown("t"))
        {
            SceneManager.LoadScene("TitleScreen");
        }
        if (Input.GetKeyDown("y"))
        {
            SceneManager.LoadScene("MainMap");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Win"))
        {
            if (name == "Player1") SceneManager.LoadScene("ProtestWin");
            else if (name == "Player2") SceneManager.LoadScene("PrWin");
        }
        else if (collision.CompareTag("tweetTrigger1"))
        {
            if(name == "Player1")
            {
                protest1.gameObject.GetComponent<Renderer>().enabled = true;
                Destroy(pr1);
                tweetSound.Play(0);
            }
            else if(name == "Player2")
            {
                pr1.gameObject.GetComponent<Renderer>().enabled = true;
                Destroy(protest1);
                tweetSound.Play(0);
            }
        }
        else if (collision.CompareTag("tweetTrigger2"))
        {
            if(name == "Player1")
            {
                protest2.gameObject.GetComponent<Renderer>().enabled = true;
                Destroy(pr2);
                tweetSound.Play(0);
            }
            else if (name == "Player2")
            {
                pr2.gameObject.GetComponent<Renderer>().enabled = true;
                Destroy(protest2);
                tweetSound.Play(0);
            }
        }
        else if (collision.CompareTag("item"))
        {
            //0 = self speedup
            //1 = enemy slowdown 
            //2 = enemy gravity + 
            //randomNum = Random.Range(0, numItems+1);
            randomNum = 0;
            if(name == "Player1")
            {
                p1ItemNum = randomNum;
            }
            else if(name == "Player2")
            {
                p2ItemNum = randomNum;
            }
        }
    }

    IEnumerator AudioWait()
    {
        yield return new WaitForSeconds(2);
        soilWalkIsPlaying = false;
    }

    void UseItem(int userNumber, int otherPlayerNumber, int itemNumber)
    {
        if(itemNumber == 0) //self speedup
        {
            speedSound.Play(0);
            this.maxSpeed = 10;
            Invoke("returnSpeed", 5);
        }
        else if(itemNumber == 1)    //enemy speed down
        {

        }
        else if(itemNumber == 2)    //enemy jump down
        {
            //string playerNumber = this.GetType().GetField("p" + otherPlayerNumber + "JumpTakeOffSpeed") = 1;
            //StartCoroutine(ReturnJumpSpeed(playerNumber));
            if(otherPlayerNumber == 1)
            {
                p1JumpTakeOffSpeed = 1;
                //p1JumpTakeOffSpeed = 6;
            }
            if(otherPlayerNumber == 2)
            {
                this.p1JumpTakeOffSpeed = 1;
                Debug.Log(p2JumpTakeOffSpeed);
                //p2JumpTakeOffSpeed = 6;
            }
        }
    }

    /*IEnumerator ReturnJumpSpeed(playerNumber)
    {
        yield return new WaitForSeconds(3);
        this.GetType().GetField("p" + otherPlayerNumber + "JumpTakeOffSpeed") = 6;
    }*/

    void returnSpeed()
    {
        this.maxSpeed = 6;
    }
}
