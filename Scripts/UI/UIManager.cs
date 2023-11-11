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
		
		inventory.InitInventory(playerControler.GetInventory());
	}
	
	public void OpenCustomInventory(Inventory inventory)
	{
		CustomInventoryUI UIinventory = UIcustInv.Instantiate<CustomInventoryUI>();
		AddChild(UIinventory);
		allLayers[0].Visible = false;
		allLayers.Add(UIinventory);
		
		UIinventory.InitInventory(inventory);
	}
	
	
}
