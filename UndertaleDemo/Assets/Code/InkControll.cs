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

    void Start()
    {
        story = new Story(inkJSON.text);
    }

    public void StartActDialogue()
    {
        Debug.Log("ACT START");

        if (inkJSON == null)
        {
            Debug.Log("NO INK JSON");
            return;
        }

        story = new Story(inkJSON.text);

        dialogueText.gameObject.SetActive(true);

        string text = story.Continue();
        Debug.Log("INK OUTPUT: " + text);

        dialogueText.text = text;
    }

    private void RefreshDialogue()
    {
        if (story.canContinue)
        {
            dialogueText.text = story.Continue();
        }
    }
}


