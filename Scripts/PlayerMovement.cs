using Godot;

public partial class PlayerMovement : CharacterBody3D
{
	[Export] Camera3D camera;
	[Export] float movementSpeed = 1f, runningSpeed = 1f;

	Vector3 forward, left;

	public override void _Ready()
	{
		base._Ready();
		
		Vector3 tempVector = camera.GlobalTransform.Basis.Z;
		tempVector.Y = 0;
		
		forward = tempVector.Normalized();
		
		left = camera.GlobalTransform.Basis.X.Normalized();
	}


	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		
		Vector3 moveDirection = left * (Input.GetActionStrength("Right") - Input.GetActionStrength("Left")) + forward * (Input.GetActionStrength("Back") - Input.GetActionStrength("Forward"));

		moveDirection *= movementSpeed + (Input.GetActionStrength("Sprint") * runningSpeed);
		moveDirection.Y -= 5;
		
		Velocity = moveDirection;

		MoveAndSlide();
	}
}
