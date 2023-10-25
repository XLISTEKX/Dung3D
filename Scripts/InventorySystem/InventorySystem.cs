using System.Collections.Generic;
using Godot;

namespace XGeneric.Inventory
{
	public static class InventorySystem
	{
		public static InvItem GetItemByID(int ID)
		{
			return ResourceLoader.Load<InvItem>("res://Prefabs/Items/Item(" + ID + ").tres");
		}
		public static InvItem[] GetAllItems()
		{
			DirAccess dir  = DirAccess.Open("res://Prefabs/Items");
			string[] itemNames = dir.GetFiles();
			
			List<InvItem> items = new();
			
			foreach(string name in itemNames)
			{
				items.Add(ResourceLoader.Load<InvItem>("res://Prefabs/Items/" + name));
			}
			
			return items.ToArray();
		}
	}
}