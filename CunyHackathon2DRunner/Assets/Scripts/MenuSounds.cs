using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    public AudioClip buttonHover;
    AudioSource audioSource;

    void Start()
    {
        //buttonHover = GameObject.Find("MainMenu").GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseEnter()
    {
        Debug.Log("Enter");
        audioSource.PlayOneShot(buttonHover);
    }
}
