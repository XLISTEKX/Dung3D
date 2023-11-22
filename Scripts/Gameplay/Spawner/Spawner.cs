using Godot;
using System;

public partial class Spawner : Node3D
{
	[Export] PackedScene mob;
	[Export] float spawnTime;



	public override void _Ready()
	{
		Timer timer = new()
		{
			WaitTime = spawnTime
			
		};
		timer.Timeout += Spawn;
		AddChild(timer);
		timer.Start();
	}
	
	void Spawn()
	{
		Node3D spawn = mob.Instantiate<Node3D>();
		GetNode("/root/").AddChild(spawn);
		spawn.GlobalPosition = GlobalPosition;
	}


}
