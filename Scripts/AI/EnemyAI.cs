using Godot;
using XGeneric.Statics;
using XGeneric.Utilities;

public partial class EnemyAI : CharacterBody3D, HealthSystem
{
	[Export] float timeToLook = 1;
	[Export] float attackRange = 1f;
	[Export] float movementSpeed = 10;
	[Export] AnimationTree tree;
	Node3D target;
	Vector3 targetPosition;
	Timer timer;
	int health = 5;
	
	bool dead = false, canMove = true, canAttack = false;
	
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
		if(dead || !canMove)
			return;
		
		Vector3 moveDirection = targetPosition - GlobalPosition;
		
		if(moveDirection.Length() <= attackRange)
		{
			moveDirection = Vector3.Zero;
			tree.Set("parameters/conditions/inAttackRange", true);
			tree.Set("parameters/conditions/isWalking", false);
			canMove = false;
			
			Rotation = new(0,MathV.GetAngleToVector(target.GlobalPosition, GlobalPosition),0);
		}
		else
		{
			moveDirection = moveDirection.Normalized();
			moveDirection *= movementSpeed;
			moveDirection.Y = 0;
			
			//tree.Set("parameters/conditions/isIdle", false);
			tree.Set("parameters/conditions/isWalking", true);
		}
		moveDirection.Y = -9.87f;
		Velocity = moveDirection;
		
		RotateBody((float) delta);
		MoveAndSlide();
		
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
		switch(animName)
		{
			case "death":
				QueueFree();
			break;
			case "attack1":
				canAttack = true;
				if((targetPosition - GlobalPosition).Length() >= attackRange)
				{
					tree.Set("parameters/conditions/inAttackRange", false);
					tree.Set("parameters/conditions/isWalking", true);
					canMove = true;
				}
				else
				{
					Rotation = new(0,MathV.GetAngleToVector(target.GlobalPosition, GlobalPosition),0);
				}
			break;
		}
	}

	public void HitEnemy(Node3D body)
	{
		if(!canAttack)
			return;
		
		if(XStatic.GetScriptInNode(body, out HealthSystem healthSystem))
		{
			healthSystem.TakeDamage(10);
			canAttack = false;
		}
	}
}
