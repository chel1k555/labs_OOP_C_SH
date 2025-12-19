using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Classes_For_IN{
    public class Inv_System
    {
        private readonly Random chan = new Random();
        private List<InventorySlot> Slots = new List<InventorySlot>();
        private int MaxSlots = 36;

        public bool It_Has(Item itm) {return Slots.Any(s => s.Item.Id == itm.Id);}

        public List<InventorySlot> GetSlots()
        {
            return new List<InventorySlot>(Slots);
        }

        public int add_item(Item it, int count)
        {
            int max_stack = 0;
            if (it.CanStack)
            {
                if (it.CanStack)
                {
                    max_stack = 64;
                }
                var can_add_inslot = Slots.FirstOrDefault(s => s.Item.Id == it.Id && s.Count < max_stack);

                if (can_add_inslot != null)
                {
                    int can_add = max_stack - can_add_inslot.Count;
                    int for_add = Math.Min(can_add, count);
                    can_add_inslot.Count += for_add;
                    count -= for_add;
                }
                else
                {
                    return 2;
                }
                if(count > 0)
                {
                    if(Slots.Count >= MaxSlots) {return 22;}
                    int for_add = Math.Min(count, max_stack);
                    InventorySlot new_slot = new InventorySlot(it, for_add);
                    Slots.Add(new_slot);
                    return 11;
                }
                return 1;
            }
            else
            {
                max_stack = 1;
                InventorySlot new_slot = new InventorySlot(it, max_stack);
                if(Slots.Count >= MaxSlots){return 22;}
                Slots.Add(new_slot);
                return 1;
            }
        }

        public int remove_item(Item it, int count)
        {
            int max_stack = 0;
            var removing_slot = Slots.FirstOrDefault(s => s.Item.Id == it.Id);
            if(removing_slot != null){
                if (it.CanStack)
                {
                    max_stack = 64;
                    if(count > max_stack){count = 64;}
                    if(removing_slot.Count <= count)
                    {
                        Slots.Remove(removing_slot);
                        return 11;
                    }
                    else
                    {
                        removing_slot.Count -= count;
                        return 1;
                    }
                }
                else
                {
                    max_stack = 1;
                    count = 1;
                    Slots.Remove(removing_slot);
                    return 11;
                }
            }
            return 200;
        }

        public int Can_Comb(Item one, Item two)// если сильно разные, то удаление
        {
            if((one.Rarity == Rare.legendary && two.Rarity == Rare.normal) || (one.Rarity == Rare.normal && two.Rarity == Rare.legendary))
            {
                return 10;
            }
            else
            {
                int chance = chan.Next(1, 3);
                if(chance == 1){return 1;}
                else{return 2;}
            }
        }

        public bool Clear_Inv(bool realy)
        {
            if(realy)
            {
                Slots.Clear();
                return true;
            }
            return false;
        }
    }
}