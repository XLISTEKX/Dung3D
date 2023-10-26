using Godot;
using System;

public partial class InventoryUI : CanvasLayer
{
	[Export] PackedScene slot;
	[Export] GridContainer slotTransform;
	
	public void InitInventory(PlayerControler playerControler)
	{
		foreach(InvItem item in playerControler.playerInventory.items)
		{
			
			Node temp = slot.Instantiate();
			slotTransform.AddChild(temp);
		}
	}


	public void CloseInventory()
	{		
		UIManager manager = GetNode<UIManager>("..");
		
		manager.ToggleUILayer(0);
		manager.allLayers.Remove(this);
		QueueFree();
	}
}
