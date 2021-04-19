using Godot;
using System;

public class HUD : CanvasLayer
{
    [Export]
    public string Score{get;set;}

    public string Message{get;set;}
    
    private Label ScoreLabel {get;set;}

    private Label MessageLabel{get;set;}

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ScoreLabel = GetNode<Label>("ScoreBox/HBoxContainer/Score");
        MessageLabel = GetNode<Label>("Message");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    ScoreLabel.Text = Score;
    MessageLabel.Text = Message;      
  }
}
