using Godot;
using XGeneric.Utilities;

public partial class EnemyAI : RigidBody3D, HealthSystem
{
	[Export] float timeToLook = 1;
	[Export] float movementSpeed = 10;
	Node3D target;
	Vector3 targetPosition;
	Timer timer;
	int health = 5;
	public int Health 
	{
		get
		{
			return health;
		}
	}

	public override void _Ready()
	{
		target = GetTree().GetFirstNodeInGroup("Player") as Node3D;
		
		SetUpTimer();
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 moveDirection = (targetPosition - GlobalPosition).Normalized();
		moveDirection *= movementSpeed;
		moveDirection.Y = 0;
		
		MoveAndCollide(moveDirection * (float)delta);
		RotateBody((float) delta);
	}

	public void SetUpTimer()
	{
		timer = new()
		{
			WaitTime = timeToLook
		};

		AddChild(timer);

		timer.Timeout += GetNewLocation;
		timer.Start(timeToLook);
		
		
	}
	
	void RotateBody(float delta)
	{
		Rotation = new(0,Mathf.LerpAngle(Rotation.Y, MathV.GetAngleToVector(targetPosition, GlobalPosition), 2f * delta), 0);
	}	
	void GetNewLocation()
	{
		targetPosition = target.GlobalPosition;
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
		
		if(health <= 0)
		{
			Dead();
		}
	}
	
	public void Dead()
	{
		QueueFree();
	}

}
