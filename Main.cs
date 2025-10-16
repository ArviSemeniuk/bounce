using Godot;
using System;

public partial class Main : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NewGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	
	public void NewGame()
	{
		var player = GetNode<Player>("Player");
		var platform = GetNode<Platform>("Platform");
		
		var playerStartPos = GetNode<Marker2D>("PlayerStartPos");
		var platformStartPos = GetNode<Marker2D>("PlatformStartPos");

		player.Start(playerStartPos.Position);
		platform.Start(platformStartPos.Position);
	}
}
