using Godot;
using System;
using System.Collections.Generic;
using XGeneric.Inventory;

public partial class UIManager : CanvasLayer
{
	public PlayerControler playerControler;
	
	public List<CanvasLayer> allLayers = new();
	[Export] PackedScene UIInventory, UIcustInv;
	
	int currentID = 0;

	public override void _Ready()
	{
		allLayers.Add(GetNode<CanvasLayer>("Gameplay"));

		playerControler = GetTree().GetFirstNodeInGroup("Player").GetChild<PlayerControler>(1);
	}
    public override void _UnhandledInput(InputEvent @event)
    {
        if(Input.IsActionJustPressed("Inventory") && allLayers.Count <= 1)
		{
			OpenPlayerInventory();
		}
    }
    public void ToggleUILayer(int ID)
	{
		allLayers[currentID].Visible = false;
		allLayers[ID].Visible = true;
	}
	
	public void OpenPlayerInventory()
	{
		InventoryUI inventory = UIInventory.Instantiate<InventoryUI>();
		AddChild(inventory);
		allLayers[0].Visible = false;
		allLayers.Add(inventory);
		
		inventory.InitInventory(playerControler.GetInventory(), playerControler);
	}
	
	public bool OpenCustomInventory(Inventory inventory, Node3D initNode = null)
	{
		if(allLayers.Count > 1)
			return false;
		
		CustomInventoryUI UIinventory = UIcustInv.Instantiate<CustomInventoryUI>();
		AddChild(UIinventory);
		allLayers[0].Visible = false;
		allLayers.Add(UIinventory);
		
		UIinventory.InitInventory(inventory, initNode);
		
		return true;
	}
	
}
