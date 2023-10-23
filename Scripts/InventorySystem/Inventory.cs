using System.Collections.Generic;
using System.Linq;

namespace XGeneric.Inventory
{
    public class Inventory
    {
        public Item[] items;
        public int size;

        public Inventory(int InventorySize)
        {
            items = new Item[InventorySize];
            size = InventorySize;
        }
        //Adds item to inventory 
        public bool AddItem(Item itemToAdd)
        {
            for(int i = 0; i < size; i++)
            {
                if(items[i] == null)
                {
                    items[i] = itemToAdd;
                    return true;
                }
            }
            return false;
        }
        //Remove item by given ID
        public void RemoveItem(int ID)
        {
            items[ID] = null;
        }
    }
}