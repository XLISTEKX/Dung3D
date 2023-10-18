using Godot;
using System;

public partial class CameraRotator : Node3D
{
	[Export] float rotationSpeed;
	Vector2 currentPosition;
	bool pressed;

	public override void _PhysicsProcess(double delta)
	{
		if(pressed)
		{
			Vector2 direction = GetViewport().GetMousePosition() - currentPosition;
			RotateY(direction.X * (float)delta * rotationSpeed);
			
			currentPosition = GetViewport().GetMousePosition();
		}
	}

	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventMouseButton press)
		{
			currentPosition = GetViewport().GetMousePosition();
			pressed = press.Pressed;
		}
	}
}
