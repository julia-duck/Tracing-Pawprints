using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Forest : Node2D
{
	private TextBox Text;
	private InteractArea door;
	private InteractArea bush;
	private InteractArea clearing;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Text = GetNode<TextBox>("Camera/TextBox");
		door = GetNode<InteractArea>("DoorInteract");
		bush = GetNode<InteractArea>("BowBushInteract");
		clearing = GetNode<InteractArea>("ClearingInteract");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		switch (StateData.CurrentMemory) {
			case "Bowties": door.SetDisabled(false);
							bush.SetDisabled(true);
							clearing.SetDisabled(true);
							break;
			case "BushBowtie": bush.SetDisabled(false);
								door.SetDisabled(true);
								clearing.SetDisabled(true);
								break;
			case "Reunion": clearing.SetDisabled(false);
							door.SetDisabled(true);
							bush.SetDisabled(true);
							break;
			default: door.SetDisabled(true);
					bush.SetDisabled(true);
					clearing.SetDisabled(true);
					break;
		}
	}

	private async void OnDoorInteract(InteractArea trigger)
	{
		trigger.SetDisabled(true);
		await Text.BowtieShopOwnerScene();
		//=trigger.SetDisabled(false);
	}

	private async void OnBowBushInteract(InteractArea trigger)
	{
		 trigger.SetDisabled(true);
		await Text.BushBowtieScene();
		// //=trigger.SetDisabled(false);
	}

	private async void OnClearingInteract(InteractArea trigger)
	{
		trigger.SetDisabled(true);
		await Text.ReunionScene();
		//=trigger.SetDisabled(false);
	}
}
