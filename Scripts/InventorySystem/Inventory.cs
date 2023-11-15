using Godot;

namespace XGeneric.Inventory
{
	public class Inventory
	{
		public InvItem[] items;
		public int size;
		
		public uint cash = 1000;

		public Inventory(int InventorySize)
		{
			items = new InvItem[InventorySize];
			size = InventorySize;
		}
		//Adds item to inventory 
		public bool AddItem(InvItem itemToAdd)
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
		
		public void ReplaceItem(int firstID, int secID)
		{
			(items[secID], items[firstID]) = (items[firstID], items[secID]);
		}

		//Exchange Item from this inventory with other inventory
		public void ExchangeItem(int firstItemID, int secIDsecInventory, Inventory inventory)
		{
            (inventory.items[secIDsecInventory], items[firstItemID]) = (items[firstItemID], inventory.items[secIDsecInventory]);
        }
    }
}