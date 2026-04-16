using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> Items { get; private set; }
    public Item Weapon { get; private set; }
    public Item Armor { get; private set; }
    public int Capacity { get; set; }

    public event Action<int> OnInventoryChanged; //Przekazuje rozmiar nowego Inventory
    public event Action<string, int> OnArmorChanged; //Przekazuje nazwê i Protection Armor'a
    public event Action<string, int> OnWeaponChanged; //Przekazuje nazwê i damage Broni
    public event Action<Item> OnItemUse; //U¿ycie itemu

    public Inventory(int capacity = 10, Item weapon = null, Item armor = null)
    {
        Items = new List<Item>();
        Weapon = weapon;
        Armor = armor;
        Capacity = capacity;
    }

    public bool AddItem(Item item)
    {
        if (item == null)
        {
            Debug.LogWarning("Przekazany item = null");
            return false;
        }

        if (Items.Count >= Capacity)
        {
            Debug.Log("Brak miejsca w Ekwipunku");
            return false;
        }

        Items.Add(item);
        OnInventoryChanged?.Invoke(Items.Count); //Jak dodajemy item do Inventory to odpalamy event ¿eby np. UI siê odœwie¿y³o
        return true;
    }

    public bool RemoveItem(Item item = null, int index = -1)
    {
        if (item == null && (index < 0 || index >= Items.Count)) { return false; }

        if (index > -1 && index < Items.Count)
        {
            Items.RemoveAt(index);
            OnInventoryChanged?.Invoke(Items.Count); //Po usuniêciu itemu odpalamy event ¿eby UI siê odœwie¿y³o
            return true;
        }

        if (Items.Remove(item))
        {
            OnInventoryChanged?.Invoke(Items.Count); //Po usuniêciu itemu odpalamy event ¿eby UI siê odœwie¿y³o
            return true;
        }

        return false;
    }

    public bool SetWeapon(Item weapon)
    {
        if (weapon == null)
        {
            Debug.LogWarning("Przekazany item = null");
            return false;
        }
        else if (weapon.ItemType != TypeOfItem.Weapon)
        {
            Debug.Log("Przekazany item nie jest 'Weapon'");
            return false;
        }

        Weapon = weapon;
        RemoveItem(weapon);
        OnWeaponChanged?.Invoke(weapon.Name, weapon.Damage); //Odpalamy event ¿eby UI siê odœwie¿y³o
        return true;
    }

    public bool SetArmor(Item armor)
    {
        if (armor == null)
        {
            Debug.LogWarning("Przekazany item = null");
            return false;
        }
        if (armor.ItemType != TypeOfItem.Armor)
        {
            Debug.LogWarning("Przekazany item nie jest 'Armor'");
            return false;
        }

        Armor = armor;
        RemoveItem(armor);
        OnArmorChanged?.Invoke(armor.Name, armor.Protection); //Odpalamy event ¿eby UI siê odœwie¿y³o
        return true;
    }

    public void UseItem(int index, Player player)
    {
        Item item = Items[index];
        OnItemUse?.Invoke(Items[index]);
        Debug.Log("dosz³o");

        switch (item.ItemType)
        {
            case TypeOfItem.Weapon:
                SetWeapon(item); break;
            case TypeOfItem.Armor:
                SetArmor(item); break;
            case TypeOfItem.Healing:
                player.Heal(item.HealingAmount);
                RemoveItem(item);
                break;
        }
        
    }

}
