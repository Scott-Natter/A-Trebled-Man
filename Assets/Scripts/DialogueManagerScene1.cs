using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManagerScene1 : MonoBehaviour
{

    public GameObject DoorOpener;
    public Canvas redNote;
    public Canvas greenNote;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private int stringTicker = 0;


    private Queue<string> sentences;

    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        redNote.enabled = !redNote.enabled;
        greenNote.enabled = !greenNote.enabled;

        Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;

        sentences = new Queue<string>();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        print(sentences.Count);
        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        stringTicker += 1;
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }


        if (stringTicker == 3)
        {
            greenNote.enabled = !greenNote.enabled;
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        if (stringTicker == 4)
        {
            redNote.enabled = !redNote.enabled;
            greenNote.enabled = !greenNote.enabled;
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        if (stringTicker == 5)
        {
            redNote.enabled = !redNote.enabled;
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        else
        {
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }


    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        Debug.Log("End of conversation");
        DoorOpener.GetComponent<OpenDoors>().OpenElevator();
    }
}
