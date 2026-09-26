using Godot;
using System;

public partial class Town : Node2D
{
	private TextBox text;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		text = GetNode<Camera2D>("Camera").GetNode<TextBox>("TextBox");
		StartDialogue();
	}
	public async void StartDialogue()
	{
		// await text.IntroScene();
		// await text.FountainGrassScene();
		// await text.BowtieShopOwnerScene();
	}
	
	public async void OnNeighborInteract(InteractArea trigger)
	{
		trigger.SetDisabled(true);
		await text.NeighborScene();
		//= trigger.SetDisabled(false);
	}

	public async void OnGrassInteract(InteractArea trigger)
	{
		trigger.SetDisabled(true);
		await text.FountainGrassScene();
		//= trigger.SetDisabled(false);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
