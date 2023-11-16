using Godot;
using XGeneric.Utilities;
namespace XGeneric.Weapons
{
	[GlobalClass]
	public partial class RangeWeapon : Weapon
	{
		[Export] int Damage;
		[Export] PackedScene Projectile;
		
		Vector3 projectileSpawnTransform = new (0, 0.6f, 0);
		
		public override void InitWeapon()
		{
			
		}

		public override void UseWeapon(Node3D user)
		{
			Viewport viewport = user.GetViewport();
			Vector3 direction = XCamera.ScreenToWorldPoint(viewport.GetMousePosition(), viewport.GetCamera3D());
			
			direction -= user.GlobalPosition;
			direction.Y = 0;
			WeaponProjectile projectile = Projectile.Instantiate<WeaponProjectile>();
			user.GetNode("/root").AddChild(projectile);
			projectile.GlobalPosition = user.GlobalPosition + projectileSpawnTransform;
			projectile.InitProjectile(direction, (user as CharacterBody3D).Velocity);
			
			
		}
		
	} 
}