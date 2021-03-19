using Godot;


public class Hurtbox : Area2D, IHurtBox
{
     // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Signal]
    public delegate void OnDamageTaken();


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void take_damage()
    {
        // this.Connect("damage_taken", this.Owner, "on_damage_taken");
        this.EmitSignal("OnDamageTaken");
        // animSM.Travel("Idle");
    }
}
