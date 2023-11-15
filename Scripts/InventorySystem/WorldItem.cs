using Godot;
using XGeneric.Inventory;
using XGeneric.System;

public partial class WorldItem : Area3D, IInteract
{
	[Export] InvItem item;


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

	public void Interact(Node3D interactObject)
	{
		if(interactObject is IInventory inventory)
		{
			int x = inventory.GetInventory().AddItem(item);
			
			UIManager manager = GetTree().GetFirstNodeInGroup("UIManager") as UIManager;
			
			if(manager.allLayers.Count > 1)
			{
				if(manager.allLayers[1] is InventoryUI inventoryUI)
				{
					inventoryUI.ReloadSlot(x);
				}
			}
			
			QueueFree();
		}
	}

}
