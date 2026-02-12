namespace Gierka.Items
{
    public class ArmorItem : Item
    {
        public int Protection { get; set; }


        public ArmorItem(string name, string description, int value, int protection, TypeOfItem itemType = TypeOfItem.Armor) : base(name, description, value, itemType)
        {
            Protection = protection;
        }
    }
}
