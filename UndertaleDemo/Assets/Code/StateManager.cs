using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;

    private GameState CurrentGameState;

    private void Start()
    {
        instance = this;
    }

    public void SetCurrentState(GameState gameState)
    {
        CurrentGameState = gameState;
    }
}

public enum GameState
{
    Gameplay,
    MainMenu,
    BattleMenu,
    GameplayDialogue,
    BattleDialogue,
    CutScene
}
