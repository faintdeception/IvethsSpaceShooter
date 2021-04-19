using Godot;
using System.Linq;


public class BasicBad : KinematicBody2D
{

    [Signal]
    public delegate void OnDeath();


    [Export]
    public int Speed { get; set; }

    [Export]
    public int Hitpoints { get; set; }

    private Hurtbox Hurtbox {get;set;}

    public Vector2[] Path { get; set; }

    private AnimationNodeStateMachinePlayback AnimationStateMachine { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Hitpoints = 3;
        // AudioPlayer = this.GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        // AudioPlayer.VolumeDb = 1;
        // AudioPlayer.PitchScale = 1;
        AnimationStateMachine = this.GetNode("AnimationTree").Get("parameters/playback") as AnimationNodeStateMachinePlayback;
        AnimationStateMachine.Start("Idle");

        Hurtbox = this.GetNode<Hurtbox>("Pivot/Body/Hurtbox");
        Hurtbox.Connect("OnDamageTaken", this, "on_damage_taken");
        Hurtbox.Connect("OnDeath", this, "on_death");

        AddToGroup("Drones");
        // StunTimer.Connect("timeout", this, "onStunTimeOut");
    }

    // public void onStunTimeOut()
    // {
    //     isStunned = false;        
    // }

    public void Die()
    {
        
        this.CollisionLayer = 10;
        this.CollisionMask = 10;

        this.Hurtbox.CollisionLayer = 10;
        this.Hurtbox.CollisionMask = 10;
        PlayAnimation("Death");
        //soundLocation = AUDIO_RESOURCE_LOCATION + DeathRattles[(int)GD.RandRange(0, DeathRattles.Count)];
        if (IsInGroup("Drones"))
        {
            RemoveFromGroup("Drones");            
            var deathTimer = GetTree().CreateTimer(5);
            deathTimer.Connect("timeout", this, "onDeathTimeout");
            this.EmitSignal("OnDeath");
        }
    }


    public void on_death()
    {
        Hitpoints = -1;
        Die();
    }

    public void on_damage_taken()
    {

        GD.Print("Basic Enemy Damage Taken!");
        GD.Print("I'm a basic enemy and I'm hit!!!");


        // var stunTimer = GetTree().CreateTimer(0.5F, false);

        //var soundLocation = AUDIO_RESOURCE_LOCATION + HurtSounds[(int)GD.RandRange(0, HurtSounds.Count)];

        // if(Hitpoints > 10)
        // {
        //     PlayAnimation("Knockback");

        //     soundLocation = AUDIO_RESOURCE_LOCATION + HurtSounds[(int)GD.RandRange(0, HurtSounds.Count)];
        // }
        // else if(Hitpoints > 5)
        // {
        //     PlayAnimation("Knockback");

        //     soundLocation = AUDIO_RESOURCE_LOCATION + HurtSounds[(int)GD.RandRange(0, HurtSounds.Count)];
        // }
        // else if(Hitpoints >= 1)
        // {
        //     soundLocation = AUDIO_RESOURCE_LOCATION + BigHurtSounds[(int)GD.RandRange(0, BigHurtSounds.Count)];
        //     PlayAnimation("Knockdown");            
        // }
        // else

        Hitpoints--;
        GD.Print(Hitpoints);

        if (Hitpoints < 0)
        {
            Die();
        }


        // var stream = ResourceLoader.Load(soundLocation) as AudioStream;

        // if (!AudioPlayer.Playing)
        // {
        //     AudioPlayer.Play();
        //     AudioPlayer.Stream = stream;
        // }


        //StunTimer.Start(StunDuration);
        //isStunned = true;

        // if(this.Hitpoints > 0)        
        //     PlayAnimation("Hurt");
        // else if(this.Hitpoints <= 0)
        //     PlayAnimation("Die");

        // this.Hitpoints--;
    }

    private void PlayAnimation(string animation)
    {
        if (AnimationStateMachine.IsPlaying())
        {
            AnimationStateMachine.Travel(animation);
        }

    }

    private void onDeathTimeout()
    {
        this.QueueFree();
    }

    public override void _Process(float delta)
    {
        if (IsInGroup("Drones"))
        {
            //     # Calculate the movement distance for this frame
            var distance_to_walk = Speed * delta;

            // # Move the player along the path until he has run out of movement or the path ends.
            while (distance_to_walk > 0 && (Path != null && Path.Length > 0))
            {
                var distance_to_next_point = Position.DistanceTo(Path[0]);
                if (distance_to_walk <= distance_to_next_point)
                //# The player does not have enough movement left to get to the next point.
                {
                    Position += Position.DirectionTo(Path[0]) * distance_to_walk;
                }
                else
                {
                    //GD.Print("Moving");
                    // 		# The player get to the next point
                    Position = Path[0];
                    Path = Path.Skip(1).ToArray();
                    // 	# Update the distance to walk
                }
                distance_to_walk -= distance_to_next_point;
            }
        }
    }

}
