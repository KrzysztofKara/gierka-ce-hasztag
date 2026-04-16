using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using System;

public class InkControll : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private TextMeshProUGUI dialogueText;

    public static event Action OnDialogueEnd; 
    public static event Action<NPC> OnMercy; 

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

        ShowNextLine(action);
    }

    public void ShowNextLine(string action = "")
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
            if (action == "mercy")
            {
                OnMercy?.Invoke(null);
            }
            else
            {
                OnDialogueEnd?.Invoke();
            }
        }
    }

    public bool IsDialogueActive()
    {
        return dialogueActive;
    }
}


