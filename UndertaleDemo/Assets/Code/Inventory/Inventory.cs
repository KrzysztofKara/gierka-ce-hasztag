using System.Collections.Generic;
using UnityEngine;
using Gierka.Items;

namespace Gierka.Inventory 
{
    public class Inventory
    {
        public List<Item> Items { get; private set; }
        public WeaponItem Weapon { get; set; } 
        public ArmorItem Armor { get; set; }
        public int Capacity { get; set; }


        public Inventory(int capacity = 10, WeaponItem weapon = null, ArmorItem armor = null) 
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
            return true;
        }

        public bool RemoveItem(Item item)
        {
            if (item  == null)
            {
                Debug.LogWarning("Przekazany item = null");
                return false;
            }

            if (Items.Remove(item))
            {
                return true;
            }
            return false;
        }
        
        public bool SetWeapon(WeaponItem weapon)
        {
            if (weapon == null)
            {
                Debug.LogWarning("Przekazany item = null");
                return false;
            }

            Weapon = weapon;
            return true;
        }

        public bool SetArmor(ArmorItem armor)
        {
            if (armor == null)
            {
                Debug.LogWarning("Przekazany item = null");
                return false;
            }

            Armor = armor;
            return true;
        }
    }
}
