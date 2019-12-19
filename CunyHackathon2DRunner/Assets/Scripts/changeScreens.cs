﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScreens : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            SceneManager.LoadScene("TitleScreen");
        }
        if (Input.GetKeyDown("y"))
        {
            SceneManager.LoadScene("MainMap");
        }
    }
}
