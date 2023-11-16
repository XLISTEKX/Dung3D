using Godot;
using XGeneric.Utilities;

public partial class EnemyAI : CharacterBody3D, HealthSystem
{
	[Export] float timeToLook = 1;
	[Export] float movementSpeed = 10;
	[Export] AnimationTree tree;
	Node3D target;
	Vector3 targetPosition;
	Timer timer;
	int health = 5;
	
	bool dead = false;
	
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
		if(dead)
			return;
		
		Vector3 moveDirection = targetPosition - GlobalPosition;
		
		if(moveDirection.Length() <= 0.75f)
		{
			moveDirection = Vector3.Zero;
			tree.Set("parameters/conditions/isIdle", true);
			tree.Set("parameters/conditions/isWalking", false);
		}
		else
		{
			moveDirection = moveDirection.Normalized();
			moveDirection *= movementSpeed;
			moveDirection.Y = 0;
			
			tree.Set("parameters/conditions/isIdle", false);
			tree.Set("parameters/conditions/isWalking", true);
		}
		
		//MoveAndCollide(moveDirection * (float)delta);
		
		Velocity = moveDirection;
		MoveAndSlide();
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
		dead = true;
		tree.Set("parameters/conditions/isDead", true);
	}
	
	public void AnimFinish(string animName)
	{
		if(animName == "death")
			QueueFree();
	}

}
