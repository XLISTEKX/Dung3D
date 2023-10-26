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
	
		public static Color GetColorByRarity(RarityTypes rarity)
		{
			switch(rarity)
			{
				case RarityTypes.Common:
					return new Color(110 / 255f, 110 / 255f, 110 / 255f);
				case RarityTypes.Unrare:
					return new Color(0,1,0);
				case RarityTypes.Rare:
					return new Color(0,0,1);
				case RarityTypes.Epic:
					return new Color(102 / 255f, 0, 212 / 255f);
				case RarityTypes.Legendery:
					return new Color(1, 1, 0);
					
				default:
					return new Color(0,0,0);
			}
		}
	}
}