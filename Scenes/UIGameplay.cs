using Godot;
using System;

public partial class UIGameplay : CanvasLayer
{
	[Export] ProgressBar staminaBar;

	public void UpdateStamina(float value)
	{
		staminaBar.Value = value;
	}
}
