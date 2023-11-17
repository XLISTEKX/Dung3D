using System.Collections.Generic;
using Godot;
using XGeneric.Weapons;

public partial class WeaponSystem : Node3D
{
	List<Weapon> allWeapons = new();
	int currentID = 0;
	[Export] Node3D playerModel;

	public override void _UnhandledInput(InputEvent @event)
	{
		if(allWeapons.Count <= 0)
			return;
		
		if(Input.IsActionJustPressed("Click"))
		{
			allWeapons[currentID].UseWeapon(GetParent() as Node3D);
		}
		else if(@event is InputEventKey key && key.Keycode == Key.Key1 && key.Pressed)
		{
			ChangeWeapon(1);
		}
		else if(@event is InputEventKey key2 && key2.Keycode == Key.Key2 && key2.Pressed)
		{
			ChangeWeapon(-1);
		}
	}
	public void ChangeWeapon(int ID)
	{
		currentID = Mathf.Clamp(currentID + ID, 0, allWeapons.Count - 1);
	}
	public void AddWeapon(Weapon weapon)
	{
		allWeapons.Add(weapon);
	}
	
	public void RemoveWeapon(Weapon weapon)
	{
		allWeapons.Remove(weapon);
		currentID = 0;
	}

}
