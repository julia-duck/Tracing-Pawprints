using Godot;
using System;

public partial class Popup : CanvasLayer
{
	[Signal]
	public delegate void PopupConfirmedEventHandler();
	
	private AcceptDialog popup;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		popup = GetNode<AcceptDialog>("AcceptDialog");
		//popup.Confirmed += OnConfirmed();
		popup.Hide();
	}
	public void ShowPopup()
	{
		popup.Show();
	}
	public void OnConfirmed()
	{
		string text = GetNode<LineEdit>("%LineEdit").Text;
		if (text != "") {
			TextBox.cat = text;
		}
		string p1T = GetNode<LineEdit>("%P1").Text;
		string p2T = GetNode<LineEdit>("%P2").Text;
		if (p1T != "") {
			TextBox.pronoun1 = p1T;
		}
		if (p2T != "") {
			TextBox.pronoun2 = p2T;
		}
		EmitSignal(SignalName.PopupConfirmed);
	}
}
