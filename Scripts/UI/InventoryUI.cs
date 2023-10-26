using Godot;
using XGeneric.Inventory;

public partial class InventoryUI : CanvasLayer
{
	[Export] PackedScene slot;
	[Export] GridContainer slotTransform;
	
	Inventory inventory;
	
	public void InitInventory(PlayerControler playerControler)
	{
		inventory = playerControler.playerInventory;
		
		foreach(InvItem item in inventory.items)
		{
			Slot temp = slot.Instantiate<Slot>();
			slotTransform.AddChild(temp);
			
			temp.InitSlot(item);
		}
	}

	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventKey key && key.Keycode == Key.Escape && key.Pressed)
		{
			CloseInventory();
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
