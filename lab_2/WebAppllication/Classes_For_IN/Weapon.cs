using System.Collections.Generic;

namespace WebApplication.Classes_For_IN
{
    public class Weapon : Item, Is_Usable, Is_Combinable // мечи, щиты, ну наверно лук
    {
        public int Count_Uses {get; private set;}
        public int Damage {get; private set;}
        public Weapon(int id, bool comb, string name, int str, Rare rare) : base(id, comb, name, rare, false)
        {
            Damage = rarity(rare, str);
            Count_Uses = ((int)rare) * 100;
        }

        public void Use_Item(Player player)
        {
            player.Equip_Weapon(this);
        }

        public Item Combine_Items(Item other, Rare new_rare)
        {
            Weapon second = (Weapon)other; 
            int new_dam = (this.Damage + second.Damage) / 2;
            return new Weapon(this.Id, true, this.Name, new_dam, new_rare);
        }

        public void Attack_by()
        {
            Count_Uses -= 1;
        }
    }
}
