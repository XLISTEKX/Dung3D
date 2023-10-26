using Godot;
using System;
using XGeneric.Inventory;

public partial class Slot : Panel
{
	[Export] Label Quantity;
	
	[Export] TextureRect Icon, Quality;
	
	
	public void InitSlot(InvItem item)
	{
		if(item == null)
		{
			Quantity.Visible = false;
			Icon.Visible = false;
			Quality.Visible = false;
			return;
		}
		
		Quantity.Visible = false;
		
		Icon.Texture = item.ItemIcon;
		Quality.Modulate = InventorySystem.GetColorByRarity(item.rarity);
	}
}
