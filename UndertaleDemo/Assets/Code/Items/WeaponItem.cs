namespace Gierka.Items 
{
    
    public class WeaponItem : Item
    {
        public int Damage { get; set; }


        public WeaponItem(string name, string description, int value, int damage, TypeOfItem itemType = TypeOfItem.Weapon) : base(name, description, value, itemType)
        {
            Damage = damage;
        }
    }
}
