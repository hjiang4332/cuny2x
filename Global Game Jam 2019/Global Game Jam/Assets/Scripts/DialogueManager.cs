using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    //Used for Manipulation of Display
    public Text nameText;
    public Text DialogueText;
    public GameObject DialogueBox;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
      sentences = new Queue<string>();
    }

    /**
     @post Initializes conversation and Queues all dialogue.
     @param dialogue the dialogue obtained from the object.
     */
    public void StartDialogue (Dialogue dialogue){
      DialogueBox.SetActive(true);
      Debug.Log("Starting conversation with " + dialogue.name);
      nameText.text = dialogue.name;
      sentences.Clear();

      foreach (string sentence in dialogue.sentences){
        sentences.Enqueue(sentence);
      }
      DisplayNextSentence();
    }

    /**
     @post Displays the next line of dialogue
     */
    public void DisplayNextSentence(){
      if(sentences.Count == 0){
        EndDialogue();
        return;
      }
      string sentence = sentences.Dequeue();
      DialogueText.text = sentence;
      Debug.Log(sentence);
    }

    /**
     @post closes dialogue Box
    */
    public void EndDialogue(){
      DialogueBox.SetActive(false);
      Debug.Log("End of Conversation");
    }
}
