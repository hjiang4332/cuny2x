using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    private float moveInput;
    private Rigidbody2D rb;
    private bool faceR = true;
    private bool isGrounded;

    public float speed = 10;
    public float jumpForce = 10;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float checkRadius = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded == true && Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = Vector2.up * jumpForce;
        }

    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        //Debug.Log(isGrounded); //shows false?
        //is circle positioned around groundcheck, if radius is touching what is ground. 
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (faceR == true && moveInput < 0)
        {
            Flip();
        }
        else if (faceR == false && moveInput > 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        faceR = !faceR;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
