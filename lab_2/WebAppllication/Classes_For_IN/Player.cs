using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Classes_For_IN
{
    public class Player
    {
        public int Base_Armor {get; private set;}
        private bool plusing_armor = false;
        private Armor arm_on_p;
        public int Base_Damage {get; private set;}
        private bool plusing_dam = false;
        private Weapon wep_on_p;
        public int Base_Health {get; private set;}
        private Inv_System Inventory = new Inv_System();
        public string Nickname {get; private set;}

        public Player(int ar, int dam, int hel, string nick)
        {
            Base_Armor = ar;
            Base_Damage = dam;
            Base_Health = hel;
            Nickname = nick;
        }


//________________________________________________________________________
        public void add_heal(int cnt) {Base_Health += cnt;}
        public void add_armorcnt(int cnt) {Base_Armor += cnt;}
        public void add_damage(int cnt) {Base_Damage += cnt;}

        public bool Equip_Weapon(Weapon wep)
        {
            if(Inventory.It_Has(wep) && plusing_dam == false)
            {
                wep_on_p = wep;
                plusing_dam = true;
                this.add_damage(wep.Damage);
                return true;
            }
            return false;
        }

        public bool Equip_Armor(Armor armor)
        {
            if (Inventory.It_Has(armor) && plusing_armor == false)
            {
                arm_on_p = armor;
                plusing_armor = true;
                this.add_armorcnt(armor.Strength);
                return true;
            }
            return false;
        }
//_________________________________________________________________________


        public int Take_Item(Item itm, int count)
        {
            return Inventory.add_item(itm, count);
        }

        public int Drop_Item(Item itm, int count)
        {
            return Inventory.remove_item(itm, count);
        }

        public List<InventorySlot> Get_Items()
        {
            return Inventory.GetSlots();
        }

        public bool Use_Item(Item it)
        {
            if (it is Is_Usable u) 
            {
                u.Use_Item(this);
                return true; 
            }
            return false;
        }

        public bool Unequip_Armor()
        {
            if (plusing_armor)
            {
                Base_Armor -= arm_on_p.Strength;
                arm_on_p = null;
                plusing_armor = false;
                return true;
            }
            return false;
        }

        public bool Unequip_Weapon()
        {
            if (plusing_dam)
            {
                Base_Damage -= wep_on_p.Damage;
                wep_on_p = null;
                plusing_dam = false;
                return true;
            }
            return false;
        }

        public bool Combine_Item(Item first, Item second)
        {
            if (!first.CanCombine || !second.CanCombine) return false;
            if (first.GetType() != second.GetType()) return false;    
            if (first is Poiton o && second is Poiton s)
            {
                if (o.Typee != s.Typee) return false;
            }

            var combi = Inventory.Can_Comb(first, second);

            if (combi == 1)
            {
                Rare new_rare;
                if (first.Rarity == Rare.legendary)
                {
                    new_rare = Rare.legendary;
                }
                else
                {
                    int next_rare_value = (int)first.Rarity + 1;
                    new_rare = (Rare)next_rare_value;
                }

                if (first is Is_Combinable comb)
                {
                    Item new_item = comb.Combine_Items(second, new_rare);

                    Inventory.remove_item(first, 1);
                    Inventory.remove_item(second, 1);

                    Inventory.add_item(new_item, 1);
                    return true;
                }
                return false;
            }
            else if (combi == 2)
            {
                return false;
            }
            else
            {
                Inventory.remove_item(first, 1);
                Inventory.remove_item(second, 1);
                return false;
            }
        }

        public bool Attack(Player other)
        {
            if (plusing_dam)
            {
                wep_on_p.Attack_by();
            }
            other.Take_Damage(Base_Damage);
            return true;
        }

        public bool Take_Damage(int dam)
        {
            if(dam >= Base_Health + Base_Armor)
            {
                Base_Health = 0;
                Base_Armor = 0;
                return false;
            }
            int curr_dam = 0;
            if(dam >= Base_Armor)
            {
                curr_dam = dam - Base_Armor;
                Base_Health -= curr_dam;
                return true;
            }
            else
            {
                Base_Armor -= dam;
                return true;
            }
        }

        public bool Clear_Inventory()
        {
            bool arm = this.Unequip_Armor();
            bool wep = this.Unequip_Weapon();
            bool inv = Inventory.Clear_Inv(true);
            if(inv){return true;}
            return false;
        }

        public List<int> Get_Player_Stats()
        {
            return new List<int>(){Base_Health, Base_Damage, Base_Armor};
        }

    }

}
