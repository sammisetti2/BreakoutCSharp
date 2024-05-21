using Godot;
using System;

public partial class player : CharacterBody2D
{
    [Export]
    public int speed = 500;

    //Emit this signal when player presses Start (Space Bar)
    [Signal]
    public delegate void GameStartEventHandler();

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector2 direction = Vector2.Zero;

        if (Input.IsActionPressed("move_left"))
        {
            direction = Vector2.Left;
        }

        if (Input.IsActionPressed("move_right"))
        {
            direction = Vector2.Right;
        }

        Velocity = direction * speed;
        MoveAndSlide();

    }
}
