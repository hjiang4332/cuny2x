using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BadEndScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        SceneManager.LoadScene("BadEnd");
        //Invoke("LoadBadEnding", 1);
    }

    private void LoadBadEnding()
    {
        SceneManager.LoadScene("BadEnd");
    }
}
