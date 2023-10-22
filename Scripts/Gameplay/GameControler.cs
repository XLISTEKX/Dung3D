using Godot;
using System;
using XGeneric.Inventory;

public partial class GameControler : Node3D
{
	[Export] string itemsJsonPath;
	
	Item[] allItems;

	public override void _Ready()
	{
		allItems = InventorySystem.ReadAllFromJson(itemsJsonPath);
	}
	
	public Item GetItemByID(int ID)
	{
		return allItems[ID];
	}
}
