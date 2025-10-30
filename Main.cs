using Godot;
using System;

public partial class Main : Node
{
	private Player _player;
	private Platform _platform;
	private Marker2D _playerStartPos;
	private Marker2D _platformStartPos;
	private int _score = 0;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Get player and platform nodes
		_player = GetNode<Player>("Player");
		_platform = GetNode<Platform>("Platform");
		
		// Position the player and platform
		_playerStartPos = GetNode<Marker2D>("PlayerStartPos");
		_platformStartPos = GetNode<Marker2D>("PlatformStartPos");
		
		_player.Start(_playerStartPos.Position);
		_platform.Start(_platformStartPos.Position);
		
		var scoreTimer = GetNode<Timer>("ScoreTimer");
		scoreTimer.Timeout += OnScoreTimerTimeout;

		_player.StartGame += NewGame;
		_player.StopGame += End;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//public override void _Process(double delta)
	//{
	//}
	
	
	private void OnScoreTimerTimeout()
	{
		_score++;
		UpdateScore(_score);
	}
	
	
	public void UpdateScore(int score)
	{
		GetNode<Label>("Score").Text = "Time alive: " + score.ToString();
	}
	
	
	public void NewGame()
	{		
		_score = 0;
		
		_platform.Start(_platformStartPos.Position);
		_player.Start(_playerStartPos.Position);

		_platform.StartToMove();
		GetNode<Timer>("ScoreTimer").Start();
		UpdateScore(_score);
	}
	
	
	public void End()
	{
		GetNode<Timer>("ScoreTimer").Stop();
		_platform.StopMove();
	}
}
