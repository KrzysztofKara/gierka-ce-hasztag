using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;

public class InkControll : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story story;

    private bool dialogueActive = false;

    public void StartDialogue()
    {
        Debug.Log("INK START");
        dialogueText.gameObject.SetActive(true);

        if (inkJSON == null)
        {
            Debug.LogError("Brak inkJSON!");
            return;
        }

        story = new Story(inkJSON.text);

        dialogueActive = true;

        dialogueText.gameObject.SetActive(true);

        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (!dialogueActive) return;

        if (story.canContinue)
        {
            string text = story.Continue();
            Debug.Log("INK: " + text);
            dialogueText.text = text;
        }
        else
        {
            Debug.Log("KONIEC DIALOGU");
            dialogueActive = false;
            dialogueText.gameObject.SetActive(false);
        }
    }

    public bool IsDialogueActive()
    {
        return dialogueActive;
    }
}


