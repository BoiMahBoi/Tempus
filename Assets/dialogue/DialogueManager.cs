using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //laver reforance til text objekter
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    //laver en Queue til at opbevare alt dialoguen
    public Queue<string> sentences;

    public Animator animator;

    public string nextSceneOnDialogueEnd;

    //laver Queue
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);           //starter animationen
        
        nameText.text = dialogue.name;              

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)         //bruger et foreach loop til at kører gemmen sentence
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)               //kigger på om dialoguen er slut
        {
            EndDialogue();
            return;
        }
        
        string sentence = sentences.Dequeue();
        StopAllCoroutines();                        //bruger en StopAllCoroutines til vhis spilleren shipper. så der ikke er flere dialogue der kører samtidet
        StartCoroutine(TypeSentence(sentence));     //bruger coroutines for at lave lidt text animation. 
    }

    IEnumerator TypeSentence (string sentence)      
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())      
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);          // bruges til at DialogueClose animationen.

        if(nextSceneOnDialogueEnd.Length > 0)
        {
            SceneManager.LoadScene(nextSceneOnDialogueEnd);
        }
    }
}
