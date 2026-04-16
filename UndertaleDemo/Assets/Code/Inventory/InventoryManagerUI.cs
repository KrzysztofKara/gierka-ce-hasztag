using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerUI : MonoBehaviour
{
    //Obiekty w UI podczas chodzenia po mapie
    [SerializeField] private GameObject _Inventory;
    [SerializeField] private GameObject _QuickInfo;
    [SerializeField] private GameObject _Stats;
    [SerializeField] private GameObject _Options;

    [SerializeField] private InventorySlots _SlotsScript;

    [SerializeField] private QuickInfo quickStats; // Skrypt dla wyœwietlania szybkich statystyk (uzupe³nianie informacji)
    [SerializeField] private Stats stats; // Skrypt dla wyœwietlania statystyk (uzupe³nianie informacji)
    [SerializeField] private UsageOptions usageOptions; // Skrypt dla aktywowania serca w opcjach u¿ycia itemów
    [SerializeField] private ItemDescription itemDescription; //Skrypt do opisu dla itemów
    [SerializeField] private Options options; // Skrypt dla aktywowania serca w opcjach
    [SerializeField] private Player player; // Skrypt gracza (statystyki, inventory)

    [SerializeField] private int OptionIndex = 0; // Na której opcji jesteœmy (Staty/Itemy)
    [SerializeField] private int ItemIndex = -1; // Na którym itemie jesteœmy
    [SerializeField] private int ItemOptionIndex = -1; // Na której opcji u¿ycia itemu jesteœmy

    [SerializeField] private Menu CurrentMenu = Menu.Options;

    // --- Sta³e ---
    const int optionsCount = 2;
    const int itemOptionsCount = 3;

    private void OnEnable()
    {
        Player.OnPlayerHpChanged += UpdateHP;
        Player.OnPlayerLvlChanged += UpdateLvl;
        Player.OnPlayerGoldChanged += UpdateGold;

        player.inventory.OnInventoryChanged += UpdateSlots;
        player.inventory.OnWeaponChanged += UpdateWeapon;
        player.inventory.OnArmorChanged += UpdateArmor;
    }

    private void OnDisable()
    {
        Player.OnPlayerHpChanged -= UpdateHP;
        Player.OnPlayerLvlChanged -= UpdateLvl;
        Player.OnPlayerGoldChanged -= UpdateGold;

        player.inventory.OnInventoryChanged -= UpdateSlots;
        player.inventory.OnWeaponChanged -= UpdateWeapon;
        player.inventory.OnArmorChanged -= UpdateArmor;
    }


    private void Start()
    {
        Item item1 = new Item("cep bojowy", "Potezna bron", 2137, TypeOfItem.Weapon, damage: 15);
        Item item2 = new Item("noz kuchenny", "Dobrze kroi sie nim chleb", 2137, TypeOfItem.Weapon, damage: 10);
        Item item3 = new Item("patyk", "Jakiœ patyk, chyba oogway'a", 2137, TypeOfItem.Weapon, damage: 6);


        //Item item2 = new Item("metalowe wiadro", "Jakies wiadro ktore znalazles u dziadka na gospodarstwie, ma dziure na oczy", 2137, TypeOfItem.Armor, protection: 24);
        //Item item2 = new Item("", "Jakies wiadro ktore znalazles u dziadka na gospodarstwie, ma dziure na oczy", 2137, TypeOfItem.Armor, protection: 24);
        //Item item2 = new Item("wiadro", "Jakies wiadro ktore znalazles u dziadka na gospodarstwie, ma dziure na oczy", 2137, TypeOfItem.Armor, protection: 24);

        //Item item3 = new Item("Odwar z Nagietka", "Uwazony przez cyrulika Henryka", 2137, TypeOfItem.Healing, healingAmount: 10);
        //Item item4 = new Item("patyk", "Jakiœ patyk, chyba oogway'a", 2137, TypeOfItem.Default);
        //Item item5 = new Item("patyk2", "Jakiœ patyk2", 2137, TypeOfItem.Default);




        //player.inventory.AddItem(item1);
        player.inventory.AddItem(item2);
        //player.inventory.AddItem(item4);
        //player.inventory.AddItem(item3);
        //player.inventory.AddItem(item5);



    }

    void Update()
    {
        if (_Inventory == null || _QuickInfo == null || _Inventory == null || _Options == null) 
        {
            Debug.LogWarning("Inventory, QuickInfo, Stats lub Options s¹ null");
            return;
        }


        //Jeœli gracz wciœnie ctrl i nie bêdzie w statystykach lub inventory to opcje i QuickInfo siê wyœwietl¹/schowaj¹
        if (SwitchInventoryUI())
        {

        }
        

        //Jeœli nie mamy w³¹czonego UI to nie mo¿emy siê po nim poruszaæ
        if (StateManager.CurrentGameState != GameState.MainMenu)
        {
            return;
        }

        //gracz wybiera opcjê/item
        ChooseOptionOrItem();

        //Jeœli gracz wciœnie Enter to przechodzimy dalej z UI (opcje ->Itemy/Staty -> Opcje u¿ycia itemu)
        ProgressUI();


        //Cofanie po wciœniêciu Shift'a
        GetBackOrClose();

    }


    //Aktywowanie lub wy³aczamie UI do Opcji i Szybkich statystyk (wybiera opcjê) 
    private bool SwitchInventoryUI()
    {
        if (Input.GetKeyDown(KeyCode.RightControl) && !_Inventory.activeSelf && !_Stats.activeSelf)
        {
            _Options.SetActive(!_Options.activeSelf);
            _QuickInfo.SetActive(!_QuickInfo.activeSelf);

            //zmiana stanu gry (jak otwiarte menu to jesteœmy w menu a jak nie to jest gameplay normalnie)
            if (_Options.activeSelf) StateManager.CurrentGameState =  GameState.MainMenu;
            else StateManager.CurrentGameState =  GameState.Gameplay;

            SelectOption(OptionIndex, true);

            return true;
        }
        return false;
    }

    //Aktualizowanie wszystkich Statystyk w Inventory UI 

    private void UpdateGold(int gold)
    {
        stats.SetGold(gold);
        quickStats.SetGold(gold);
    }

    private void UpdateHP(int hp, int baseHp)
    {
        stats.SetHp(hp, baseHp);
        quickStats.SetHp(hp, baseHp);
    }

    private void UpdateLvl(int lvl)
    {
        stats.SetLvl(lvl);
        quickStats.SetLvl(lvl);
    }

    private void UpdateWeapon(string name, int damage)
    {
        stats.SetWeapon(name);
        stats.SetAtack(damage);
    }

    private void UpdateArmor(string name, int protection)
    {
        stats.SetArmor(name);
        stats.SetDefence(protection);
    }

    private void UpdateSlots(int count)
    {
        _SlotsScript.UpdateSlots(count);
    }

    /// <summary>
    /// po wciœniêciu Shift'a patrzymy na to co jest otwarte i cofamy siê do poprzedniego elementu UI lub je zamykamy (ustawia te¿ CurrentMenu)
    /// </summary>
    private void GetBackOrClose()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            //Jak jesteœmy w Opcjach w pierwszym menu
            if (CurrentMenu == Menu.Options)
            {
                _Options.SetActive(false);
                _QuickInfo.SetActive(false);
                StateManager.CurrentGameState = GameState.Gameplay;
            }
            //Jesteœmy w Statystykach
            else if (CurrentMenu == Menu.Stats)
            {
                _Stats.SetActive(false);
                OptionIndex = 1;
                CurrentMenu = Menu.Options;

                SelectOption(1, true);
            }
            //Jak jesteœmu w Opcjach u¿ycia itemu
            else if (CurrentMenu == Menu.ItemOptions)
            {
                SelectOption(ItemOptionIndex, false);
                ItemOptionIndex = -1;

                CurrentMenu = Menu.Inventory;
                SelectOption(ItemIndex, true);
            }
            //Jak jesteœmy w inventory
            else if (CurrentMenu == Menu.Inventory)
            {
                _Inventory.SetActive(false);
                OptionIndex = 0;
                SelectOption(ItemIndex, false);

                ItemIndex = -1;
                CurrentMenu = Menu.Options;
                SelectOption(0, true);
            }
            else if (CurrentMenu == Menu.ItemDescriprion)
            {
                itemDescription.gameObject.SetActive(false);
                CurrentMenu = Menu.ItemOptions;
                SelectOption(ItemOptionIndex, true);
            }
        }
    }

    //po wciœniêciu enter patrzy na obecny wybór i zmienia UI (ustawia te¿ CurrentMenu)
    private void ProgressUI()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

            //Jak jesteœmy w opcjach to przenosimy siê do Inventory/Statystyk 
            if (OptionIndex == 0)
            {
                _Inventory.SetActive(true);
                SelectOption(OptionIndex, false);
                OptionIndex = -1;
                CurrentMenu = Menu.Inventory;

                //Jak nasze Inventory jest puste to nic nie wybieramy
                if (player.InventorySize() == 0) { return; }
                ItemIndex = 0;
                SelectOption(0, true);
            }
            else if (OptionIndex == 1)
            {
                _Stats.SetActive(true);
                SelectOption(OptionIndex, false);
                OptionIndex = -1;

                CurrentMenu = Menu.Stats;
            }
            //Wychodzenie z Opisu itemu
            else if (CurrentMenu == Menu.ItemDescriprion)
            {
                itemDescription.gameObject.SetActive(false);
                CurrentMenu = Menu.ItemOptions;
                SelectOption(ItemOptionIndex, true);
            }
            //U¿ycie itemu
            else if (ItemOptionIndex == 0)
            {
                player.UseItem(ItemIndex);
                CloseInventoryUI();
            }
            //Info o itemie
            else if (ItemOptionIndex == 1)
            {
                itemDescription.gameObject.SetActive(true);
                CurrentMenu = Menu.ItemDescriprion;
                SelectOption(ItemOptionIndex, false);

                itemDescription.SetDescription(player.inventory.Items[ItemIndex].Description);
            }
            //Wyrzucenie itemu
            else if (ItemOptionIndex == 2) 
            {
                player.inventory.RemoveItem(index:ItemIndex);
                CloseInventoryUI();
            }
            //Jak jesteœmy w Inventory to przechodzimy do Menu z opcjami itemu 
            else if (CurrentMenu == Menu.Inventory && player.InventorySize() > 0)
            {
                SelectOption(ItemIndex, false);
                CurrentMenu = Menu.ItemOptions;

                SelectOption(0, true);
                ItemOptionIndex = 0;
            }
        }
    }

    //Wybieranie Opcji/Itemów
    private void ChooseOptionOrItem()
    {
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                SetIndexes(i == 0 ? 9 : i - 1);
            }
        }
    }


    //ustawia indeksy
    private void SetIndexes(int index)
    {
        //Kiedy Jesteœmy w pierwszym menu i przekazany index jest 0/1 to zmieniamy wybran¹ opcjê
        if (CurrentMenu == Menu.Options && index < 2)
        {
            SelectOption(OptionIndex, false);
            OptionIndex = index;
            SelectOption(index, true); 
        }
        //Kiedy jesteœmy w menu z opcjami itemów to -||-
        else if (CurrentMenu == Menu.ItemOptions && index < 3)
        {
            SelectOption(ItemOptionIndex, false);
            ItemOptionIndex = index;
            SelectOption(index, true);
        }
        //Kiedy jesteœmy w menu z itemami to zmnieniamy wybrany item
        else if (CurrentMenu == Menu.Inventory && player.InventorySize() > index)
        {
            SelectOption(ItemIndex, false);
            ItemIndex = index;
            SelectOption(index, true);
        }
    }

    //aktywuje lub wy³¹cza Serca (sprawdza obecne menu)
    private void SelectOption(int index, bool action)
    {
        if (index  < 0) { return; }

        if (CurrentMenu == Menu.Options && index < optionsCount)
        {
            options.Active(index, action);
        }
        else if (CurrentMenu == Menu.ItemOptions && index < itemOptionsCount)
        {
            usageOptions.Active(index, action);
        }
        else if (CurrentMenu == Menu.Inventory)
        {
            if (player.InventorySize() <= index) { return; }

            _SlotsScript.Active(index, action);
        } 
    }

    //Wy³¹cza ca³e Inventory UI i ustawia wszystkie zmienne do stanu pocz¹tkowego 
    public void CloseInventoryUI()
    {
        StateManager.CurrentGameState = GameState.Gameplay;
        CurrentMenu = Menu.ItemOptions;
        SelectOption(ItemOptionIndex, false);
        CurrentMenu = Menu.Inventory;
        SelectOption(ItemIndex, false);
        CurrentMenu = Menu.Options;
        SelectOption(OptionIndex, false);


        _Inventory.SetActive(false);
        _Options.SetActive(false);
        _QuickInfo.SetActive(false);
        _Stats.SetActive(false);


        OptionIndex = 0;
        ItemIndex = -1;
        ItemOptionIndex = -1;

    }
}