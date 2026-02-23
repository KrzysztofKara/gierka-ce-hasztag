using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public GameObject dialogPanel; // panel
    public TMP_Text dialogText;    // wyswietlany tekst
    private bool isOpen = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                CloseDialog();
            }
            else
            {
                OpenDialog("przykladowy dialog"); 
            }
        }
    }

    public void OpenDialog(string text)
    {
        dialogPanel.SetActive(true);
        dialogText.text = text;
        isOpen = true;
    }

    public void CloseDialog()
    {
        dialogPanel.SetActive(false);
        isOpen = false;
    }
}
