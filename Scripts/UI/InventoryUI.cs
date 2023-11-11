using Godot;
using XGeneric.Inventory;

public partial class InventoryUI : CanvasLayer
{
	[Export] protected PackedScene slot;
	[Export] protected GridContainer slotTransform;
	[Export] private Label cashLabel;
	
	protected Inventory inventory;
	
	protected Slot hoveredSlot, initSlot;
	protected TextureRect dragObject;
	
	protected bool isDraging = false;
	
	public virtual void InitInventory(Inventory inventory)
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
	
	public virtual void BeginDrag(Slot initSlot)
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
	public virtual void UpdateHoveredDrag(Slot hovered, bool enter)
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
	
	public virtual void EndDrag()
	{
		if(initSlot == null)
		 	return;
		
		isDraging = false;

		dragObject.QueueFree();
		dragObject = null;
		initSlot.Icon.Modulate += new Color(0,0,0, 0.5f);
		if(hoveredSlot != null)
		{
			inventory.ReplaceItem(initSlot.slotID, hoveredSlot.slotID);
			
			initSlot.UpdateSlot(inventory.items[initSlot.slotID]);
			hoveredSlot.UpdateSlot(inventory.items[hoveredSlot.slotID]);
			hoveredSlot = null;
		}
		
		initSlot = null; 
		
	}

	public virtual void CloseInventory()
	{		
		UIManager manager = GetNode<UIManager>("..");
		
		manager.ToggleUILayer(0);
		manager.allLayers.Remove(this);
		QueueFree();
	}
}
