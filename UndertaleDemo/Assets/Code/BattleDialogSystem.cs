using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleDialogSystem : MonoBehaviour
{
    public GameObject battlePanel;
    private bool isBattleOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isBattleOpen)
                CloseBattle();
            else
                OpenBattle();
        }

    }

    void OpenBattle()
    {
        battlePanel.SetActive(true);
        isBattleOpen = true;
    }

    void CloseBattle()
    {
        battlePanel.SetActive(false);
        isBattleOpen = false;
    }

    public void OnAttack()
    {
        Debug.Log("Atak");
        // kod atak
    }

    public void OnDefend()
    {
        Debug.Log("Obrona");
        // kod obrona
    }

    public void OnItem()
    {
        Debug.Log("Item");
        // kod item
    }

    public void OnMercy()
    {
        Debug.Log("Mercy");
        // kod do mercy
    }
}

