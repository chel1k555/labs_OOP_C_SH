using System.Collections.Generic;

namespace WebApplication.Classes_For_IN
{
    public class Poiton : Item, Is_Usable, Is_Combinable // +здоровье1 +броня2 +урон3
    {
        public int Effect {get; private set;}
        private int Count_Use = 2;
        public int Typee {get; private set;}
        public Poiton(int id, bool comb, string name, Rare rare, int what, int eff) : base(id, comb, name, rare, false)
        {
            Effect = rarity(rare, eff);
            Typee = what;
        }

        public void Use_Item(Player player)
        {
            if(Typee == 1)
            {
                player.add_heal(Effect);
            }
            if(Typee == 2)
            {
                player.add_armorcnt(Effect);
            }
            if(Typee == 3)
            {
                player.add_damage(Effect);
            }
        }

        public Item Combine_Items(Item other, Rare new_rare)
        {
            Poiton sec = (Poiton)other;
            int new_eff = (sec.Effect + this.Effect)*10/12;
            string new_name = "super" + this.Name;
            return new Poiton(this.Id, this.CanCombine, new_name, new_rare, this.Typee, new_eff);
        }
    }
}