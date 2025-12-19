using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Classes_For_IN
{
    public class InventorySlot
    {
        public Item Item { get; set; }
        public int Count { get; set; }

        public InventorySlot(Item item, int count)
        {
            Item = item;
            Count = count;
        }
    }
}