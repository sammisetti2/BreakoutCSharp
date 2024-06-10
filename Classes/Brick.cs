using Godot;
using System;

public partial class Brick : StaticBody2D
{
	int hp = 2;
	AnimatedSprite2D animatedSprite;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public int TakeAHit()
	{
		--hp;
		if (hp == 1)
		{
			animatedSprite.Play("broken");
		}
		else
		{
			QueueFree();
		}
		return hp;
	}
}
