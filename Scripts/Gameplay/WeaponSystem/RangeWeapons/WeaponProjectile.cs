using System.Collections.Generic;
using Godot;

namespace XGeneric.Weapons
{
	public partial class WeaponProjectile : RigidBody3D
	{
		[Export] float speed;
		[Export] float LifeTime;
		
		int Damage;
		
		bool canMove = false;
		Vector3 moveDirection;
		
		public void InitProjectile(Vector3 direction, int damage)
		{
			Damage = damage;
			moveDirection = direction;
			canMove = true;
			
			Timer timer = new()
			{
				WaitTime = LifeTime
			};
			AddChild(timer);
			timer.Timeout += DestroyParticle;
			timer.Start();
			
			LinearVelocity = direction.Normalized() * speed;
		}
		public void On_body_entered(Node node)
		{
			List<Node> nodes = new()
			{
				node	
			};
			nodes.AddRange(node.GetChildren());
			
			foreach(Node child in nodes)
			{
				if(child is HealthSystem health)
				{
					health.TakeDamage(Damage);
					break;
				}
			}
			QueueFree();
		}
		
		void DestroyParticle()
		{
			QueueFree();
		}
	}
}