using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;

public class InkDialogSystem : MonoBehaviour
{
    public TextAsset inkJSON;
    public GameObject dialogPanel;
    public TMP_Text dialogText;

    private Story story;
    private bool isOpen = false;

    void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.F))
        {
            StartDialog();
        }
    }

    public void StartDialog()
    {
        if (isOpen)
        {
            ContinueStory();
        }
        else
        {
            story = new Story(inkJSON.text);

            dialogPanel.SetActive(true);
            isOpen = true;

            ContinueStory();
        }
    }

    void ContinueStory()
    {
        if (story.canContinue)
        {
            string line = story.Continue();
            Debug.Log(line);
            dialogText.text = line;
        }
        else
        {
            CloseDialog();
        }

        Debug.Log("Story linie count: " + story.currentChoices.Count);
        Debug.Log("Story canContinue: " + story.canContinue);
    }

    public void CloseDialog()
    {
        dialogPanel.SetActive(false);
        isOpen = false;
    }
}




