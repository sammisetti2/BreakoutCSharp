using Godot;
using System;

public partial class GameManager : Node
{
	int score = 0;

	CharacterBody2D player;
	ball ball;

	public override void _Ready()
	{
		player = GetNode<CharacterBody2D>("../Player");
		ball = GetNode<ball>("../Ball");
	}

	private void OnGameOverAreaBodyEntered(Node2D body)
	{
		if (body is ball)
		{
			GD.Print("Ball went out of bounds");
			CallDeferred("ResetPlayer");
		}

	}

	private void ResetPlayer()
	{
		ball.followPlayer = true;
		ball.GlobalPosition = new Vector2(0f, player.GlobalPosition.Y - 40);

		ball.Velocity = Vector2.Zero;
		player.Velocity = Vector2.Zero;
		player.GlobalPosition = new Vector2(0f, player.GlobalPosition.Y);
	}

	private void ResetLevel()
	{
		GetTree().ReloadCurrentScene();
	}

	public void AddPoint()
	{
		score += 1;
		var scoreLabel = GetNode<Label>("Score");
		if (score < 10)
		{
			scoreLabel.Text = "Score: 0" + score;
		}
		else
		{
			scoreLabel.Text = "Score: " + score;
		}
	}
}
