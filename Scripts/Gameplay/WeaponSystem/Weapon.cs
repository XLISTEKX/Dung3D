using Godot;
using XGeneric.Inventory;

namespace XGeneric.Weapons
{
	public enum WeaponType
	{
		Range,
		Melee,
	}
	
	
	[GlobalClass]
	public partial class Weapon : InvItem
	{
		static Vector3 s = new(0,0,0);
		[Export] WeaponType weaponType;
		public virtual void UseWeapon(Node3D user)
		{
			
		}
		
		public virtual void InitWeapon()
		{
			
		}
		
	}
}