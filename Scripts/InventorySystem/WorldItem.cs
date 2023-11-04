using Godot;
using System;
using XGeneric.Inventory;

public partial class WorldItem : Area3D
{
	[Export] int itemID;
	public InvItem item;


	public override void _Ready()
	{
		item = InventorySystem.GetItemByID(itemID);
	}

	public void OnBodyEnter(Node3D node)
	{
		foreach(Node child in node.GetChildren())
		{
			if(child is IInventory inventory)
			{
				inventory.AddToPickUp(this);
				break;
			}
		}
	}
	
	public void OnBodyExit(Node3D node)
	{
		foreach(Node child in node.GetChildren())
		{
			if(child is IInventory inventory)
			{
				inventory.RemoveToPickUp(this);
				break;
			}
		}
	}
}
