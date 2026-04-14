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

    public void StartDialogue(string npcID, string action)
    {
        story = new Story(inkJSON.text);

        string path = npcID + "_" + action;

        Debug.Log("PATH: " + path);

        story.ChoosePathString(path);

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


