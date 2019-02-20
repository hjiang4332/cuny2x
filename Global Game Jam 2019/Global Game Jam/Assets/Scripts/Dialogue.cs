using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue: MonoBehaviour
{
    public string name;

    [TextArea(1, 3)]
    public string[] sentences;
    private int index;


    public void nextSentence()
    {
        if(index < sentences.Length-1)
        {
            index++;

        }
    }

}
