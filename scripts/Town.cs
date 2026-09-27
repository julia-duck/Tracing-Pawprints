using Godot;
using System;
using System.Collections.Generic;

public partial class Town : Node2D
{
	[Signal]
	public delegate void GetFishEventHandler();
	private TextBox text;
	private Godot.Collections.Array<InteractArea> grasses;
	private InteractArea door;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (StateData.FirstStartGame) {
			StateData.FirstStartGame = false;
			GetNode<Popup>("/root/PopupGlobal").ShowPopup();
		}
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
		if (Input.IsActionJustPressed("enter"))
		{
			EmitSignal(SignalName.GetFish);
		}
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
		for (int i = 0; i < 7; i ++)
		{
			grasses[i].SetDisabled(toggle);
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
			//fish
			var rod = GetNode<Sprite2D>("FishingRod");
			rod.Frame = 0;
			var icon = GetNode<Sprite2D>("FishingIcon");
			icon.Frame = 1;
			icon.Show();
			var time = GD.RandRange(0.0, 5.0);
			await ToSignal(GetTree().CreateTimer((float) time), SceneTreeTimer.SignalName.Timeout);
			icon.Frame = 0;
			await ToSignal(this, SignalName.GetFish);
			icon.Hide();
			var fishNum = GD.RandRange(0, 3);
			var fish = GetNode<Sprite2D>("Fish");
			fish.Frame = fishNum;
			fish.Visible = true;
			var anim = GetNode<AnimationPlayer>("AnimationPlayer");
			anim.Play("fish");
			await ToSignal(anim, AnimationPlayer.SignalName.AnimationFinished);
			fish.Visible = false;
			rod.Frame = 1;
		}
		else
		{
			await text.NoBait();
		}
		await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
		trigger.SetDisabled(false);
	}

}
