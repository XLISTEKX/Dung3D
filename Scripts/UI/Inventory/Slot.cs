using Godot;
using System;
using XGeneric.Inventory;

public partial class Slot : Panel
{
	[Export] Label Quantity;
	
	[Export] TextureRect Icon, Quality;
	
	bool isDraged = false;
	
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

	public override void _GuiInput(InputEvent @event)
	{
		base._GuiInput(@event);
		
		// if(@event is InputEventMouseButton click && click.Pressed)
		// {
		// 	GD.Print("Clicked: " + Name);
		// 	isDraged = true;
			
			
		// }
		// else if(@event is InputEventMouseButton click2 && !click2.Pressed)
		// {
		// 	GD.Print("UnClicked: " + Name);
		// 	isDraged = false;
		// }
		// else if(@event is InputEventMouseMotion move)
		// {
		// 	if(!isDraged)
		// 		return;
			
		// 	GetChild<Control>(0).GlobalPosition = move.GlobalPosition;
			
		// }
	}
}
