using Godot;

public class Hero : KinematicBody2D
{
    [Signal]
    public delegate void OnDeath();

    [Export]
    public int Speed = 200;

    [Export]
    public int HitPoints = 10;
    private PackedScene laserScene;

	public Vector2 Velocity = new Vector2();

    private AnimationNodeStateMachinePlayback AnimationStateMachine { get; set; }

    public override void _Ready()
    {
        //spawnTimer = GetNode<Timer>("SpawnTimer");
        //spawnTimer.Connect("timeout", this, "onSpawnTimeout");
        laserScene = ResourceLoader.Load<PackedScene>("res://Laser.tscn");

        AnimationStateMachine = this.GetNode("AnimationTree").Get("parameters/playback") as AnimationNodeStateMachinePlayback;
        AnimationStateMachine.Start("Idle");

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
        var randomY = this.Position.y - 100;
        var newPosition = new Vector2(randomX, randomY);
        var laser = laserScene.Instance() as Laser;
        //laser.Scale = new Vector2(10,10);
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

        Velocity = Velocity.Normalized() * Speed;
    }

	public override void _PhysicsProcess(float delta)
	{
		GetInput();
		Velocity = MoveAndSlide(Velocity);
	}

    public void _on_Hurtbox_area_entered(Area2D area)
    {
        // var sprite = GetNode<Sprite>("Arrow");
        // sprite.Visible = !sprite.Visible;

        HitPoints--;
        AnimationStateMachine.Travel("Hurt");
        if(HitPoints <= 0)
        {
            EmitSignal("OnDeath");
        }

        GD.Print("Hero taking damage from " + area.Name + "!");
        if (area is IHurtBox)
        {
            (area as IHurtBox).die();
            //var stream = ResourceLoader.Load(SLAP_SOUND_RES) as AudioStream;

            // if (!AudioPlayer.Playing)
            // {
            //     AudioPlayer.Stream = stream;
            //     AudioPlayer.Play();
            // }
        }
        //this.QueueFree();

    }



}
