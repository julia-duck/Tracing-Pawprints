using Godot;
using System;
using System.Threading.Tasks;

public partial class TitleScreen : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private async void OnStartButtonPressed()
	{
		// var popup = GetNode<Popup>("Popup");
		// popup.Show();
		// await ToSignal(popup, Popup.SignalName.PopupConfirmed);
		//intro text
		GetNode<Button>("StartButton").Disabled = true;
		GetNode<ColorRect>("ColorRect").Show();
		var text = GetNode<TextBox>("Camera/TextBox");
		await text.IntroScene();
		GetTree().ChangeSceneToFile("res://scenes/town.tscn");
	}
}
