using Godot;
using System;

public partial class Bomba : Node3D
{
	public void _on_area_3d_body_entered(Node3D node)
	{
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
