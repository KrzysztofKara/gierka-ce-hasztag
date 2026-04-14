using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    public BattleUIManager battleUIManager;
    [SerializeField] private Player player;
    [SerializeField] private Sprite DeathSprite;
    private NPC currentNPC;
    

    private void OnEnable()
    {
        NPC.OnPlayerMeetNPC += StartBattle;
        NPC.OnNPCDeath += EndBattle;
        AttackSelector.OnSliderSelected += DealDamage;
    }

    private void OnDisable()
    {
        NPC.OnPlayerMeetNPC -= StartBattle;
        NPC.OnNPCDeath -= EndBattle;
        AttackSelector.OnSliderSelected -= DealDamage;
    }





    private void StartBattle(NPC npc)
    {
        currentNPC = npc;

        battleUIManager.StartBattle();
        battleUIManager.SetNPCSprite(npc.battleSprite);
        StateManager.CurrentGameState = GameState.BattleMenu;

        Debug.Log("Rozpoczêto walkê z: " + npc.npcName);
    }


    private void EndBattle(NPC npc)
    {
        StartCoroutine(EndBattleCoroutine());
    }

    private IEnumerator EndBattleCoroutine()
    {
        battleUIManager.SetNPCSprite(DeathSprite);

        StateManager.CurrentGameState = GameState.CutScene;

        yield return new WaitForSeconds(1f);

        StateManager.CurrentGameState = GameState.Gameplay;

        battleUIManager.EndBattle();
    }


    /// <summary>
    /// W zale¿noœci od powodzenia ataku gracza zadaje obra¿enia obecnemu NPC
    /// </summary>
    public void DealDamage(float precent)
    {
        StateManager.CurrentGameState = GameState.Fight;
        battleUIManager.StartFight();

        if (currentNPC == null) return;

        float dmg = player.GetAtack();
        dmg = dmg - (dmg * precent);

        currentNPC.TakeDamage((int)dmg);
    }
}