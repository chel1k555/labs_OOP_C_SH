using System.Collections.Generic;

namespace WebApplication.Classes_For_IN
{
    public class Armor : Item, Is_Usable, Is_Combinable // броня, один общий предмет на все тело)
    {
        public int Strength {get; private set;}
        public Armor(int id, bool comb, string name, int str, Rare rare) : base(id, comb, name, rare, false)
        {
            Strength = this.rarity(rare, str);
        }

        public void Use_Item(Player player)
        {
            player.Equip_Armor(this);
        }

        public Item Combine_Items(Item other, Rare new_rare)
        {
            Armor sec = (Armor)other;
            int new_str = (this.Strength + sec.Strength) * 10 / 12;
            return new Armor(this.Id, this.CanCombine, this.Name, new_str, new_rare);
        }

    }
}
