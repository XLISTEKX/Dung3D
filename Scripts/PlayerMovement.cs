using Godot;
using System;

public partial class PlayerMovement : Node3D
{
	[Export] Camera3D camera;
	[Export] float speed = 1f;
	
	public override void _PhysicsProcess(double delta)
	{
		Vector3 directionZ = GlobalPosition - camera.GlobalPosition;
		directionZ.Y = 0;
		directionZ = directionZ.Normalized();
		Vector3 directionX = camera.GlobalTransform.Basis.X;
		directionX.Y = 0;
		directionX = directionX.Normalized();
		
		Vector3 position = Position;
		
		if(Input.IsActionPressed("Forward"))
		{
			position += directionZ * speed * (float)delta;
		}
		if(Input.IsActionPressed("Back"))
		{
			position -= directionZ * speed * (float)delta;
		}
		if(Input.IsActionPressed("Right"))
		{
			position += directionX * speed * (float)delta;
		}
		if(Input.IsActionPressed("Left"))
		{
			position -= directionX * speed * (float)delta;
		}
		
		Position = position;
	}

}
