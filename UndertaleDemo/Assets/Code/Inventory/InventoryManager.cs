using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject _Inventory;
    [SerializeField] GameObject _QuickInfo;
    [SerializeField] GameObject _Stats;
    [SerializeField] GameObject _Options;

    //Na której opcji jesteœmy (Staty/Itemy)
    private int OptionIndex = 1;
    //Na którym itemie jesteœmy
    private int ItemIndex = -1;
    //Na której opcji u¿ycia itemu jestesmy
    private int UsageOptionIndex = -1;




    void Update()
    {
        if (_Inventory == null || _QuickInfo == null || _Inventory == null || _QuickInfo == null) 
        {
            Debug.LogWarning("Inventory, QuickInfo, Stats lub Options s¹ null");
            return;
        }


        //Jeœli gracz wciœnie ctrl i nie bêdzie w statystykach lub inventory to opcje i QuickInfo siê wyœwietl¹/schowaj¹
        SwichInventoryUI();

        //Jeœli nie mamy w³¹czonego UI to nie mo¿emy siê po nim poruszaæ
        if (!_Options.activeSelf)
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


    private void SwichInventoryUI()
    {
        if (Input.GetKeyDown(KeyCode.RightControl) && !_Inventory.activeSelf && !_Stats.activeSelf)
        {
            _Options.SetActive(!_Options.activeSelf);
            _QuickInfo.SetActive(!_QuickInfo.activeSelf);

            SelectOption(OptionIndex);
        }
    }

    //po wciœniêciu Shift'a patrzymy na to co jest otwarte i cofamy siê do poprzedniego elementu UI lub je zamykamy
    private void GetBackOrClose()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (OptionIndex > -1)
            {
                _Options.SetActive(false);
                _QuickInfo.SetActive(false);
            }
            else if (_Stats.activeSelf)
            {
                _Stats.SetActive(false);
                OptionIndex = 1;

                SelectOption(1);
            }
            else if (ItemIndex > -1)
            {
                _Inventory.SetActive(false);
                OptionIndex = 0;

                SelectOption(0);
            }
            else if (UsageOptionIndex > -1)
            {
                ItemIndex = 0;
                UsageOptionIndex = -1;
            }
        }
    }

    //po wciœniêciu enter patrzy na wybór 
    private void ProgressUI()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

            //Jak jesteœmy w opcjach to wybieramy gdzie chcemy przejœæ
            if (OptionIndex == 0)
            {
                _Inventory.SetActive(true);
                OptionIndex = -1;
                ItemIndex = 0;
                

                UnSelectOption(0);

            }
            else if (OptionIndex == 1)
            {
                _Stats.SetActive(true);
                OptionIndex = -1;

                UnSelectOption(1);
            }
        }
    }

    
    private void ChooseOptionOrItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetIndexes(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetIndexes(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetIndexes(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetIndexes(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetIndexes(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetIndexes(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SetIndexes(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SetIndexes(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SetIndexes(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetIndexes(9);
        }
    }


    //ustawia indexy dla "wskaŸników"
    private void SetIndexes(int index)
    {
        if (OptionIndex > -1 && index < 2)
        {
            OptionIndex = index;
            SelectOption(index);
            UnSelectOption(Math.Abs(index - 1));
        }
        else if (ItemIndex > -1)
        {
            ItemIndex = index;
        }
        else if (UsageOptionIndex > -1 && index < 3)
        {
            UsageOptionIndex = index;
        }
    }

    //Dokopuje siê do grafiki serca w opcjach
    private void SelectOption(int index)
    {
        if (index  < 0) { return; }

        GameObject Heart = _Options.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.transform.GetChild(1).gameObject;

        Heart.SetActive(true);
    }
    private void UnSelectOption(int index)
    {
        if (index < 0) { return; }

        GameObject Heart = _Options.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.transform.GetChild(1).gameObject;

        Heart.SetActive(false);
    }

}
