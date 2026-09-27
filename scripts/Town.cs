using Godot;
using System;
using System.Collections.Generic;

public partial class Town : Node2D
{
	private TextBox text;
	private Godot.Collections.Array<InteractArea> grasses;
	private InteractArea door;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		text = GetNode<Camera2D>("Camera").GetNode<TextBox>("TextBox");
		grasses = new Godot.Collections.Array<InteractArea>();
		for (int i = 1; i <= 8; i ++)
		{
			var grass = GetNode<InteractArea>("GrassInteract" + i);
			grasses.Add(grass);
		}
		door = GetNode<InteractArea>("NeighborDoor");
		RefreshAreas();
	}
	public override void _Process(double delta)
	{
	}
	
	public void RefreshAreas()
	{
		GD.Print("Refreshing>");//=
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
		GD.Print("Area 2");//=
		for (int i = 0; i < 7; i ++)
		{
			grasses[0].SetDisabled(true);
		}
		GetNode<InteractArea>("Fountain").SetDisabled(toggle);
	}
	
	public async void OnNeighborInteract(InteractArea trigger)
	{
		trigger.SetDisabled(true);
		var marcy = GetNode<Sprite2D>("Marcy");
		marcy.Show();
		await text.NeighborScene();
		marcy.Hide();
		RefreshAreas();
	}

	public async void OnGrassInteract(InteractArea trigger)
	{
		trigger.SetDisabled(true);
		await text.FountainGrassScene();
		RefreshAreas();
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

	public async void OnFishingInteract(InteractArea trigger) 
	{
		trigger.SetDisabled(true);
		if (StateData.Bait)
		{
		}
		else
		{
			await text.NoBait();
		}
		trigger.SetDisabled(false);
	}

}
