using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorial : MonoBehaviour
{
    public Dialogue dialogue;

    public void Awake()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
