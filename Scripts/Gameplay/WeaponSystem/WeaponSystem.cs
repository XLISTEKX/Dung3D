using Godot;
using System;
using XGeneric.Weapons;

public partial class WeaponSystem : Node3D
{
	[Export] Weapon currentWeapon;
	
	[Export] Node3D playerModel;


	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("Click"))
		{
			currentWeapon.UseWeapon(GetParent() as Node3D);
		}
	}

}
