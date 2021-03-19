using Godot;
using System;

public class Hero : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	private PackedScene laserScene;
	
	private Timer spawnTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		spawnTimer = GetNode<Timer>("SpawnTimer");
		spawnTimer.Connect("timeout", this, "onSpawnTimeout");
		laserScene = ResourceLoader.Load<PackedScene>("res://Laser.tscn");
		SpawnLasers();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	 private void SpawnLaser()
	{
		var randomX = this.GlobalPosition.x;
		var randomY = this.GlobalPosition.y;
			var newPosition = new Vector2(randomX, randomY).Normalized();
			var laser = laserScene.Instance() as Laser; 
			laser.Position = this.Position;
			

			
			this.GetParent().AddChild(laser);
			laser._Ready();
	}

 public void onSpawnTimeout()
	{
		GD.Print("Spawn a laser!");
		this.SpawnLaser();

		
	}

	

	public void SpawnLasers()
	{
		spawnTimer.WaitTime = 3;
		spawnTimer.OneShot = false;
		spawnTimer.Start();
	}

}
