using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : PhysicsObject
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
    private bool p1canDash;
    private bool p2canDash;

    //better jumping
    //public float fallMultiplier = 2.5f;

    //avoid slamming onto walls
    private RaycastHit hit;

    //audio
    AudioSource soilWalk;
    private GameObject jump;
    AudioSource jumpSound;

    //tweets
    //private GameObject protest1;
    //private GameObject protest2;
    //private GameObject protest3;
    //private GameObject protest4;
    //private GameObject pr1;
    //private GameObject pr2;

    void Awake()
    { 
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //dash
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;

        //tweets instantiation
        //protest1 = GameObject.Find("protest1");
        //  protest2 = GameObject.Find("protest2");
        // protest3 = GameObject.Find("protest3");
        // protest4 = GameObject.Find("protest4");
        //pr1 = GameObject.Find("pr1");
        //pr2 = GameObject.Find("pr2");

        //audio
        soilWalk = GetComponent<AudioSource>();
        jump = GameObject.Find("Jump");
        jumpSound = jump.GetComponent<AudioSource>();
    }

    void Start()
    {
        //protest1.gameObject.SetActive(false);
        //protest2.gameObject.SetActive(false);
        //protest3.gameObject.SetActive(false);
        //protest4.gameObject.SetActive(false);
        //pr1.gameObject.SetActive(false);
        //pr2.gameObject.SetActive(false);
    }



    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal" + characterNumber);

        //Vector3 forward = GameObject.Find("Player1").transform.TransformDirection(Vector3.up) * 10;
        //Debug.DrawRay(GameObject.Find("Player1").transform.position, forward, Color.green);

        if (characterNumber == 1)
        {
            //use items
            if (Input.GetKeyDown("space"))  //items
            {
                //use item
            }

            //reset dash ability 
            if (grounded)
            {
                p1canDash = true;
                if(move.x != 0)
                {
                    soilWalk.Play(0);
                    //yield return new WaitForSeconds(12.0f);
                }
            }

            //player jump
            if (Input.GetKeyDown("g") && grounded)
            {
                jumpSound.Play();
                velocity.y = jumpTakeOffSpeed;
                //velocity.y += jumpTakeOffSpeed * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (Input.GetKeyUp("g")) //cancel jump
            {
                if (velocity.y > 0) //going up
                {
                    velocity.y = velocity.y * .5f;
                }
            }

            //dash mechanics
            if (dashDirection == 0)  //player not dashing
            {
                if (Input.GetKeyDown("t"))    //dashDirection up
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
                    if (dashDirection == 1 && p1canDash)
                    {
                        p1canDash = false;
                        rb.velocity = Vector2.up * (2 * dashSpeed); //up
                                                                    //Ray upRay = new Ray(transform.position, Vector3.up);
                                                                    /*if(Physics.Raycast(upRay, 2*dashSpeed)<upRay)
                                                                    {

                                                                    }
                                                                    else
                                                                    {
                                                                        rb.velocity = Vector2.up * (2 * dashSpeed); //up
                                                                    }*/
                    }
                    else if (dashDirection == 2 && p1canDash)
                    {
                        p1canDash = false;
                        rb.velocity = new Vector2(-1, 1) * dashSpeed;  //up left
                    }
                    else if (dashDirection == 3 && p1canDash)
                    {
                        p1canDash = false;
                        rb.velocity = Vector2.one * dashSpeed;    //up right
                    }
                }
            }
        }
        if (characterNumber == 2)
        {
            //use items
            if (Input.GetKeyDown("0"))  //items
            {
                //use item
            }

            //reset dash ability 
            if (grounded)
            {
                p2canDash = true;
                soilWalk.Play(0);
            }

            //player jump
            if (Input.GetKeyDown("k") && grounded)
            {
                jumpSound.Play();
                velocity.y = jumpTakeOffSpeed;
                //velocity.y += jumpTakeOffSpeed * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (Input.GetKeyUp("k")) //cancel jump
            {
                if (velocity.y > 0) //going up
                {
                    velocity.y = velocity.y * .5f;
                }
            }

            //dash mechanics
            if (dashDirection == 0)  //player not dashing
            {
                if (Input.GetKeyDown("i"))    //dashDirection up
                {
                    dashDirection = 1;
                }
                else if (Input.GetKeyDown("j"))  //dashDirection up left
                {
                    dashDirection = 2;
                }
                else if (Input.GetKeyDown("l")) //dashDirection up right
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
                    if (dashDirection == 1 && p2canDash)
                    {
                        p2canDash = false;
                        rb.velocity = Vector2.up * (2 * dashSpeed); //up
                        //Ray upRay = new Ray(transform.position, Vector3.up);
                        /*if(Physics.Raycast(upRay, 2*dashSpeed)<upRay)
                        {

                        }
                        else
                        {
                            rb.velocity = Vector2.up * (2 * dashSpeed); //up
                        }*/
                    }
                    else if (dashDirection == 2 && p2canDash)
                    {
                        p2canDash = false;
                        rb.velocity = new Vector2(-1, 1) * dashSpeed;  //up left
                    }
                    else if (dashDirection == 3 && p2canDash)
                    {
                        p2canDash = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Win"))
        {
            SceneManager.LoadScene("ProtestWin");
        }
    }

}
