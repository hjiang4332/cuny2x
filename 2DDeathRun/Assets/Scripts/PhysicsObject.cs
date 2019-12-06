using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float gravityModifier = 1f;
    public float minGroundNormalY = .65f;

    //Dont let other classes access this.
    protected Vector2 velocity;
    protected Rigidbody2D rb2d;
    protected const float minMoveDistance = 0.001f;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected const float padding = 0.01f;
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected bool isGrounded;
    protected Vector2 groundNormal;

    //store incoming input from outside class
    protected Vector2 targetVelocity;

    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //dont detect collisions on triggers
        contactFilter.useTriggers = false;
        //use settings from physics 2d settings to determine what layers you gonna check collision against.
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    //for physics
    private void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;
        isGrounded = false;
        Vector2 deltaPosition = velocity * Time.deltaTime;

        //calculate vector along ground (angle) go in angle, not in one direction
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        //movement x
        Vector2 move = moveAlongGround * deltaPosition.x;
        Movement(move, false);

        //movement y
        move = Vector2.up * deltaPosition.y;
        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;
        if(distance > minMoveDistance)
        {
            //check if collider will overlap with anything in next grame. 
            //move, layer, array, 
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + padding);
            //copy indices of object contacts
            hitBufferList.Clear();
            for(int i = 0; i<count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }
            //check normal of each raycast2d object to determine angle of collision
            for(int i = 0; i< hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                //determine if player is grounded 
                if(currentNormal.y > minGroundNormalY)
                {
                    //check if ground (player contacting a vertical wall will be considered on gorund if didnt check)
                    isGrounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    //cancel velocity that would be stopped from collision
                    velocity = velocity - projection * currentNormal;
                }
                //prevent getting stuck in colliders.
                float modifiedDistance = hitBufferList[i].distance - padding;
                distance = modifiedDistance < distance ? modifiedDistance : distance;

            }
        }
        rb2d.position = rb2d.position + move.normalized * distance;
    }
}
