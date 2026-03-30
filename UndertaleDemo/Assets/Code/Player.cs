using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory = new ();
    public int LVL = 1;
    public int EXP = 0;
    public int GOLD = 0;
    public int BaseHP = 20;
    public int HP = 15;
    public int BaseDamage = 5;

    public static event Action<int> OnPlayerLvlChanged;
    public static Action<int> OnPlayerGoldChanged;
    public static Action<int> OnPlayerBaseHpChanged;
    public static Action<int, int> OnPlayerHpChanged;

    private void Start()
    {
        OnPlayerLvlChanged?.Invoke(LVL);
        OnPlayerGoldChanged?.Invoke(GOLD);
        OnPlayerHpChanged?.Invoke(HP, BaseHP);
    }


    public int GetAtack()
    {
        if (inventory.Weapon != null)
        {
            return inventory.Weapon.Damage + BaseDamage;
        }
        return BaseDamage;
    }

    public int GetDefence()
    {
        if (inventory.Armor != null)
        {
            return inventory.Armor.Protection;
        }
        return 0;
    }
    
    public void UseItem(int index)
    {
        inventory.UseItem(index, this);
    }

    public void Heal(int amount)
    {
        if (amount + HP > BaseHP)
        {
            HP = BaseHP;
        }
        else
        {
            HP += amount;
        }
        OnPlayerHpChanged(HP, BaseHP);
    }
}
