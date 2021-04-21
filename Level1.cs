using Godot;
using System;

public class Level1 : Node2D
{
    private PackedScene basicBadScene, gameOverScene;

    public double Score {get;set;}
    public HUD HUD {get;set;}

    // Called when the node enters the scene tree for the first time.
    public Navigation2D Navigation2D { get; set; }
    public Node2D Hero { get; set; }

    public BasicBad BasicBad { get; set; }

    public Line2D Line2D { get; set; }
    public override void _Ready()
    {
        Navigation2D = GetNode<Navigation2D>("Navigation2D");
        Hero = GetNode<Node2D>("Hero");
        HUD = GetNode<HUD>("HUD");

        basicBadScene = ResourceLoader.Load<PackedScene>("res://Characters/BasicBad/BasicBad.tscn");

        gameOverScene = ResourceLoader.Load<PackedScene>("res://GameOver.tscn");

        Hero.Connect("OnDeath", this, "_on_Hero_Death");
    }

    private void _on_BasicBad_OnDeath()
    {
        Score += 1000;
        HUD.Score = Score.ToString();
    }

    private void _on_Hero_Death()
    {
       HUD.Message = "Game Over!";
       GetTree().Paused = true;
       var gameOverNode = gameOverScene.Instance();
       AddChild(gameOverNode);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        var drones = GetTree().GetNodesInGroup("Drones");

        foreach (BasicBad drone in drones)
        {
            drone.Path = Navigation2D.GetSimplePath(drone.Position, Hero.Position);
        }

        //If there are no drones left, spawn some new ones.
        if (drones.Count == 0)
        {
            SpawnBasicBad(5);
        }        
    }

    private void SpawnBasicBad(int numberOfBaddies)
    {
        Random r = new Random();
        for (int i = 0; i < numberOfBaddies; i++)
        {
            var randomX = this.Position.x - r.Next(0, 1000);
            var randomY = this.Position.y - r.Next(0, 100);
            var newPosition = new Vector2(randomX, randomY);
            var basicBad = basicBadScene.Instance() as BasicBad;
            basicBad.Speed = r.Next(100, 165);
            if (i % 2 == 0)
            {
                basicBad.Position = newPosition;
            }
            else
            {
                basicBad.Position = newPosition * -1;
            }

            basicBad.Connect("OnDeath", this, "_on_BasicBad_OnDeath");

            this.AddChild(basicBad);



            basicBad._Ready();
        }

    }
}
