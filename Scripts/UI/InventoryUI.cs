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
		
		for(int i = 0; i < inventory.items.Length; i++)
		{
			Slot temp = slot.Instantiate<Slot>();
			slotTransform.AddChild(temp);
			temp.Name = "Slot" + i;
			temp.InitSlot(inventory.items[i]);
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
