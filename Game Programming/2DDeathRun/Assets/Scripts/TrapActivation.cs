using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapActivation : PlayerController
{
    //First log hazard
    int trapCounter = 0;
    private GameObject log1;
    private GameObject log2;
    private GameObject longCrate;
    private GameObject bigCrate2;
    private GameObject jesus;
    private GameObject omae;
    private GameObject thanos;

    //P2 able to activate traps
    private bool canActivateTrap = true;

    AudioSource jesusSound;
    AudioSource omaewaSound;
    AudioSource thanosSound;

    void Start()
    {
        log1 = GameObject.Find("log1");
        log2 = GameObject.Find("log2");
        longCrate = GameObject.Find("LongCrate");
        bigCrate2 = GameObject.Find("BigCrate (1)");
        jesus = GameObject.Find("Savior");
        omae = GameObject.Find("omaewa");
        thanos = GameObject.Find("Thanos");
        log1.gameObject.SetActive(false);
        log2.gameObject.SetActive(false);
        longCrate.gameObject.SetActive(false);
        bigCrate2.gameObject.SetActive(false);
        jesus.gameObject.SetActive(false);
        omae.gameObject.SetActive(false);
        thanos.gameObject.SetActive(false);

        jesusSound = jesus.GetComponent<AudioSource>();
        omaewaSound = omae.GetComponent<AudioSource>();
        thanosSound = thanos.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q") && canActivateTrap)
        {
            if (trapCounter == 0)   //log
            {
                log1.gameObject.SetActive(true);
                log2.gameObject.SetActive(true);
            }
            else if(trapCounter == 1)   //drop long box
            {
                longCrate.gameObject.SetActive(true);
                longCrate.gameObject.transform.position = new Vector2(-1,3);
            }
            else if(trapCounter == 2)   //left big box
            {
                bigCrate2.gameObject.SetActive(true);
                bigCrate2.gameObject.transform.position = new Vector2(10, 0);

            }
            else if(trapCounter == 3)   //left long box
            {
                longCrate.gameObject.SetActive(true);
                longCrate.gameObject.transform.position = new Vector2(23, 0);
            }
            else if(trapCounter == 4)   //jesus
            {
                jesus.gameObject.SetActive(true);
                jesusSound.Play(0);
            }
            else if(trapCounter == 5)   //omaewa
            {
                omae.gameObject.SetActive(true);
                omaewaSound.Play(0);
            }
            else if(trapCounter == 6)   //thanos
            {
                thanos.gameObject.SetActive(true);
                thanosSound.Play(0);
            }

            canActivateTrap = false;
            Invoke("despawnLogs", 4);
        }
    }

    void despawnLogs()
    {
        log1.gameObject.SetActive(false);
        log2.gameObject.SetActive(false);
        bigCrate2.gameObject.SetActive(false);
        longCrate.gameObject.SetActive(false);
        jesus.gameObject.SetActive(false);
        omae.gameObject.SetActive(false);
        thanos.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Trap0"))
       {
            trapCounter = 0;
            canActivateTrap = true;
       }
        if (collision.CompareTag("Trap1"))
        {
            trapCounter = 1;
            canActivateTrap = true;
        }
        if (collision.CompareTag("Trap2"))
        {
            trapCounter = 2;
            canActivateTrap = true;
        }
        if (collision.CompareTag("Trap3"))
        {
            trapCounter = 3;
            canActivateTrap = true;
        }
        if (collision.CompareTag("Trap4"))
        {
            trapCounter = 4;
            canActivateTrap = true;
        }
        if (collision.CompareTag("Trap5"))
        {
            trapCounter = 5;
            canActivateTrap = true;
        }
        if (collision.CompareTag("Trap6"))
        {
            trapCounter = 6;
            canActivateTrap = true;
        }
        if (collision.CompareTag("House") && Input.GetKeyDown("up"))
        {
            SceneManager.LoadScene("End");
        }

    }
}
