using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Custom/new Item")]
public class Item : ScriptableObject
{
    public string Name;
    public string Description;
    public int Value;
    public TypeOfItem ItemType;
    public int HealingAmount;
    public int NumberOfUses;
    public int Protection;
    public int Damage;


    public Item(string name, string description, int value, TypeOfItem itemType = TypeOfItem.Default, int healingAmount = 0, int numberOfUses = 1, int protection = 0, int damage = 0)
    {
        Name = name;
        Description = description;
        Value = value;
        ItemType = itemType;
        HealingAmount = healingAmount;
        NumberOfUses = numberOfUses;
        Protection = protection;
        Damage = damage;
    }
}


public enum TypeOfItem
{
    Default,
    Healing,
    Weapon,
    Armor
}

