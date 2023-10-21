using Godot;
using System;
using System.IO;
using System.Text.Json;
using XGeneric.Inventory;

public partial class PlayerControler : Node3D
{
	[Export] public float staminaMax = 10;
	UIGameplay uIGameplay;
	Inventory playerInventory;

	public float stamina = 10;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		uIGameplay = GetNode<UIGameplay>("../../UIGameplay");
		
		uIGameplay.InitUI(this);
		
		playerInventory = new(10);
		
		InventorySystem.ReadFromJson("Items.json");

	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		
		UpdateUI();
	}
	
	public void UpdateUI()
	{
		uIGameplay.UpdateStamina(stamina);
	}

	public void UpdateStamina(float valueToAdd)
	{
		var newValue = valueToAdd + stamina;

		if(newValue <= staminaMax)
		{
			if(newValue <= 0)
			{
				stamina = 0;
			}
			else
			{
				stamina = newValue;
			}
		}
		else
		{
			stamina = staminaMax;
		}
	}
	
}
