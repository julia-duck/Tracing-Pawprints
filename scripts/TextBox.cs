using Godot;
using System;
using System.Threading.Tasks;

public partial class TextBox : Node2D
{	
	[Signal]
	public delegate void ContinueDialogueEventHandler();
	
	private Godot.Timer textTimer;
	private bool showingText;
	private RichTextLabel label;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		textTimer = GetNode<Godot.Timer>("Timer");
		label = GetNode<RichTextLabel>("%RichTextLabel");
		showingText = false;
	}
	
	//show text
	public async Task ShowText(String text)
	{
		label.Clear();
		label.AppendText("[font_size=30]" + text + "[/font_size]");
	
		showingText = true;
		Show();
		//textTimer.Start();
		
		await ToSignal(this, TextBox.SignalName.ContinueDialogue);
	}
	
	public override void _Input(InputEvent @event)
	{
		if (showingText && Input.IsActionJustPressed("enter")) //&& textTimer.IsStopped())
		{
			showingText = false;
			Hide();
			EmitSignal(SignalName.ContinueDialogue);
		}
	}
}
