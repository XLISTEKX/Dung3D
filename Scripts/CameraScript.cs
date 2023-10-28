using Godot;
using System;

public partial class CameraScript : Node3D
{
	[Export] Node3D followTarget;
	[Export] float lerpSpeed = 0.05f;

	public override void _PhysicsProcess(double delta)
	{
		float lerpValue = 1 - (float)Mathf.Pow(0.5f, delta *lerpSpeed);
	
		Position = Position.Lerp(followTarget.GlobalPosition, lerpValue);
	}
}
