using Godot;

namespace XGeneric.Weapons
{
	public partial class WeaponProjectile : RigidBody3D
	{
		[Export] float speed;
		
		bool canMove = false;
		Vector3 moveDirection, playerVelocty;
		
		public void InitProjectile(Vector3 direction, Vector3 velocity)
		{
			moveDirection = direction;
			playerVelocty = velocity;
			canMove = true;
			
			Timer timer = new()
			{
				WaitTime = 2f
			};
			AddChild(timer);
			timer.Timeout += DestroyParticle;
			timer.Start();
		}

		public override void _PhysicsProcess(double delta)
		{
			if(!canMove)
				return;
			moveDirection = moveDirection.Normalized();
			moveDirection *= (float)delta * speed;
			
			MoveAndCollide(moveDirection);
		}
		
		public void On_body_entered(Node node)
		{
			GD.Print(node.Name);
		}
		
		void DestroyParticle()
		{
			QueueFree();
		}
	}
}