using Godot;

public partial class PlayerMovement : CharacterBody3D
{
	[Export] Camera3D camera;
	[Export] AnimationTree tree;
	[Export] float movementSpeed = 4f, runningSpeed = 2.5f;
	[Export] float rotationSpeed = 10f;
	
	Node3D model;

	Vector3 forward, left, rotationDirection;

	private bool isIdle, isRunning, isSprinting;
	//For animation mostly

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
		{
			rotationDirection = moveDirection + GlobalPosition;
		} 
		
		RotatePlayer(rotationDirection, (float)delta);
		
		moveDirection *= movementSpeed + (Input.GetActionStrength("Sprint") * runningSpeed);
		moveDirection.Y -= 5;
		
		Velocity = moveDirection;

		if((left * (Input.GetActionStrength("Right") - Input.GetActionStrength("Left")) + forward * (Input.GetActionStrength("Back") - Input.GetActionStrength("Forward"))) != Vector3.Zero){
			//^ this has been copied from init of Vector3 moveDirection
			isIdle = false;
			if(Input.GetActionStrength("Sprint") != 0){
				isRunning = false;
				isSprinting = true;
			}
			else
			{
			isSprinting = false;
			isRunning = true;
			}
			//oopsies, a nested if statement!
		}
		else
		{
			isSprinting = false;
			isRunning = false;
			isIdle = true;
		}

		MoveAndSlide();
		HandleAnimations();
	}

	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventMouseButton press && press.Pressed)
		{
			rotationDirection = ScreenToWorldPoint(press.Position);
		}
	}
	
	void RotatePlayer(Vector3 vectorToLook, float delta = 1)
	{
		Vector3 look = vectorToLook - model.GlobalPosition;
		look.Y = 0;
		
		float angle = look.SignedAngleTo(new Vector3(0,0,1), new Vector3(0,-1,0));
		model.Rotation = new(0,Mathf.LerpAngle(model.Rotation.Y, angle, rotationSpeed * delta), 0);
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

	private void HandleAnimations()
	{
		tree.Set("parameters/conditions/idle", isIdle);
		tree.Set("parameters/conditions/running", isRunning);
		tree.Set("parameters/conditions/sprinting", isSprinting);
	}
}
