using Godot;
using System;

public partial class ball : RigidBody2D
{
    private bool followParent;

    CharacterBody2D player;

    public override void _Ready()
    {
        followParent = true;
        player = GetNode<CharacterBody2D>("../../Player");
        base._Ready();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Vector2 position = GlobalPosition;

        if (followParent)
        {
            position.X = player.GlobalPosition.X;
            GlobalPosition = position;
        }
    }

}
