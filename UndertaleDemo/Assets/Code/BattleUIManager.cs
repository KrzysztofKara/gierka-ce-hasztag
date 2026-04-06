using UnityEngine;


public class BattleUIManager : MonoBehaviour
{
    [SerializeField] GameObject _BattleUI;

    [SerializeField] BattleNPC BattleNPCScript;


    [SerializeField] GameObject _Panel;
    [SerializeField] ActionPanel ActionPanelScript;

    [SerializeField] GameObject _FightScene;
    [SerializeField] GameObject _AttackScene;
    [SerializeField] GameObject _DialogueScene;
    [SerializeField] GameObject _Inventory;


    [SerializeField] BattleHP BattleHPScript;


    [SerializeField] BattleOptions Options;


    [SerializeField] int currOption = -1;

    private void OnEnable()
    {
        Player.OnPlayerHpChanged += UpdateHP;
    }

    private void OnDisable()
    {
        Player.OnPlayerHpChanged -= UpdateHP;
    }


    private void Update()
    {
        if (StateManager.CurrentGameState != GameState.BattleMenu) return;

        //for (int i = 0; i < 10; i++)
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha0 + i))
        //    {
        //        SelectOption(i == 0 ? 9 : i - 1, true);
        //        currOption = i -1;
        //    }
        //}
    }


    private void UpdateHP(int baseHP, int newHP)
    {
        BattleHPScript.UpdateHP(baseHP, newHP);
    }

    //private void SelectOption(int  index, bool action)
    //{
    //    if (currOption > -1 && currOption < 4) deactivate(currOption, false);
    //    deactivate(index, action);
    //} 

    //private void deactivate(int index, bool action)
    //{
    //    if (index > -1)
    //    {
    //        if (index < 4)
    //        {
    //            Options.SelectOption(index, action);
    //        }
    //    }
    //}
}
