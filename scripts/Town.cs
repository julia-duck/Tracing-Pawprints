using Godot;
using System;

public partial class Town : Node2D
{
	private TextBox text;
	private InteractArea grass1;
	private InteractArea grass2;
	private InteractArea door;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		text = GetNode<Camera2D>("Camera").GetNode<TextBox>("TextBox");
		grass1 = GetNode<InteractArea>("GrassInteract");
		grass2 = GetNode<InteractArea>("GrassInteract2");
		door = GetNode<InteractArea>("NeighborDoor");
	}
	public override void _Process(double delta)
	{
		switch (StateData.CurrentMemory)
		{
			case "Neighbor": door.SetDisabled(false);
							 grass1.SetDisabled(true);
							 grass2.SetDisabled(true);
							 break;
			case "FountainGrass": door.SetDisabled(true);
								  grass1.SetDisabled(false);
								  grass2.SetDisabled(false);
								  break;
			default: door.SetDisabled(true);
					 grass1.SetDisabled(true);
					 grass2.SetDisabled(true);
					 break;
		}
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
}
