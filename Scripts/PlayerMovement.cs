using Godot;

public partial class PlayerMovement : CharacterBody3D
{
	[Export] Camera3D camera;
	[Export] float movementSpeed = 1f, runningSpeed = 1f;
	
	Node3D model;

	Vector3 forward, left;

	public override void _Ready()
	{
		base._Ready();
		
		Vector3 tempVector = camera.GlobalTransform.Basis.Z;
		tempVector.Y = 0;
		
		forward = tempVector.Normalized();
		
		left = camera.GlobalTransform.Basis.X.Normalized();
		
		model = GetChild<Node3D>(0);
	}


	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		
		Vector3 moveDirection = left * (Input.GetActionStrength("Right") - Input.GetActionStrength("Left")) + forward * (Input.GetActionStrength("Back") - Input.GetActionStrength("Forward"));

		if(moveDirection != Vector3.Zero)
			RotatePlayer(moveDirection + GlobalPosition);
		
		moveDirection *= movementSpeed + (Input.GetActionStrength("Sprint") * runningSpeed);
		moveDirection.Y -= 5;
		
		Velocity = moveDirection;

		MoveAndSlide();
	}

	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventMouseButton press && press.Pressed)
		{
			RotatePlayer(ScreenToWorldPoint(press.Position));
		}
	}
	
	void RotatePlayer(Vector3 vectorToLook)
	{
		model.LookAt(vectorToLook);
			
		model.Rotation = new(0,model.Rotation.Y + Mathf.Pi, 0);
	}


	public Vector3 ScreenToWorldPoint(Vector2 position)
	{
		var SpaceState = GetWorld3D().DirectSpaceState;
		
		var rayOrigin = camera.ProjectRayOrigin(position);
		var rayEnd = rayOrigin + camera.ProjectRayNormal(position) * 1000;
		var querry = PhysicsRayQueryParameters3D.Create(rayOrigin, rayEnd);
		
		var rayArray = SpaceState.IntersectRay(querry);
		
		return (Vector3) rayArray["position"];
	}
	
}
