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
	}
	
	public void ToggleArea1(bool disabled)
	{
		door.SetDisabled(disabled);
	}
	public void ToggleArea2(bool disabled)
	{
		bush.SetDisabled(disabled);
		GetNode<InteractArea>("NoBowBush1").SetDisabled(disabled);
		GetNode<InteractArea>("NoBowBush2").SetDisabled(disabled);
		GetNode<InteractArea>("NoBowBush3").SetDisabled(disabled);
	}
	public void ToggleArea3(bool disabled)
	{
		clearing.SetDisabled(disabled);
	}
	
	public void RefreshAreas()
	{
		switch (StateData.CurrentMemory)
		{
			case "Bowties": ToggleArea1(false);
							ToggleArea2(true);
							ToggleArea3(true);
							break;
			case "BushBowtie": ToggleArea1(true);
							ToggleArea2(false);
							ToggleArea3(true);
							break;
			case "Reunion": ToggleArea1(true);
							ToggleArea2(true);
							ToggleArea3(false);
							break;
			default: ToggleArea1(true);
					 ToggleArea2(true);
					ToggleArea3(true);
					 break;
		}
	}

	private async void OnDoorInteract(InteractArea trigger)
	{
		trigger.SetDisabled(true);
		await Text.BowtieShopOwnerScene();
		RefreshAreas();
		//=trigger.SetDisabled(false);
	}

	private async void OnBowBushInteract(InteractArea trigger)
	{
		 trigger.SetDisabled(true);
		await Text.BushBowtieScene();
		RefreshAreas();
		// //=trigger.SetDisabled(false);
	}

	private async void OnClearingInteract(InteractArea trigger)
	{
		trigger.SetDisabled(true);
		await Text.ReunionScene();
		RefreshAreas();
		//=trigger.SetDisabled(false);
	}
	
	private async void OnNoBowBushInteract(InteractArea trigger)
	{
		trigger.SetDisabled(true);
		await Text.NoBowBush();
	}
}
