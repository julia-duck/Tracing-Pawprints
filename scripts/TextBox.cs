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
	private Camera camera;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		camera = (Camera) GetParent();
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
		await ST("It feels like forever ago since you held your cat in your arms, even though your cat only disappeared this morning.");
		await ST("You gulp, stifling an anxious sob. You wonder if you should put up posters and sit back waiting or look for your cat yourself.");
		await ST("Suddenly, a memory threads through your mind.");
		await ST("The memory is from last fall. You are holding your cat and going to visit your neighbor.");
		await ST("Your cat meows in protest but calms when set down. While you chat with the neighbor about an autumn gathering, your cat spots a red sweater.");
		await ST("You and your neighbor watch in amusement as your cat snuggles into the sweater, falling fast asleep.");
		await ST("You: I have to get my cat back. And I know where to look first.");
	}
	
	public async Task NeighborScene()
	{
		StateData.CurrentMemory = "FountainGrass";
		await ST("Neighbor: Well, hello! What brings you here, dear? Where's your sweet sweet kitty?");
		await ST("You: Hi, Marcy. I've lost " + cat + ". " + pronoun1 + " escaped this morning and I was wondering if you saw " + pronoun2 + ".");
		await ST("Marcy: Oh, that's horrible! I'm sorry, but I'm up late in the mornings and haven't seen your cat.");
		await ST("You nod, hiding your disappointment and dread. Winter will come soon; what will " + cat + " do then?");
		await ST("Marcy smiles in sympathy and goes to bring her clothes in from the clothes line.");
		await ST("Marcy: Wait, my red sweater is missing!");
		await ST("Your heart pounds with renewed hope.");
		await ST("You: " + cat + " must have passed through here! Maybe there's something else here that will help me find " + pronoun2 + "!");
		camera.SetLeftLimit();
	}
	
	public async Task FountainGrassScene()
	{
		StateData.CurrentMemory = "Bowties";
		await ST("You: Hmm, that's interesting. The grass seems to be shorter on this side.");
		await ST("Suddenly, you become very still as another memory hits you. It is hazy at first but begins to solidify.");
		camera.ToggleImage("Grass", true);
		await ST("You are going to the lake to cool off. You don't expect " + cat + " to be interested, as " + pronoun1 + " hates getting wet, but you bring " + pronoun2 + " along anyways.");
		await ST(cat + " isn't interested in the water. But " + pronoun1 + " is very interested in the fountain grass beside it.");
		await ST("You have no idea why, but " + cat + " kept trying to eat the grass.");
		await ST("You wondered why the grass tasted good to " + cat + ", but you didn't dare try it yourself.");
		camera.ToggleImage("Grass", false);
		camera.SetLeftLimit();
	}
	
	public async Task BowtieShopOwnerScene()
	{
		StateData.CurrentMemory = "BushBowtie";
		await ST("You: Hello. I've lost my cat, and I think " + pronoun1 + " went this way. Have you seen " + pronoun2 + "?");
		await ST("Shop Owner: How funny, it seems to be the day of losing things. I've lost something too: My orange bowtie. It vanished while I was taking a walk.");
		await ST("You frown; you don't think losing " + cat + " is remotely funny. But your frown begins to turn into a hopeful smile.");
		camera.ToggleImage("Bow", true);
		await ST(cat + " loved carrying bowties around, you remember. Bowties of all colors. " + cat + " loved it more than yarn, even!");
		camera.ToggleImage("Bow", false);
		await ST("You: Where were you walking when you lost your bowtie?");
		await ST("The shop owner indicates towards the forest nearby.");
		await ST("You: Thank you! I'll let you know if I find your bowtie---and my cat.");
		camera.SetLeftLimit();
	}
	
	public async Task BushBowtieScene()
	{
		StateData.CurrentMemory = "Reunion";
		await ST("You look carefully through bushes, searching for hints that " + cat + " has been here.");
		await ST("Suddenly, you find a bowtie in one of the bushes!");
		await ST("You: Did that Bowtie shop owner drop it in here...?");
		await ST("You: Or did " + cat + " steal it and put it in the bush?");
		camera.SetLeftLimit();
	}
	
	public async Task ReunionScene()
	{
		StateData.CurrentMemory = "End";
		camera.ToggleImage("Reunion", true);
		Position = new Vector2(Position.X, 40);
		await ST("You stop in your tracks upon seeing a lump of orange-white fur curled among the roots of a tree.");
		await ST("You are choked with tears and unable to speak at first.");
		await ST("The memories of " + cat + " have made you miss " + pronoun2 + " more than you'd thought possible.");
		await ST("You: " + cat + "..." + cat + "...is it really you?");
		await ST(cat + ": Purrrrrrrrrrr");
		await ST("You: You don't seem worried in the slightest, sunbathing like that!");
		await ST("You give out a husky laugh in a sudden rush of relief that drains your energy.");
		await ST("Come on, let's go home, " + cat + ". Then you can have all the tuna you want!");
		await ST(cat + ": PURRRRRRRRRRRRRRRR!");
		camera.ToggleImage("Reunion", false);
		Position = new Vector2(Position.X, 510);
	}
	
	//not main storyline
	public async Task YellowHouse() 
	{
		await ST("Out of desparation, you knock on the door of a neighbor you don't know well.");
		await ST("You: Maybe they've seen " + cat + ". I have to try!");
		await ST("An old lady emerges and peers down at you through her glasses with narrowed eyes.");
		await ST("Old Lady: Don't you see the sign? No soliciting!");
		await ST("You: I was just wondering ---");
		await ST("Old Lady: The answer is no! I'm not buying your stuff.");
		await ST("The old lady slams the door in your face. You frown.");
		await ST("You: But...I'm not even holding anything to sell.");
	}
	public async Task OrangeHouse() 
	{
		await ST("You knock on the door and a young man answers it. He seems to be a college student, with thin-rimmed round glasses and black hair.");
		await ST("You: Hello, sorry to bother you. I was wondering if you saw my cat around today. Tortoishell, orange and white fur?");
		await ST("Young Man: Sorry to disappoint you, but I've been locked in my room all day studying for an exam.");
		await ST("Young Man: I hope you find your cat, though.");
	}
	public async Task Tree()
	{
		await ST(cat + " acted more like a dog than a cat sometimes; " + pronoun1 + " didn't like climbing trees but loved chasing squirrles.");
		await ST(cat + " was usually lazy and would stop at the base of the tree and meow when the squirrel climbed up.");
		await ST("But maybe " + cat + " was determined enough this time and climbed the tree. Or maybe there was a squirrel up there that could lure " + pronoun1 + " back home.");
		await ST("You are worrying about how to catch a squirrel to attract " + cat + " with, but turns out you don't have to worry.");
		await ST("There are no squirrels in the tree, and " + cat + " isn't there either.");
	}
	public async Task Fountain()
	{
		await ST("You approach the fountain, hiding exhaustion behind a bundle of anxiety. You dip your hand into the cool water and watch as water droplets roll off your fingers.");
		await ST("Something about this place, this fountain, reminds you of " + cat + ". What was it, though? " + cat + " hated water, after all.");
	}
	
	//forest
	public async Task NoBowBush()
	{
		await ST("You look carefully through bushes, searching for hints that " + cat + " has been here.");
		await ST("You gently part the leaves and branches, but see no flash of color other than the orange and brown of the leaves.");
		await ST("You: If the bow was orange, it might've blended in...");
	}
}
