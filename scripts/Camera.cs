using Godot;
using System;

public partial class Camera : Camera2D
{
	[Export]
	public int Speed {get; set;} = 800;

	private Vector2 Velocity;
	private int xLimit;
	private int yLimit;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//=
		yLimit = 540;
		xLimit = 2340;
		if (GetParent().Name == "Town")
		{
			xLimit = 2340;
		}
		Velocity = Vector2.Zero;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		// Velocity = direction * Speed;
		if (Input.IsActionPressed("move_left"))
		{
			Velocity.X = -Speed;
		}
		else if (Input.IsActionPressed("move_right"))
		{
			Velocity.X = Speed;
		}
		else
		{
			Velocity = Vector2.Zero;
		}
		GlobalPosition += Velocity * (float)delta;
		GlobalPosition = new Vector2(
			x: Mathf.Clamp(GlobalPosition.X, 960, xLimit),
			y: Mathf.Clamp(GlobalPosition.Y, 0, yLimit)
		);
	}
}
