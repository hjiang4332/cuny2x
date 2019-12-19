using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{

    private float gravityModifier = 1f;
    private float minGroundNormalY = .65f;

    protected Vector2 velocity;
    protected Rigidbody2D rb2d;
    protected const float minMoveDistance = 0.001f;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected const float shellRadius = 0.01f;
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Vector2 targetVelocity; // store input from outside of class

    //tweets
    protected GameObject protest1;
    protected GameObject protest2;
    protected GameObject pr1;
    protected GameObject pr2;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        contactFilter.useTriggers = false; //dont use collision for triggers
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;

        //tweets instantiation
        protest1 = GameObject.Find("protest1");
        protest2 = GameObject.Find("protest2");
        pr1 = GameObject.Find("pr1");
        pr2 = GameObject.Find("pr2");

        protest1.gameObject.GetComponent<Renderer>().enabled = false;
        protest2.gameObject.GetComponent<Renderer>().enabled = false;
        pr1.gameObject.GetComponent<Renderer>().enabled = false;
        pr2.gameObject.GetComponent<Renderer>().enabled = false;
    }

    private void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    void FixedUpdate() //for physics (gravity)
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x); //perpendicular line to climb slopes

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);  //x axis

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);   //y axis
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius); //add shell so we dont get stuck in collider.
            hitBufferList.Clear(); //first clear
            for (int i = 0; i < count; i++) //copy only current contacts
            {
                hitBufferList.Add(hitBuffer[i]); //take from hitbuffer and add to list
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY) //set grounded state, check angle to see if ground. (hitting wall sideways will make it seem like ground, this makes sure players have to be standing at a certain angle 
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal); //avoid sloped ceiling, dont kill velocity. calculation with this.
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance; //if true modified, if not distance
            }


        }
        rb2d.position = rb2d.position + move.normalized * distance;
    }

}