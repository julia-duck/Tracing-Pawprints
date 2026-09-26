using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Forest : Node2D
{
	private TextBox Text;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Text = GetNode<TextBox>("Camera/TextBox");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private async void OnDoorInteract(InteractArea trigger)
	{
		trigger.SetDisabled(true);
		await Text.BowtieShopOwnerScene();
		//=trigger.SetDisabled(false);
	}

	private async void OnBowBushInteract(InteractArea trigger)
	{
		// trigger.SetDisabled(true);
		// await Text.BowtieShopOwnerScene();
		// //=trigger.SetDisabled(false);
	}

	private async void OnClearingInteract(InteractArea trigger)
	{
		trigger.SetDisabled(true);
		await Text.ReunionScene();
		//=trigger.SetDisabled(false);
	}
}
