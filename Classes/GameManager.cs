using Godot;
using System;

public partial class GameManager : Node
{
	int score = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnGameOverAreaBodyEntered(Node2D body)
	{
		if (body is ball)
		{
			GD.Print("Ball went out of bounds");
			CallDeferred("ResetLevel");
		}

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
