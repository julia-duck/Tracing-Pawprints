using Godot;
using System;
using System.Threading.Tasks;

public partial class TextBox : Node2D
{	
	[Signal]
	public delegate void ContinueDialogueEventHandler();
	public const string DEFAULT_CAT_NAME = "Autumn";
	public static string cat = DEFAULT_CAT_NAME;
	public static string pronoun1 = "she";
	public static string pronoun2 = "her";
	
	private Godot.Timer textTimer;
	private bool showingText;
	private RichTextLabel label;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		textTimer = GetNode<Godot.Timer>("Timer");
		label = GetNode<RichTextLabel>("%RichTextLabel");
		showingText = false;
		Hide();
	}
	
	//show text
	public async Task ST(String text)
	{
		label.Clear();
		label.AppendText("[font_size=50]" + text + "[/font_size]");
	
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
	public async Task IntroScene()
	{
		await ST("It feels like forever ago since you held " + cat + " in your arms, even though " + cat + " only disappeared this morning.");
		await ST("You gulp, stifling an anxious sob. You wonder if you should put up posters and sit back waiting or look for " + cat + " yourself.");
		await ST("Suddenly, a memory threads through your mind.");
		await ST("The memory is from last fall. You are holding " + cat + " and going to visit your neighbor.");
		await ST(cat + " meows in protest but calms when set down. While you chat with the neighbor about an autumn gathering, " + cat + " spots a red flannel shirt.");
		await ST("You and your neighbor watch in amusement as " + cat + " snuggles into the shirt, falling fast asleep.");
		await ST("You: I have to get " + cat + " back. And I know where to look first.");
	}
	
	public async Task NeighborScene()
	{
		await ST("Neighbor: Well, hello! What brings you here, dear? Where's your sweet sweet kitty?");
		await ST("You: Hi, Marcy. I've lost " + cat + ". " + pronoun1 + " escaped this morning and I was wondering if you saw " + pronoun2 + ".");
		await ST("Marcy: Oh, that's horrible! I'm sorry, but I'm up late in the mornings and haven't seen your cat.");
		await ST("You nod, hiding your disappointment and dread. Winter will come soon; what will " + cat + " do then?");
		await ST("Marcy smiles in sympathy and goes to bring her clothes in from the clothes line.");
		await ST("Marcy: Wait, my red flannel shirt is missing!");
		await ST("Your heart pounds with renewed hope.");
		await ST("You: " + cat + " must have passed through here! Maybe there's something else here that will help me find " + pronoun2 + "!");
	}
	
	public async Task FountainGrassScene()
	{
		await ST("You: Hmm, that's interesting. The grass seems to be shorter on this side.");
		await ST("Suddenly, you become very still as another memory hits you. It is hazy at first but begins to solidify.");
		await ST("You are going to the lake to cool off. You don't expect " + cat + " to be interested, as " + pronoun1 + " hates getting wet, but you bring " + pronoun2 + " along anyways.");
		await ST(cat + " isn't interested in the water. But " + pronoun1 + " is very interested in the fountain grass beside it.");
		await ST("You have no idea why, but " + cat + " kept trying to eat the grass.");
		await ST("You wondered why the grass tasted good to " + cat + ", but you didn't dare try it yourself.");
	}
	
	public async Task BowtieShopOwnerScene()
	{
		await ST("You: Hello. I've lost my cat, and I think " + pronoun1 + " went this way. Have you seen " + pronoun2 + "?");
		await ST("Shop Owner: How funny, it seems to be the day of losing things. I've lost something too: My orange bowtie. It vanished while I was taking a walk.");
		await ST("You frown; you don't think losing " + cat + " is remotely funny. But your frown begins to turn into a hopeful smile.");
		await ST(cat + " loved carrying bowties around, you remember. Bowties of all colors. " + cat + " loved it more than yarn, even!");
		await ST("You: Where were you walking when you lost your bowtie?");
		await ST("The shop owner indicates towards the forest nearby.");
		await ST("You: Thank you! I'll let you know if I find your bowtie---and my cat.");
	}
	
	public async Task ReunionScene()
	{
		await ST("You are choked with tears and unable to speak at first.");
		await ST("The memories of " + cat + " have made you miss " + pronoun2 + " more than you'd thought possible.");
		await ST("You: " + cat + "..." + cat + "...is it really you?");
		await ST(cat + ": Purrrrrrrrrrr");
		await ST("You: You don't seem worried in the slightest, sunbathing like that!");
		await ST("You give out a husky laugh in a sudden rush of relief that drains your energy.");
		await ST("Come on, let's go home, " + cat + ". Then you can have all the tuna you want!");
		await ST(cat + ": PURRRRRRRRRRRRRRRR!");
	}
}
