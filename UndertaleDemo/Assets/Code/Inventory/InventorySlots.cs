using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlots : MonoBehaviour
{
    [SerializeField] int CurrIndex;

    [SerializeField] Player player;

    [SerializeField] private InventorySlot _SlotPrefab; // Prefab do generowania slotów w inventory
    [SerializeField] private Transform _SlotsParent; // Rodzic do którego bêd¹ do³¹czane prefaby slotów
    private List<InventorySlot> _slots = new(); // Lista w której bêd¹ przechowywane referencje do slotów

    /// <summary>
    /// Wy³¹cza wszystkie opcje
    /// </summary>
    public void Off()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            _slots[i].Active(false);
        }
        CurrIndex = 0;
    }

    /// <summary>
    /// Przesuwa aktywn¹ opcjê i ustawia obecny index 
    /// </summary>
    public void Move(Direction dir)
    {
        if (dir == Direction.Up)
        {
            Activate(CurrIndex - 2);
        }
        else if (dir == Direction.Down)
        {
            Activate(CurrIndex + 2);
        }
        else if (dir == Direction.Left)
        {
            //Jak jesteœmy w kolumnie to nie przeskakujemy do nowej tylko siê zatrzymujemy
            if(CurrIndex%2 == 0) { return; }

            Activate(CurrIndex - 1);
        }
        else if (dir == Direction.Right)
        {
            //Jak jesteœmy w kolumnie to nie przeskakujemy do nowej tylko siê zatrzymujemy
            if (CurrIndex % 2 == 1) { return; }
            Activate(CurrIndex + 1);
        }
    }

    /// <summary>
    /// Wybiera obecnie zaznaczon¹ opcjê 
    /// </summary>
    /// <returns>Index Opcji któr¹ zaznaczono (-1 jeœli nie by³a ¿adna zaznaczona)</returns>
    public int Select()
    {
        if (CurrIndex > -1 && CurrIndex < _slots.Count)
        {
            Active(CurrIndex, false);
            return CurrIndex;
        }
        return -1;
    }

    /// <summary>
    /// W³¹cza opcjê o podanym indexie, ustawia obecny index i wy³¹cza poprzednio aktywn¹ opcjê
    /// </summary>
    /// <returns>Informacjê czy uda³o siê wykonaæ akcjê</returns>
    public bool Activate(int index )
    {
        if (index > -1 && index < _slots.Count)
        {
            Active(CurrIndex, false);
            Active(index, true);
            CurrIndex = index;
            return true;
        }
        else
        {
            return false;
        }
    }




    /// <summary>
    /// Aktualizowanie Wszystkich Slotów w Inventory UI
    /// </summary>
    public void UpdateSlots(int count)
    {
        ClearSlots();

        CreateSlots(count);

        for (int i = 0; i < count; i++)
        {
            SetItemNameInSlot(i, player.inventory.Items[i].Name);
        }
    }

    //Tworzy okreœlon¹ liczbê slotów w Inventory UI
    private void CreateSlots(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            InventorySlot slot = Instantiate(_SlotPrefab, _SlotsParent, false);
            slot.SetItemName("Empty");
            _slots.Add(slot);
        }
    }

    //Ustawia Nazwê itemu w Slocie
    private void SetItemNameInSlot(int index, string itemName)
    {
        if (index < 0 || index >= _slots.Count)
            return;

        _slots[index].SetItemName(itemName);
        _slots[index].name = itemName;
    }

    //Usuwa slot o podanym id
    private void RemoveSlot(int index)
    {
        if (index < 0 || index >= _slots.Count)
            return;

        Destroy(_slots[index].gameObject);
        _slots.RemoveAt(index);
    }

    //Usuwa wszystkie sloty
    private void ClearSlots()
    {
        foreach (InventorySlot slot in _slots)
        {
            Destroy(slot.gameObject);
        }

        _slots.Clear();
    }



    /// <summary>
    /// Aktywuje/Deaktywuje Serce elementu o podamnym id (Starszy system u¿ywany w Menu g³ównym)
    /// </summary>
    public void Active(int index, bool action)
    {
        if (index < 0 || index > _slots.Count) { return; }

        _slots[index].Active(action);
    }
}