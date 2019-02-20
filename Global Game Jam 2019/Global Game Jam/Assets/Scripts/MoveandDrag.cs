using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveandDrag : MonoBehaviour
{
  private const double V = 0.1;
  public float speed;
  Direction currentDir;
  public Sprite uSprite, dSprite, lSprite, rSprite;
  bool isMoving;
  public bool canMove;
  public float moveVert;
  public float moveHoriz;
  Vector3 startPos;
  Vector3 endPos;
  float t;
  bool isGrabbing;

  void Start()
  {
      canMove = true;

  }

  // Update is called once per frame
  void OnCollisionEnter2D(Collision2D collision)
  {

        if(isGrabbing){
          Debug.Log("Yes we unhold");
          isGrabbing = false;
        }
        else{
          Debug.Log("We hold now");
          isGrabbing = true;
        }


  }

  void FixedUpdate(){
    if(Input.GetKeyDown(KeyCode.E)){
      Debug.Log("Yes we unhold");
      isGrabbing = false;
    }
    if(isGrabbing){
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

            }
            else if (moveHoriz>0) {
                currentDir = Direction.RIGHT;

            }
            else if (moveVert<0) {
                currentDir = Direction.DOWN;

            }
            else if (moveVert>0) {
                currentDir = Direction.UP;

            }

        }




        //StartCoroutine(Move(transform)); no longer used
    }

    //add velocity to player
    GetComponent<Rigidbody2D>().velocity = new Vector2(moveHoriz*speed, moveVert*speed);

  }
  }


enum Direction
  {
      UP,
      LEFT,
      RIGHT,
      DOWN
  }



}
