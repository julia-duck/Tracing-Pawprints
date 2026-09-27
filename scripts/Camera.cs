using Godot;
using System;

public partial class Camera : Camera2D
{
	[Export]
	public int Speed {get; set;} = 800;

	public bool MovementEnabled;
		
	private Vector2 Velocity;
	private int xLimit;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		xLimit = 2340;
		MovementEnabled = true;
		Velocity = Vector2.Zero;
		var roomName = GetParent().Name;
		switch (roomName)
		{
			case "TitleScreen":
				MovementEnabled = false;
				break;
			case "Town":
				xLimit = 2340;
				break;
			case "Forest":
				xLimit = 4240;
				break;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		// Velocity = direction * Speed;
		bool moveLeft = true;
		if (GetParent().Name == "Town") {
			if (StateData.CurrentMemory == "Neighbor") {
				moveLeft = false;
			}
			else if (StateData.CurrentMemory == "FountainGrass") {
				moveLeft = true;
			}
		}
		else if (GetParent().Name == "Forest") {
			if (StateData.CurrentMemory == "Bowties") {
				moveLeft = false;
			}
			else if (StateData.CurrentMemory == "BushBowtie") {
				moveLeft = GetScreenCenterPosition().X > 2500;
			}
			else {
				moveLeft = true;
			}
		}
		if (Input.IsActionPressed("move_left") && moveLeft)
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
			y: GlobalPosition.Y
		);
	}
	/// <summary>
	/// Image Names: Grass, Bow, Reunion
	/// </summary>
	/// <param name="name"></param>
	/// /// <param name="show"></param>
	public void ToggleImage(String name, bool show)
	{
		GetNode<Sprite2D>(name).Visible = show;
	}
}
