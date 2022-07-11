using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Queue<string> sentences;
    [SerializeField] Text nameText;
    [SerializeField] Text dialogueText;
    [SerializeField] CanvasGroup dialogueCanvasGroup;
    bool isEndDialogue = true;
    float timeTexting;
    bool isTexting = false;
    bool startDialogue = false;
    public bool IsEndDialogue
    {
        get { return isEndDialogue; }
        set { isEndDialogue = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        dialogueCanvasGroup.alpha = 0;
        dialogueCanvasGroup.interactable = false;
    }
    private void Update()
    {
        if (!isTexting&&startDialogue)
        {
            timeTexting = Time.time;
            isTexting = true;
            DisplayNextSentence();
        }
        if (Time.time >= timeTexting + 2) isTexting = false;
    }
    public void StartDialogue(Dialogue dialogue)
    {
        startDialogue = true;
        dialogueCanvasGroup.alpha = 1;
        dialogueCanvasGroup.interactable = true;
        nameText.text = dialogue.CharacterName + " :";
        sentences.Clear();
        foreach (string sentence in dialogue.Sentences)
        {
            sentences.Enqueue(sentence);
        }
        //DisplayNextSentence();
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count ==0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    void EndDialogue()
    {
        startDialogue = false;
        dialogueCanvasGroup.alpha = 0;
        dialogueCanvasGroup.interactable = false;
        isEndDialogue = true;
    }
}
