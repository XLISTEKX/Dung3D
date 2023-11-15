using Godot;
using XGeneric.Inventory;

public partial class ChestInventoryUI : CustomInventoryUI
{
	[Export] float MaxChestRange = 5f;
	
	[Export] GridContainer playerInventoryTransform;
	Inventory[] inventories;
	Node3D player, chest;
	
	public override void InitInventory(Inventory inventory, Node3D initNode = null)
	{
		player = GetTree().GetFirstNodeInGroup("Player") as Node3D;
		chest = initNode;

		inventories = new Inventory[]
		{
			inventory,
			(player.FindChild("PlayerControler") as PlayerControler).GetInventory()	
		};
		
		for(int i = 0; i < inventories[0].items.Length; i++)
		{
			Slot temp = slot.Instantiate<Slot>();
			slotTransform.AddChild(temp);
			temp.Name = "Slot: " + i;
			temp.InitSlot(inventories[0].items[i], this, i, 0);
		}
		
		for(int i = 0; i < inventories[1].items.Length; i++)
		{
			Slot temp = slot.Instantiate<Slot>();
			playerInventoryTransform.AddChild(temp);
			temp.Name = "Slot: " + i;
			temp.InitSlot(inventories[1].items[i], this, i, 1);
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
	public override void _Process(double delta)
	{
		var distance = (player.GlobalPosition - chest.GlobalPosition).Length();
		
		if(distance >= MaxChestRange)
		{
			CloseInventory();
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
			if(initSlot.inventoryID != hoveredSlot.inventoryID)
			{
				if(initSlot.inventoryID == 0)
				{
					inventories[0].ExchangeItem(initSlot.slotID, hoveredSlot.slotID, inventories[1]);
					initSlot.UpdateSlot(inventories[0].items[initSlot.slotID]);
					hoveredSlot.UpdateSlot(inventories[1].items[hoveredSlot.slotID]);
				}
				else
				{
					inventories[1].ExchangeItem(initSlot.slotID, hoveredSlot.slotID, inventories[0]);
					initSlot.UpdateSlot(inventories[1].items[initSlot.slotID]);
					hoveredSlot.UpdateSlot(inventories[0].items[hoveredSlot.slotID]);
				}

				
				hoveredSlot = null;
			}
			else
			{
				inventories[initSlot.inventoryID].ReplaceItem(initSlot.slotID, hoveredSlot.slotID);
			
				initSlot.UpdateSlot(inventories[initSlot.inventoryID].items[initSlot.slotID]);
				hoveredSlot.UpdateSlot(inventories[initSlot.inventoryID].items[hoveredSlot.slotID]);
				hoveredSlot = null;
			}
		}
		
		initSlot = null; 
		
	}


}