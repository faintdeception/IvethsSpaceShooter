using Godot;
using System;

public class Laser : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	 [Export] public float MaximumSpeed = 500;
	[Export] public float Acceleration = 2000;
	// Node2D Laser {get;set;}

	Vector2 CurrentPosition {get;set;}
	VisibilityNotifier2D VisibilityNotifier {get;set;}
	// KinematicBody2D LaserBody {get;set;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		//LaserBody = GetNode<KinematicBody2D>("Laser");
		// Laser = GetNode<Node2D>("Sprite");
		VisibilityNotifier = GetNode<VisibilityNotifier2D>("VisibilityNotifier2D");
		GD.Print("I am a laser pew pew!");
		
	
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {
	 //GD.Print("Tick");


 	this.CurrentPosition = this.Position;

	 //GD.Print(this.CurrentPosition);
	 var newYPosition = this.CurrentPosition.y - 1;

	 this.Position = new Vector2(this.Position.x, newYPosition);
	 
 }
public void _on_LaserHitArea_area_entered(Area2D area)
    {
        // var sprite = GetNode<Sprite>("Arrow");
        // sprite.Visible = !sprite.Visible;
        GD.Print("Hit!");
        if (area is IHurtBox)
        {
            (area as IHurtBox).take_damage();
            //var stream = ResourceLoader.Load(SLAP_SOUND_RES) as AudioStream;

            // if (!AudioPlayer.Playing)
            // {
            //     AudioPlayer.Stream = stream;
            //     AudioPlayer.Play();
            // }
			this.QueueFree();
        }

    }
 
}
