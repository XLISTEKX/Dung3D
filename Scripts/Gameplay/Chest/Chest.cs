using Godot;
using System;
using XGeneric.Inventory;
using XGeneric.System;

public partial class Chest : Area3D, IInventory, IInteract
{
	Inventory inventory;

	public override void _Ready()
	{
		inventory = new(10);
		
		inventory.AddItem(InventorySystem.GetItemByID(0));
		inventory.AddItem(InventorySystem.GetItemByID(2));
	}

	public void OnBodyEnter(Node3D node)
	{
		foreach(Node child in node.GetChildren())
		{
			if(child is IInteractMain interact)
			{
				interact.AddInteract(this);
				break;
			}
		}
	}
	
	public void OnBodyExit(Node3D node)
	{
		foreach(Node child in node.GetChildren())
		{
			if(child is IInteractMain interact)
			{
				interact.RemoveInteract(this);
				break;
			}
		}
	}


	public Inventory GetInventory()
	{
		return inventory;
	}

	public void Interact(Node3D interactObject)
	{
		GetNode<UIManager>("/root/TestSite/UI").OpenCustomInventory(inventory, this);
	}
}
