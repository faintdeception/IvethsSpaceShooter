using Godot;
using System;

public class GameOver : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Button RetryButton { get; set; }
    private Button QuitButton { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        QuitButton = GetNode<Button>("VBoxContainer/QuitButton");
        RetryButton = GetNode<Button>("VBoxContainer/RetryButton");
        RetryButton.GrabFocus();

        RetryButton.Connect("pressed", this, "_on_Retry_Pressed");

        QuitButton.Connect("pressed", this, "_on_Quit_Pressed");
    }


    private void _on_Quit_Pressed()
    {
        GetParent().GetTree().Quit();
    }

    private void _on_Retry_Pressed()
    {
        GetTree().Paused = false;
        GetParent().GetTree().ReloadCurrentScene();
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
