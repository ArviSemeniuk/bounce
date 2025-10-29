using Godot;
using System;

public partial class Main : Node
{
	private Player _player;
	private Platform _platform;
	private bool _isReady = false;
	private int _score = 0;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = GetNode<Player>("Player");
		_platform = GetNode<Platform>("Platform");
		
		var scoreTimer = GetNode<Timer>("ScoreTimer");
		scoreTimer.Timeout += OnScoreTimerTimeout;

		NewGame();
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_isReady = _player.IsReady();
		
		if (_isReady) {
			_platform.StartToMove();
			GetNode<Timer>("ScoreTimer").Start();
			SetProcess(false);
		}
	}
	
	private void OnScoreTimerTimeout()
	{
		_score++;
		UpdateScore(_score);
	}

	
	public void UpdateScore(int score)
	{
		GetNode<Label>("Score").Text = "Score: " + score.ToString();
	}
	
	
	public void NewGame()
	{
		var playerStartPos = GetNode<Marker2D>("PlayerStartPos");
		var platformStartPos = GetNode<Marker2D>("PlatformStartPos");
		_player.Start(playerStartPos.Position);
		_platform.Start(platformStartPos.Position);
		UpdateScore(_score);
	}
}
