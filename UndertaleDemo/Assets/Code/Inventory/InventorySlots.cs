using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlots : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] private InventorySlot _SlotPrefab; // Prefab do generowania slotów w inventory
    [SerializeField] private Transform _SlotsParent; // Rodzic do którego bêd¹ do³¹czane prefaby slotów
    private List<InventorySlot> _slots = new(); // Lista w której bêd¹ przechowywane referencje do slotów


    /// <summary>
    /// Aktywuje/Deaktywuje Serce elementu o podamnym id
    /// </summary>
    public void Active(int index, bool action)
    {
        if (index < 0 || index >= _slots.Count  ) { return; }

        _slots[index].Active(action);
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
}
