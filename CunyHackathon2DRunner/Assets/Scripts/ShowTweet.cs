using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTweet : MonoBehaviour
{
    private GameObject pr1;
    private GameObject pr2;
    private GameObject protest1;
    private GameObject protest2;
    AudioSource tweetSound;

    // Start is called before the first frame update
    void Start()
    {
        pr1 = GameObject.Find("pr1");
        //pr2 = GameObject.Find("pr2");
        protest1 = GameObject.Find("protest1");
        //protest2 = GameObject.Find("protest2");

        pr1.gameObject.GetComponent<Renderer>().enabled = false;
        //pr2.gameObject.GetComponent<Renderer>().enabled = false;
        protest1.gameObject.GetComponent<Renderer>().enabled = false;
        //protest2.gameObject.GetComponent<Renderer>().enabled = false;


        tweetSound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        tweetSound.Play(0);
        if(collision.collider.CompareTag("Player1"))
        {
            Debug.Log("Collided with player1");
            pr1.gameObject.GetComponent<Renderer>().enabled = true;
        }
    }
}
