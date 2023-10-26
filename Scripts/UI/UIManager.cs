using Godot;
using System;
using System.Collections.Generic;

public partial class UIManager : CanvasLayer
{
	public PlayerControler playerControler;
	
	public List<CanvasLayer> allLayers = new();
	[Export] PackedScene UIInventory;
	
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
	
	public void OpenInventory()
	{
		InventoryUI inventory = UIInventory.Instantiate<InventoryUI>();
		AddChild(inventory);
		allLayers[0].Visible = false;
		allLayers.Add(inventory);
		
		inventory.InitInventory(playerControler);
	}
}
