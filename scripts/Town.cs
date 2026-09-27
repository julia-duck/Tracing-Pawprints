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
		RefreshAreas();
	}
	public override void _Process(double delta)
	{
		/*switch (StateData.CurrentMemory)
		{
			case "Neighbor": ToggleArea1(false);
							 ToggleArea2(true);
							 break;
			case "FountainGrass": ToggleArea1(true);
								  ToggleArea2(false);
								  break;
			default: ToggleArea1(true);
					 ToggleArea2(true);
					 break;
		}*/
	}
	
	public void RefreshAreas()
	{
		switch (StateData.CurrentMemory)
		{
			case "Neighbor": ToggleArea1(false);
							 ToggleArea2(true);
							 break;
			case "FountainGrass": ToggleArea1(true);
								  ToggleArea2(false);
								  break;
			default: ToggleArea1(true);
					 ToggleArea2(true);
					 break;
		}
	}
	
	/*toggle = disabled*/
	public void ToggleArea1(bool toggle) {
		door.SetDisabled(toggle);
		GetNode<InteractArea>("YellowHouseDoor").SetDisabled(toggle);
		GetNode<InteractArea>("OrangeHouseDoor").SetDisabled(toggle);
		GetNode<InteractArea>("Tree").SetDisabled(toggle);
	}
	public void ToggleArea2(bool toggle) {
		grass1.SetDisabled(toggle);
		grass2.SetDisabled(toggle);
		GetNode<InteractArea>("Fountain").SetDisabled(toggle);
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
		RefreshAreas();
		//= trigger.SetDisabled(false);
	}

	public async void OnGrassInteract(InteractArea trigger)
	{
		trigger.SetDisabled(true);
		await text.FountainGrassScene();
		RefreshAreas();
		//= trigger.SetDisabled(false);
	}
	
	public async void OnYellowInteract(InteractArea trigger) 
	{
		trigger.SetDisabled(true);
		await text.YellowHouse();
	}
	
	public async void OnOrangeInteract(InteractArea trigger) 
	{
		trigger.SetDisabled(true);
		await text.OrangeHouse();
	}
	
	public async void OnTreeInteract(InteractArea trigger) 
	{
		trigger.SetDisabled(true);
		await text.Tree();
	}
	
	public async void OnFountainInteract(InteractArea trigger) 
	{
		trigger.SetDisabled(true);
		await text.Fountain();
	}
}
