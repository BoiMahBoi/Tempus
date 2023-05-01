using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bruges til at trigger animationen. 

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDiaLogue(dialogue);
    }
}
