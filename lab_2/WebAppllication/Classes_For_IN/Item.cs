using System.Collections.Generic;

namespace WebApplication.Classes_For_IN
{
    public interface Is_Usable
    {
        void Use_Item(Player player);
    }

    public interface Is_Combinable
    {
        Item Combine_Items(Item other, Rare new_rare);
    }

    public enum Rare
    {
        legendary = 4,
        epic = 3,
        rare = 2,
        normal = 1
    }

    public abstract class Item
    {
        public int Id { get; private set; }
        public bool CanCombine {get; private set;}
        public string Name {get; set;}
        public Rare Rarity {get; private set;}
        public bool CanStack{get; private set;}

        public Item(int id, bool comb, string name, Rare rare, bool canst)
        {
            Id = id;
            CanCombine = comb;
            Name = name;
            Rarity = rare;
            CanStack = canst;
        }

        public int rarity(Rare bse, int ch)
        {
            double cnt = ch;
            if(bse == Rare.normal){ cnt += cnt * 0.1; }
            if(bse == Rare.rare){ cnt += cnt * 0.15; }
            if(bse == Rare.epic){ cnt += cnt * 0.25; }
            if(bse == Rare.legendary){ cnt += cnt * 0.4; }
            return (int)cnt;
        }
    }
}