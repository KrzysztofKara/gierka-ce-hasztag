using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OSX;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{

    [SerializeField] private GameObject _BattleUI;

    [SerializeField] private BattleNPC battleNPC;

    [SerializeField] private GameObject _Panel;
    [SerializeField] private ActionPanel actionPanel;

    [SerializeField] private GameObject _FightScene;
    [SerializeField] private GameObject _AttackScene;
    [SerializeField] private GameObject _DialogueScene;
    [SerializeField] private GameObject _Inventory;

    [SerializeField] private ItemDescription itemDescription;
    [SerializeField] private BattleHP battleHP;
    [SerializeField] private BattleOptions battleOptions;
    [SerializeField] private InventorySlots inventorySlots;
    [SerializeField] private UsageOptions usageOptions;

    [SerializeField] private Player player;

    [SerializeField] private int SelectedItem;
    [SerializeField] private Menu CurrentMenu = Menu.Options;

    // --- Sta³e ---
    const int OptionsCount = 4;
    const int itemOptionsCount = 2;


    private void OnEnable()
    {
        Player.OnPlayerHpChanged += UpdateHP;
        player.inventory.OnInventoryChanged += UpdateSlots;
    }

    private void OnDisable()
    {
        Player.OnPlayerHpChanged -= UpdateHP;
        player.inventory.OnInventoryChanged -= UpdateSlots;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) { ChooseOptionOrItem(Direction.Up);} //Strza³ka w górê
            
        else if (Input.GetKeyDown(KeyCode.DownArrow)) { ChooseOptionOrItem(Direction.Down); }//Strza³ka w dó³

        else if (Input.GetKeyDown(KeyCode.LeftArrow)) { ChooseOptionOrItem(Direction.Left); }//Strza³ka w lewo

        else if (Input.GetKeyDown(KeyCode.RightArrow)) { ChooseOptionOrItem(Direction.Right); }//Strza³ka w prawo


        if (Input.GetKeyDown(KeyCode.Return))//Enter
        {
            ProgressUI();
        }

        if (Input.GetKeyDown(KeyCode.RightShift))//Shift
        {
            GetBack();
        }
    }





    public void StartBattle()
    {
        _BattleUI.SetActive(true);
        battleOptions.Activate(0);
        ResetOptions();
    }

    public void EndBattle()
    {
        _BattleUI.SetActive(false);
        ResetOptions();
    }

    public void ResetOptions()
    {
        _Inventory.SetActive(false);
        _FightScene.SetActive(false);
        _AttackScene.SetActive(false);
        CurrentMenu = Menu.Options;
        battleOptions.Activate(0);

    }


    /// <summary>
    /// W zale¿noœci od kierunku zacznacza odpowiedni¹ opcjê/item
    /// </summary>
    private void ChooseOptionOrItem(Direction direction)
    {
        //Jeœli nie jesteœmy w Menu to przerywamy dzia³anie
        if (StateManager.CurrentGameState != GameState.BattleMenu) { return; }

        switch (CurrentMenu)
        {
            case Menu.Options:
                battleOptions.Move(direction);
                break;

            case Menu.Inventory:
                inventorySlots.Move(direction);
                break;

            case Menu.ItemOptions:
                usageOptions.Move(direction);
                break;
        }
    }

    /// <summary>
    /// Wybiera zaznaczon¹ opcjê i progresuje UI
    /// </summary>
    private void ProgressUI()
    {
        //Jeœli nie jesteœmy w Menu to przerywamy dzia³anie
        if (StateManager.CurrentGameState != GameState.BattleMenu) { return; }

        if (CurrentMenu == Menu.Options)
        {
            switch (battleOptions.Select())
            {
                case 0:
                    _AttackScene.SetActive(true);
                    CurrentMenu = Menu.Attack;
                    
                    //Kod do progresowania Walki

                    break;
                case 1:
                    _DialogueScene.SetActive(true);
                    CurrentMenu = Menu.DialogueOptions;

                    //Kod do Dialogów

                    break;
                case 2:
                    _Inventory.SetActive(true);//W³¹czanie inventory
                    inventorySlots.Activate(0);//Aktywowanie serca
                    CurrentMenu = Menu.Inventory;//Ustawianie obecnego menu
                    break;
                case 3:
                    _DialogueScene.SetActive(true);
                    CurrentMenu = Menu.MercyOptions;

                    //Kod do Dialogów Mercy

                    break;
            }
        }
        else if (CurrentMenu == Menu.Inventory)
        {
            SelectedItem = inventorySlots.Select();
            itemDescription.gameObject.SetActive(true);
            usageOptions.Activate(0);
            CurrentMenu = Menu.ItemOptions;
            itemDescription.SetDescription(player.inventory.Items[SelectedItem].Description); //ustawianie opisu itemu
            
        }
        else if (CurrentMenu == Menu.ItemOptions)
        {
            switch(usageOptions.Select())
            {
                case 0:
                    player.UseItem(SelectedItem);
                    usageOptions.Off();
                    itemDescription.gameObject.SetActive(false);

                    //Kod do progresowania Walki (zmieniæ)
                    GetBack();

                    break;
                case 1:
                    GetBack();
                    break;
            }
            
        }
    }

    /// <summary>
    /// Cofamy sie w UI o jeden poziom.
    /// </summary>
    private void GetBack()
    {
        //Jeœli nie jesteœmy w Menu to przerywamy dzia³anie
        if (StateManager.CurrentGameState != GameState.BattleMenu) { return; }

        if (CurrentMenu == Menu.ItemOptions)
        {
            itemDescription.gameObject.SetActive(false);//Wy³¹czanie okna z opisem Itemu
            usageOptions.Off();//Wy³¹czanie serca
            CurrentMenu = Menu.Inventory;//Ustawiamy obecne Menu
            inventorySlots.Activate(SelectedItem);//Zaznaczamy item o którym 
        }
        else if (CurrentMenu == Menu.Inventory)
        {
            _Inventory.SetActive(false);
            inventorySlots.Off();
            CurrentMenu = Menu.Options;
            battleOptions.Activate(2);
        }
    }

    public void StartFight()
    {
        _AttackScene.SetActive(false);

        _FightScene.SetActive(true);

        actionPanel.SetSize(type: ActionPanelSize.Square);
    }

    public void EndFight()
    {
        _AttackScene.SetActive(false);
    }



    public void SetNPCSprite(Sprite sprite)
    {
        battleNPC.SetNPCsprite(sprite);
    }

    public void SetNPCBackground(Sprite sprite)
    {
        battleNPC.SetNPCBackground(sprite);
    }



    private void UpdateHP(int baseHP, int newHP)
    {
        battleHP.UpdateHP(baseHP, newHP);
    }

    private void UpdateSlots(int count)
    {
        inventorySlots.UpdateSlots(count);
    }
}
