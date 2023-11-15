using Godot;
using XGeneric.Inventory;

public partial class CustomInventoryUI : CanvasLayer
{
	[Export] protected PackedScene slot;
	[Export] protected GridContainer slotTransform;
	
	protected Inventory inventory;
	
	protected Slot hoveredSlot, initSlot;
	protected TextureRect dragObject;
	
	protected bool isDraging = false;
	
	public virtual void InitInventory(Inventory inventory, Node3D initNode = null)
	{
		
	}
	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventKey key && key.Keycode == Key.Escape && key.Pressed)
		{
			CloseInventory();
		}
	}

	public virtual void BeginDrag(Slot initSlot)
	{
		
	}
	public virtual void UpdateHoveredDrag(Slot hovered, bool enter)
	{
		
	}
	public virtual void EndDrag()
	{
		
	}
	
	public virtual void ReloadSlot(int slotID)
	{
		slotTransform.GetChild<Slot>(slotID).UpdateSlot(inventory.items[slotID]);
	}
	
	public void ReloadSlots()
	{
		for(int i = 0; i < slotTransform.GetChildCount(); i++)
		{
			slotTransform.GetChild<Slot>(i).UpdateSlot(inventory.items[i]);
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
