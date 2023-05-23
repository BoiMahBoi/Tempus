using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bruges til at trigger animationen. 

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private void OnEnable()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
