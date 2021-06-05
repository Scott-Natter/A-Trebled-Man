using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private int stringTicker = 0;
    public Image Composer1;
    public Image Composer2;


    private Queue<string> sentences;

    public void StartDialogue (Dialogue dialogue)
    {
        Composer1.enabled = !Composer1.enabled;

        animator.SetBool("isOpen", true);
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

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        Composer1.enabled = !Composer1.enabled;
        Composer2.enabled = !Composer1.enabled;
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
    }
}
