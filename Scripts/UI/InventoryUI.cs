using Godot;
using XGeneric.Inventory;

public partial class InventoryUI : CustomInventoryUI
{
	[Export] Label cashLabel;
	
	[Export] Slot playerEQSlot1, playerEQSlot2, playerEQSlot3;
	
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
		
		playerEQSlot1.InitSlot(inventory.eq.EQSlots[0], this, -1);
		playerEQSlot2.InitSlot(inventory.eq.EQSlots[1], this, -2);
		playerEQSlot3.InitSlot(inventory.eq.EQSlots[2], this, -3);
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
			var hoverID = (hoveredSlot.slotID * -1) - 1;
			var initID = (initSlot.slotID * -1) - 1;
			
			InvItem initItem = new(), hoveredItem = new();
			
			switch(initSlot.slotID, hoveredSlot.slotID)
			{
				case (>= 0, >= 0):
				
					inventory.ReplaceItem(initSlot.slotID, hoveredSlot.slotID);
					initItem = inventory.items[initSlot.slotID];
					hoveredItem = inventory.items[hoveredSlot.slotID];
					
				break;
				
				case (< 0, >= 0):
					inventory.UnEquipItem(initID, hoveredSlot.slotID);
					initItem = inventory.eq.EQSlots[initID];
					hoveredItem = inventory.items[hoveredSlot.slotID];
				break;
				
				case (>= 0, < 0):
					inventory.EquipItem(initSlot.slotID, hoverID);
					initItem = inventory.items[initSlot.slotID];
					hoveredItem = inventory.eq.EQSlots[hoverID];
				break;
				
				case (< 0, < 0):
				
					(inventory.eq.EQSlots[initID], inventory.eq.EQSlots[hoverID]) = (inventory.eq.EQSlots[hoverID], inventory.eq.EQSlots[initID]);
					initItem = inventory.eq.EQSlots[initID];
					hoveredItem = inventory.eq.EQSlots[hoverID];
				break;
			}
			
			initSlot.UpdateSlot(initItem);
			hoveredSlot.UpdateSlot(hoveredItem);

			hoveredSlot = null;
		}
		
		initSlot = null; 
		
	}
}
