using Godot;
using System;

public partial class PlayerMovement : CharacterBody3D
{
	[Export] Camera3D camera;
	[Export] NavigationAgent3D navAgent;
	[Export] float speed = 1f;
	
	Vector3 destination;
	bool movePlayer;
	
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		
		if(navAgent.IsNavigationFinished())
		{
			return;
		}

		MovePlayer();
	}

	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventMouseButton press)
		{
			if(!press.Pressed)
			{
				destination = ScreenPointToRay(press.Position);
				destination.Y = Position.Y;
				
				movePlayer = true;
				
				navAgent.TargetPosition = destination;
			}
		}
	}
	
	void MovePlayer()
	{
		Vector3 currentPosition = GlobalTransform.Origin;
		Vector3 nextPathPosition = navAgent.GetNextPathPosition();
		
		Vector3 newVelocity = (nextPathPosition - currentPosition).Normalized();
		newVelocity *= speed;
		GD.Print(newVelocity);
		Velocity = newVelocity;
		MoveAndSlide();
	}
	
	public Vector3 ScreenPointToRay(Vector2 position)
	{
		var SpaceState = GetWorld3D().DirectSpaceState;
		
		var rayOrigin = camera.ProjectRayOrigin(position);
		var rayEnd = rayOrigin + camera.ProjectRayNormal(position) * 2000;
		var query = PhysicsRayQueryParameters3D.Create(rayOrigin, rayEnd);
		
		var rayArray = SpaceState.IntersectRay(query);
		
		return (Vector3) rayArray["position"];

	}
}
