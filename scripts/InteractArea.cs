using Godot;
using System;

public partial class InteractArea : Node2D
{
	[Signal]
	public delegate void ClickedEventHandler(InteractArea trigger);
	[Export]
	public int IconNumber {get; set;} = 0;
	[Export]
	
	public int VerticalIconOffset {get; set; }= 20;
	private Sprite2D IconShown;
	private bool Disabled;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		IconShown = GetParent().GetNode<Sprite2D>("Icon");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetDisabled(bool disabled)
	{
		GetNode<Button>("Button").Disabled = disabled;
		Disabled = disabled;
	}

	private void OnMouseEntered()
	{
		if (!Disabled)
		{
			IconShown.Position = new Vector2(Position.X, Position.Y - VerticalIconOffset);
			IconShown.Frame = IconNumber;
			IconShown.Show();
		}
	}

	private void OnMouseExited()
	{
		IconShown.Hide();
	}

	private void OnButtonPressed()
	{
		EmitSignal(SignalName.Clicked, GetNode<InteractArea>(GetPath()));
	}
}
