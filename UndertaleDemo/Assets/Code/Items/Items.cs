namespace Gierka.Items 
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public TypeOfItem ItemType { get; set; }


        public Item(string name, string description, int value, TypeOfItem itemType = TypeOfItem.Default)
        {
            Name = name;
            Description = description;
            Value = value;
            ItemType = itemType;
        }
    }


    public enum TypeOfItem
    {
        Default,
        Healing,
        Weapon,
        Armor
    }
}
