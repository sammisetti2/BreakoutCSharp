using Godot;
using System;

public partial class ball : CharacterBody2D
{
    [Signal]
    public delegate void CollidedWithBrickEventHandler(Brick brick);

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
        GodotObject? collider = null;

        if (collision != null)
        {
            collider = collision.GetCollider();
            if (collider is Brick)
            {
                var finalhp = Collided((Brick)collider);
                if (finalhp == 0)
                {
                    Velocity = Velocity.Bounce(collision.GetNormal()) * speedMultiplier;
                }
                else
                {
                    Velocity = Velocity.Bounce(collision.GetNormal());
                }
            }
            else
            {
                Velocity = Velocity.Bounce(collision.GetNormal());
            }
            //Velocity = Velocity.Bounce(collision.GetNormal());
            GD.Print(Velocity);
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

    public int Collided(Brick brick)
    {
        var finalhp = brick.TakeAHit();
        return finalhp;
    }

}
