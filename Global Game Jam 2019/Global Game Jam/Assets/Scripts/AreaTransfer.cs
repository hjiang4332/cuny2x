using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransfer : SceneChangeManager
{

    void OnCollisionEnter2D(Collision2D collision)
    {
      Debug.Log("Collision");
      if(collision.gameObject.tag == "player"){
        ChangeScene();
      }
    }


}
