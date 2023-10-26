using Godot;
using System;

public partial class UIGameplay : CanvasLayer
{
	[Export] ProgressBar staminaBar;
	[Export] ProgressBar healthBar;

	
	
	public void UpdateStamina(float value)
	{
		staminaBar.Value = value;
	}
	public void UpdateHealth(float value)
	{
		healthBar.Value = value;
	}
	
	public void InitUI(PlayerControler playerControler)
	{
		staminaBar.MaxValue = playerControler.staminaMax;
		healthBar.MaxValue = playerControler.maxHealth;
	}
}
