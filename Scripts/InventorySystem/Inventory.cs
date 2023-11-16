using Godot;

namespace XGeneric.Inventory
{
	public class Inventory
	{
		public InvItem[] items;
		public EQ eq = new (3);
		public int size;
		
		public uint cash = 333;

		public Inventory(int InventorySize)
		{
			items = new InvItem[InventorySize];
			size = InventorySize;
		}
		//Adds item to inventory (Returns item ID)
		public int AddItem(InvItem itemToAdd)
		{
			for(int i = 0; i < size; i++)
			{
				if(items[i] == null)
				{
					items[i] = itemToAdd;
					return i;
				}
			}
			return -1;
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
		
		public void EquipItem(int itemID, int eqID)
		{
			(eq.EQSlots[eqID], items[itemID]) = (items[itemID], eq.EQSlots[eqID]);
		}
		
		public void UnEquipItem(int eqID, int slotID)
		{
			(eq.EQSlots[eqID], items[slotID]) = (items[slotID], eq.EQSlots[eqID]);
		}
	}
}