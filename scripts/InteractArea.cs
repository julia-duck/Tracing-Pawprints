using Godot;
using System;

public partial class InteractArea : Node2D
{
	[Signal]
	public delegate void ClickedEventHandler();
	[Export]
	public int IconNumber {get; set;} = 0;
	
	private static int VerticalIconOffset = 20;
	private Sprite2D IconShown;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		IconShown = GetParent().GetNode<Sprite2D>("Icon");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnMouseEntered()
	{
		GD.Print("Hover");
		IconShown.Position = new Vector2(Position.X, Position.Y - VerticalIconOffset);
		IconShown.Frame = IconNumber;
		IconShown.Show();
	}

	private void OnMouseExited()
	{
		IconShown.Hide();
	}

	private void OnButtonPressed()
	{
		GD.Print("Chicked");
		EmitSignal(SignalName.Clicked);
	}
}
