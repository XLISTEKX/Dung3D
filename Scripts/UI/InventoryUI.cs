using System.Collections.Generic;
using Godot;
using XGeneric.Inventory;

public partial class InventoryUI : CanvasLayer
{
	[Export] PackedScene slot;
	[Export] GridContainer slotTransform;
	
	Inventory inventory;
	
	Slot hoveredSlot, initSlot;
	TextureRect dragObject;
	
	bool isDraging = false;
	
	public void InitInventory(PlayerControler playerControler)
	{
		inventory = playerControler.playerInventory;
		
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
		else if(@event is InputEventMouseMotion move)
		{
			if(dragObject != null)
			{
				dragObject.Position = move.Position - dragObject.PivotOffset;
			}
		}
	}
	
	public void BeginDrag(Slot initSlot)
	{
		isDraging = true;
		this.initSlot = initSlot;
		
		dragObject = new()
		{
			Size = initSlot.Icon.Size,
			Texture = initSlot.Icon.Texture,
			Position = initSlot.Icon.Position,
			MouseFilter = Control.MouseFilterEnum.Ignore,
			PivotOffset = initSlot.Icon.Size / 2
			
		};

		AddChild(dragObject);

	}
	public void UpdateHoveredDrag(Slot hovered, bool enter)
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
	
	public void UpdateSlot(int ID)
	{
		Slot slot = slotTransform.GetChild<Slot>(ID);
		slot.InitSlot(inventory.items[ID], this, ID);
	}
	
	public void EndDrag()
	{
		isDraging = false;

		dragObject.QueueFree();
		dragObject = null;
		
		if(hoveredSlot != null)
		{
			inventory.ReplaceItem(initSlot.slotID, hoveredSlot.slotID);
			
			initSlot.UpdateSlot(inventory.items[initSlot.slotID]);
			hoveredSlot.UpdateSlot(inventory.items[hoveredSlot.slotID]);
			hoveredSlot = null;
		}
		
		initSlot = null; 
		
	}

	public void CloseInventory()
	{		
		UIManager manager = GetNode<UIManager>("..");
		
		manager.ToggleUILayer(0);
		manager.allLayers.Remove(this);
		QueueFree();
	}
}
