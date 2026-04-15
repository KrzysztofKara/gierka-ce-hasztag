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

        player.inventory.OnItemUse += ItemUsed;
    }
    private void OnDisable()
    {
        NPC.OnPlayerMeetNPC -= StartBattle;
        NPC.OnNPCDeath -= EndBattle;
        AttackSelector.OnSliderSelected -= DealDamage;
    }



    /// <summary>
    /// rozpoczyna walkê z podanym NPC
    /// </summary>
    private void StartBattle(NPC npc)
    {
        currentNPC = npc;

        battleUIManager.StartBattle();
        battleUIManager.SetNPCSprite(npc.battleSprite);
        StateManager.CurrentGameState = GameState.BattleMenu;

        Debug.Log("Rozpoczêto walkê z: " + npc.npcName);
    }

    /// <summary>
    /// ustawia sprite nie¿yj¹cego NPC i po sekundzie koñczy walkê
    /// </summary>
    private void EndBattle(NPC npc)
    {
        StartCoroutine(EndBattleCoroutine(npc));
    }
    private IEnumerator EndBattleCoroutine(NPC npc)
    {
        if (npc.currentHP < 1) //Jak NPC ded³ to ustawiamy sprite'a œmierci
        { 
            battleUIManager.SetNPCSprite(DeathSprite);
            battleUIManager.EndBattle($"Wygrales! Zyskujesz {npc.Gold} Golda i {npc.EXP} Exp'a");
            player.ClaimReward(npc.Gold, npc.EXP);
        }
        else
        {
            npc.canFight = false; //Zmieniamy npc ¿eby ju¿ nie powodowa³ walki

            battleUIManager.EndBattle($"Wygrales! Zyskujesz {npc.Gold/2} Golda i {npc.EXP/2} Exp'a");
            player.ClaimReward(npc.Gold/2, npc.EXP/2);
        }

        StateManager.CurrentGameState = GameState.CutScene;

        yield return new WaitForSeconds(2f); //czekamy przez 2 sekundy

        StateManager.CurrentGameState = GameState.Gameplay;
        battleUIManager._BattleUI.SetActive(false);
    }

    /// <summary>
    /// W zale¿noœci od powodzenia ataku gracza zadaje obra¿enia obecnemu NPC
    /// </summary>
    private void DealDamage(float precent)
    {
        if (currentNPC == null) return;

        float dmg = player.GetAtack();
        dmg = dmg - (dmg * precent); //W zale¿noœci jak dobry by³ atak gracza zadajemy odpowieni damage

        if (!currentNPC.TakeDamage((int)dmg)) currentNPC = null; //Jeœli NPC nam dednie to go usuwamy

        StartFight();
    }
    /// <summary>
    /// Rozpoczyna walkê (jeœli NPC ¿yje)
    /// </summary>
    private void StartFight()
    {
        if (currentNPC == null) return;
        StateManager.CurrentGameState = GameState.Fight;
        battleUIManager.StartFight();
    }

    //Koñczy walkê 
    private void EndFight()
    {
        StateManager.CurrentGameState = GameState.BattleMenu;
        battleUIManager.EndFight();
    }



    private void ItemUsed(Item item)
    {
        StartFight();
    }
}