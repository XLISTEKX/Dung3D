using System;
using Godot;
using XGeneric.Utilities;

public partial class PlayerMovement : CharacterBody3D
{
	[Export] Camera3D camera;
	[Export] AnimationTree tree;
	//[Export] Skeleton3D skeleton;
	[Export] float movementSpeed = 4f, runningSpeed = 2.5f;
	[Export] float rotationSpeed = 10f;

	Node3D model;

	PlayerControler playerControler;

	Vector3 forward, left, rotationDirection;

	private bool isIdle, isWalking, isSprinting;
	//private bool isTurningLeft, isTurningRight;
	//For animation mostly

	public override void _Ready()
	{
		base._Ready();
		
		Vector3 tempVector = camera.GlobalTransform.Basis.Z;
		tempVector.Y = 0;
		
		forward = tempVector.Normalized();
		
		left = camera.GlobalTransform.Basis.X.Normalized();
		
		model = GetChild<Node3D>(0);

		playerControler = GetChild<PlayerControler>(1);		
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		Vector3 moveDirection = (left * (Input.GetActionStrength("Right") - Input.GetActionStrength("Left")) + forward * (Input.GetActionStrength("Back") - Input.GetActionStrength("Forward"))).Normalized();

		float sprintStrenght = Input.GetActionStrength("Sprint"), tempMoveSpeed = movementSpeed;

		if(moveDirection != Vector3.Zero)
		{
			rotationDirection = moveDirection + GlobalPosition;

			isIdle = false;
			if(sprintStrenght != 0)
			{
				playerControler.UpdateStamina(-10 * (float) delta);
				isWalking = false;
				isSprinting = true;
				if(playerControler.stamina > 0)
				{
					tempMoveSpeed += sprintStrenght * runningSpeed;
				}
				else
				{
				isWalking = true;
				isSprinting = false;
				}
				//TODO; fix sprint recharge after stamina is depleted
				//TODO; remake walking anim, looks subpar
			}
			else
			{
				playerControler.UpdateStamina(5 * (float) delta);

				isSprinting = false;
				isWalking = true;
			}
		} 
		else
		{
			playerControler.UpdateStamina(10 * (float) delta);

			isSprinting = false;
			isWalking = false;
			isIdle = true;
		}
		
		RotatePlayer(rotationDirection, (float)delta);

		moveDirection *= tempMoveSpeed;
		moveDirection.Y -= 5;
		
		Velocity = moveDirection;

		

		MoveAndSlide();
		HandleAnimations();
	}
	public override void _UnhandledInput(InputEvent @event)
	{
		if(@event is InputEventMouseButton press && press.ButtonIndex == MouseButton.Left && press.Pressed)
		{
			rotationDirection = ScreenToWorldPoint(press.Position);
		}
	}
	//Rotates player to given position in World + deltaTime to handle lerp
	void RotatePlayer(Vector3 vectorToLook, float delta = 1)
	{		
		model.Rotation = new(0,Mathf.LerpAngle(model.Rotation.Y, MathV.GetAngleToVector(vectorToLook, model.GlobalPosition), rotationSpeed * delta), 0);
	}
	//Static func to get Point in space by Screen point
	public Vector3 ScreenToWorldPoint(Vector2 position)
	{
		var SpaceState = GetWorld3D().DirectSpaceState;
		
		var rayOrigin = camera.ProjectRayOrigin(position);
		var rayEnd = rayOrigin + camera.ProjectRayNormal(position) * 1000;
		var querry = PhysicsRayQueryParameters3D.Create(rayOrigin, rayEnd);
		
		var rayArray = SpaceState.IntersectRay(querry);
		
		if(rayArray.Count <= 0)
			return Vector3.Zero;
		
		return (Vector3) rayArray["position"];
	}
	//???
	private void HandleAnimations()
	{
		tree.Set("parameters/conditions/idle", isIdle);
		tree.Set("parameters/conditions/walk", isWalking);
		tree.Set("parameters/conditions/sprint", isSprinting);
		//tree.Set("parameters/conditions/leftTurn", isTurningLeft);
		//tree.Set("parameters/conditions/rightTurn", isTurningRight);
	}
}
