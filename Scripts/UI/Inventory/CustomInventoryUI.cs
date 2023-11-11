using Godot;
using XGeneric.Inventory;

public partial class CustomInventoryUI : InventoryUI
{
	public override void InitInventory(Inventory inventory)
	{
		this.inventory = inventory;
		
		for(int i = 0; i < inventory.items.Length; i++)
		{
			Slot temp = slot.Instantiate<Slot>();
			slotTransform.AddChild(temp);
			temp.Name = "Slot" + i;
			temp.InitSlot(inventory.items[i], this, i);
		}
	}
	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventKey key && key.Keycode == Key.Escape && key.Pressed)
		{
			CloseInventory();
		}
	}
	


}
