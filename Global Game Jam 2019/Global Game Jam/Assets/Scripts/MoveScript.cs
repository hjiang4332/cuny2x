using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    private const double V = 0.1;
    public float speed = 15f;
    Direction currentDir;
    public Sprite uSprite, dSprite, lSprite, rSprite;
    bool isMoving;
    public bool canMove;
    public float moveVert;
    public float moveHoriz;
    Vector3 startPos;
    Vector3 endPos;
    float t;
    Animator anim;

    void Start()
    {
        canMove = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveVert = Input.GetAxis("Vertical");
        moveHoriz = Input.GetAxis("Horizontal");
       
        if (!isMoving && canMove)
        {   
            if (Mathf.Abs(moveHoriz)>Mathf.Abs(moveVert)) {
                moveVert = 0;
            }
            else {
                moveHoriz = 0;
            }

            if (moveVert != 0 || moveHoriz!=0)
            {
                if (moveHoriz<0) {
                    currentDir = Direction.LEFT;
                    anim.SetInteger("Dir", 4);
                }
                else if (moveHoriz>0) {
                    currentDir = Direction.RIGHT;
                    anim.SetInteger("Dir", 2);
                }
                else if (moveVert<0) {
                    currentDir = Direction.DOWN;
                    anim.SetInteger("Dir", 3);
                }
                else if (moveVert>0) {
                    currentDir = Direction.UP;
                    anim.SetInteger("Dir",1);
                }

            }

            switch (currentDir) {
                case Direction.RIGHT:
                    gameObject.GetComponent<SpriteRenderer>().sprite = rSprite;
                    break;
                case Direction.UP:
                    gameObject.GetComponent<SpriteRenderer>().sprite = uSprite;
                    break;
                case Direction.DOWN:
                    gameObject.GetComponent<SpriteRenderer>().sprite = dSprite;
                    break;
                case Direction.LEFT:
                    gameObject.GetComponent<SpriteRenderer>().sprite = lSprite;
                    break;

            }


            //StartCoroutine(Move(transform)); no longer used
        }

        //add velocity to player
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveHoriz*speed, moveVert*speed);


    }

    //function for moving (no longer used)
   /* public IEnumerator Move(Transform entity)
    {
        isMoving = true;
        startPos = entity.position;
        t = 0;
        endPos = new Vector3(startPos.x + System.Math.Sign(moveHoriz), startPos.y + System.Math.Sign(moveVert), startPos.z);

        while (t < 1f)
        {
            t += (float)0.1 * speed;
            entity.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        isMoving = false;
        yield return 0;
    }*/


enum Direction
    {
        UP,
        LEFT,
        RIGHT,
        DOWN
    }



}
