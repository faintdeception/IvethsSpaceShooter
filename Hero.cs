using Godot;

public class Hero : KinematicBody2D
{

	[Export] public int speed = 200;
	private PackedScene laserScene;

	public Vector2 Velocity = new Vector2();

	public override void _Ready()
	{
		//spawnTimer = GetNode<Timer>("SpawnTimer");
		//spawnTimer.Connect("timeout", this, "onSpawnTimeout");
		laserScene = ResourceLoader.Load<PackedScene>("res://Laser.tscn");

	}


	public override void _Input(InputEvent e)
	{

		if (Input.IsActionPressed("shoot"))
		{
			this.SpawnLaser();
		}
	}

	private void SpawnLaser()
	{
		var randomX = this.Position.x;
		var randomY = this.Position.y - 50;
		var newPosition = new Vector2(randomX, randomY);
		var laser = laserScene.Instance() as Laser;
		laser.Position = newPosition;



		this.GetParent().AddChild(laser);
		laser._Ready();
	}

	public void GetInput()
	{
		Velocity = new Vector2();

		if (Input.IsActionPressed("ui_right"))
			Velocity.x += 1;

		if (Input.IsActionPressed("move_left"))
			Velocity.x -= 1;

		if (Input.IsActionPressed("move_down"))
			Velocity.y += 1;

		if (Input.IsActionPressed("move_up"))
			Velocity.y -= 1;

		Velocity = Velocity.Normalized() * speed;
	}

	public override void _PhysicsProcess(float delta)
	{
		GetInput();
		Velocity = MoveAndSlide(Velocity);
	}

}
