using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueZone : MonoBehaviour
{
    public InkDialogSystem dialogSystem;   
    private bool playerInside = false; 
    void Start()
    {

    }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.F))
        {
            dialogSystem.StartDialog();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("Wszed³eœ w strefê dialogu");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            dialogSystem.CloseDialog();
            Debug.Log("Gracz wyszed³ ze strefy dialogu");
        }
    }
}
