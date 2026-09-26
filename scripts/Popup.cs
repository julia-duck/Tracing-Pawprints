using Godot;
using System;

public partial class Popup : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//AcceptDialog popup = GetNode<CanvasLayer>("/root/Popup").GetNode<AcceptDialog>("AcceptDialog");
		//popup.Confirmed += OnConfirmed();
		Show();
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
	}
}
