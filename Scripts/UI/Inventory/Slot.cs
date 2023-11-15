using Godot;
using System;
using XGeneric.Inventory;

public partial class Slot : Panel
{
	[Export] Label Quantity;
	
	[Export] public TextureRect Icon, Quality;
	
	CustomInventoryUI inventoryUI;
	public int slotID, inventoryID;
	
	
	bool isDraged = false;
	
	public void InitSlot(InvItem item, CustomInventoryUI inventoryUI, int id, int inventoryID = 0)
	{
		this.inventoryUI = inventoryUI;
		slotID = id;
		this.inventoryID = inventoryID;
		if(item == null)
		{			
			Quantity.Visible = false;
			Icon.Visible = false;
			Quality.Modulate = new(0.5f,0.5f,0.5f);
			return;
		}
		
		Quantity.Visible = false;
		
		Icon.Texture = item.ItemIcon;
		Quality.Modulate = InventorySystem.GetColorByRarity(item.rarity);
	}

	public void UpdateSlot(InvItem item)
	{
		if(item == null)
		{
			Quantity.Visible = false;
			Icon.Visible = false;
			Quality.Modulate = new(0.5f,0.5f,0.5f);
			return;
		}
		
		Icon.Visible = true;
		Quality.Visible = true;
		Icon.Texture = item.ItemIcon;
		Quality.Modulate = InventorySystem.GetColorByRarity(item.rarity);
	}
	

	public override void _GuiInput(InputEvent @event)
	{
		base._GuiInput(@event);
		
		if(!Icon.Visible) 
			return;
		
		if(@event is InputEventMouseButton click && click.Pressed && click.ButtonIndex == MouseButton.Left)
		{
			isDraged = true;
		}
		else if(@event is InputEventMouseButton click2 && !click2.Pressed && click2.ButtonIndex == MouseButton.Left)
		{
			inventoryUI.EndDrag();
			isDraged = false;
		}
		else if(@event is InputEventMouseMotion move) 
		{
			if(!isDraged)
				return;
			
			inventoryUI.BeginDrag(this);
			isDraged = false;
		}
	}
	
	public void OnMouseEnter()
	{
		inventoryUI.UpdateHoveredDrag(this, true);
	}
	
	public void OnMouseExit()
	{
		inventoryUI.UpdateHoveredDrag(this, false);
	}
}
