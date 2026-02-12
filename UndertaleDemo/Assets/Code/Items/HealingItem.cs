namespace Gierka.Items
{
    public class HealingItem : Item
    {
        public int HealingAmount { get; set; }
        public int NumberOfUses { get; set; }


        public HealingItem(string name, string description, int value, int healingAmount, int numberOfUses = 1, TypeOfItem itemType = TypeOfItem.Healing) : base(name, description, value, itemType)
        {
            HealingAmount = healingAmount;
            NumberOfUses = numberOfUses;
        }
    }
}
