using Godot;
using System;

public partial class ball : CharacterBody2D
{
    private bool followPlayer;

    [Export]
    static float initialBallSpeed = 20;

    //102% faster each time
    [Export]
    float speedMultiplier = 1.02F;

    float ballSpeed = initialBallSpeed;

    CharacterBody2D player;

    public override void _Ready()
    {
        followPlayer = true;
        player = GetNode<CharacterBody2D>("../Player");
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Vector2 position = GlobalPosition;

        if (followPlayer)
        {
            position.X = player.GlobalPosition.X;
            GlobalPosition = position;
        }

        if (Input.IsActionPressed("start_game") && followPlayer)
        {
            StartBall();
        }

        var collision = MoveAndCollide(Velocity * (float)(ballSpeed * delta));
        if (collision != null)
        {
            GD.Print(speedMultiplier);
            Velocity = Velocity.Bounce(collision.GetNormal()) * speedMultiplier;
        }
    }

    public void StartBall()
    {
        followPlayer = false;
        GD.Randomize();

        float[] randomVal1 = { -1, 1 };
        var rand1 = randomVal1[GD.Randi() % 2] * initialBallSpeed;

        Velocity = new Vector2(rand1, -1 * initialBallSpeed);
        GD.Print(Velocity);
    }

}
