using Godot;
using System;

public partial class PlayerControler : Node3D
{
	UIGameplay uIGameplay;

	public float stamina = 100;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		uIGameplay = GetNode<UIGameplay>("../../UIGameplay");
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

		if(newValue <= 100)
		{
			if(newValue <= 0)
			{
				stamina = 0;
			}
			else
			{
				stamina = newValue;
			}
			
			UpdateUI();
		}
	}
	
}
