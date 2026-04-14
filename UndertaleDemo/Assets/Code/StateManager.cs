using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static GameState CurrentGameState;

    private void Start()
    {
        CurrentGameState = GameState.BattleMenu;
    }
}

