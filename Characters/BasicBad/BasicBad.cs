using Godot;
using System;

public class BasicBad : Node2D
{
    [Export]
    public int Hitpoints {get;set;}

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

        var vpHurtbox = this.GetNode<Hurtbox>("Pivot/Body/Hurtbox");
        vpHurtbox.Connect("OnDamageTaken", this, "on_damage_taken");
        // StunTimer.Connect("timeout", this, "onStunTimeOut");
    }

    // public void onStunTimeOut()
    // {
    //     isStunned = false;        
    // }

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
        
        if(Hitpoints < 0)
        {
            PlayAnimation("Death");
            //soundLocation = AUDIO_RESOURCE_LOCATION + DeathRattles[(int)GD.RandRange(0, DeathRattles.Count)];
            var deathTimer  = GetTree().CreateTimer(5);
            deathTimer.Connect("timeout", this, "onDeathTimeout");
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
        if(AnimationStateMachine.IsPlaying())
        {
            AnimationStateMachine.Travel(animation);
        }

    }

}
