using Godot;
using System;

public partial class Bomba : Node3D
{
	public void _on_area_3d_body_entered(Node3D node)
	{
		if(node is HealthSystem health)
		{
			health.TakeDamage(10);
			QueueFree();
			return;
		}
		
		
		foreach(var t in node.GetChildren())
		{
			if(t is HealthSystem output)
			{
				output.TakeDamage(10);
				QueueFree();
			}
		}
	}
}
