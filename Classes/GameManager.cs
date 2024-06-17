using Godot;
using System;

public partial class GameManager : Node
{
	int score = 0;
	int lives = 3;

	CharacterBody2D player;
	ball ball;

	Label gameOver;
	bool isGameOver;

	public override void _Ready()
	{
		player = GetNode<CharacterBody2D>("../Player");
		ball = GetNode<ball>("../Ball");
		gameOver = GetNode<Label>("GameOver");
		gameOver.Hide();
		isGameOver = false;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (Input.IsActionJustPressed("restart_game") && isGameOver)
		{
			GD.Print("Entered Reset Level");
			ResetLevel();
		}
	}

	private void OnGameOverAreaBodyEntered(Node2D body)
	{
		if (body is ball)
		{
			GD.Print("Ball went out of bounds");
			CallDeferred("ReduceLives");
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
		GetTree().Paused = false;
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

	public void ReduceLives()
	{
		--lives;
		var livesLabel = GetNode<Label>("Lives");

		livesLabel.Text = "Lives: 0" + lives;

		if (lives == 0)
		{
			GetTree().Paused = true;
			isGameOver = true;
			gameOver.Show();
			GD.Print(isGameOver);
		}
		else
		{
			ResetPlayer();
		}
	}
}
