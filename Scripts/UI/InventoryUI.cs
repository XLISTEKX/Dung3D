using Godot;
using XGeneric.Inventory;

public partial class InventoryUI : CustomInventoryUI
{
	[Export] Label cashLabel;
	
	[Export] Panel[] playerEQslots;
	
	public override void InitInventory(Inventory inventory, Node3D initNode = null)
	{
		this.inventory = inventory;
		
		for(int i = 0; i < inventory.items.Length; i++)
		{
			Slot temp = slot.Instantiate<Slot>();
			slotTransform.AddChild(temp);
			temp.Name = "Slot" + i;
			temp.InitSlot(inventory.items[i], this, i);

		}
		cashLabel.Text = inventory.cash.ToString();
		
		
	}
	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventKey key && key.Keycode == Key.Escape && key.Pressed)
		{
			CloseInventory();
		}
		else if(@event is InputEventMouseMotion move)
		{
			if(dragObject != null)
			{
				dragObject.Position = move.Position - dragObject.PivotOffset;
			}
		}
	}
	
	public override void BeginDrag(Slot initSlot)
	{
		isDraging = true;
		this.initSlot = initSlot;
		
		dragObject = new()
		{
			Size = initSlot.Icon.Size,
			Texture = initSlot.Icon.Texture,
			Position = GetViewport().GetMousePosition() - initSlot.Icon.Size / 2,
			MouseFilter = Control.MouseFilterEnum.Ignore,
			PivotOffset = initSlot.Icon.Size / 2
			
		};

		AddChild(dragObject);

		initSlot.Icon.Modulate -= new Color(0,0,0, 0.5f);
	}
	public override void UpdateHoveredDrag(Slot hovered, bool enter)
	{
		if(!isDraging)
			return;
		if(enter)
		{
			if(hovered == initSlot || hovered == hoveredSlot)
				return;
			
			hoveredSlot = hovered;
		}
		else
		{
			hoveredSlot = null;
		}
		
		
	}
	
	public override void EndDrag()
	{
		if(initSlot == null)
		 	return;
		
		isDraging = false;

		dragObject.QueueFree();
		dragObject = null;
		initSlot.Icon.Modulate += new Color(0,0,0, 0.5f);
		if(hoveredSlot != null)
		{
			if(initSlot.slotID < 0)
			{
				
			}
			
			inventory.ReplaceItem(initSlot.slotID, hoveredSlot.slotID);
			
			initSlot.UpdateSlot(inventory.items[initSlot.slotID]);
			hoveredSlot.UpdateSlot(inventory.items[hoveredSlot.slotID]);
			hoveredSlot = null;
		}
		
		initSlot = null; 
		
	}
}
