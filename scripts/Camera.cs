using Godot;
using System;

public partial class Camera : Camera2D
{
	[Export]
	public int Speed {get; set;} = 800;

	public bool MovementEnabled;
	public int xRightLimit;
	public int xLeftLimit;
		
	private Vector2 Velocity;
	private Node2D LeftArrow;
	private Node2D RightArrow;
	private static bool AllowTransition;
	private String RoomName;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (AllowTransition)
		{
			Position = new Vector2(40, 540);
		}
		xRightLimit = 2340;
		MovementEnabled = true;
		Velocity = Vector2.Zero;
		LeftArrow = GetNode<Node2D>("LeftArrow");
		RightArrow = GetNode<Node2D>("RightArrow");
		AllowTransition = false;
		RoomName = GetParent().Name;
		switch (RoomName)
		{
			case "TitleScreen":
				MovementEnabled = false;
				break;
			case "Town":
				xRightLimit = 2340;
				break;
			case "Forest":
				xRightLimit = 4240;
				break;
		}
		SetLeftLimit();
		if (!MovementEnabled)
		{
			LeftArrow.Hide();
			RightArrow.Hide();
		}
	}

	public void SetLeftLimit()
	{
		xLeftLimit = 960;
		if (RoomName == "Town") {
			MovementEnabled = true;
			switch (StateData.CurrentMemory)
			{
			case "Neighbor":
				MovementEnabled = false;
				break;
			case "FountainGrass"://=
				break;
			case "Bowties":
				AllowTransition = true;
				break;
			}
		}
		else if (RoomName == "Forest") {
			MovementEnabled = true;
			AllowTransition = true;
			if (StateData.CurrentMemory == "Bowties") {
				xLeftLimit = 4200;
			}
			else if (StateData.CurrentMemory == "BushBowtie") {
				xLeftLimit = 2500;
			}
		}
	}

	public void ScreenTransition(bool toForest)
	{
		//=add stuff later?
		if (toForest)
		{
			GetTree().ChangeSceneToFile("res://scenes/forest.tscn");
		}
		else
		{
			GetTree().ChangeSceneToFile("res://scenes/town.tscn");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//movement
		if (MovementEnabled)
		{
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
				x: Mathf.Clamp(GlobalPosition.X, xLeftLimit, xRightLimit),
				y: GlobalPosition.Y
			);
		}
		
		//arrows
		// if (Velocity != Vector2.Zero)
		// {
			if (GlobalPosition.X == xLeftLimit || !MovementEnabled)
			{
				LeftArrow.Hide();
			}
			else
			{
				LeftArrow.Show();
			}
			if (RoomName == "Town" && (GlobalPosition.X == xRightLimit || !MovementEnabled))
			{
				RightArrow.Hide();
			}
			else
			{
				RightArrow.Show();
			}
		// }

		//transition
		if (GlobalPosition.X <= 960 && AllowTransition && RoomName == "Town")
		{
			ScreenTransition(true);
		}
		else if (GlobalPosition.X >= xRightLimit && RoomName == "Forest"  && AllowTransition)
		{
			ScreenTransition(false);
		}
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
