using Godot;
using System;

public partial class Platform : CharacterBody2D
{
	private bool _startMove = false;
	private bool _moveRight = false;
	
	public float PlatformSpeed = 100f;
	public float SpeedUpFactor = 1.0f;
	
	
	// Called when the node enters the scene tree for the first time.
	//public override void _Ready()
	//{
	//}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (!_startMove)
			return;
		
		// Which way to move
		Vector2 platformVelocity = new Vector2(_moveRight ? PlatformSpeed * SpeedUpFactor : -PlatformSpeed * SpeedUpFactor, 0);
		
		var collision = MoveAndCollide(platformVelocity * (float)delta);
		if (collision != null)
		{
			if (collision.GetNormal().X > 0.8f)
			{
				_moveRight = true;
				SpeedUpFactor += 0.25f;
			}
			else if (collision.GetNormal().X < -0.8f)
			{
				_moveRight = false;
				SpeedUpFactor += 0.25f;
			}
		}
	}
	  
	
	public void Start(Vector2 position)
	{
		Position = position;
	}
	
	
	public void StartToMove()
	{
		_startMove = true;
		SpeedUpFactor = 1.0f;
	}
	
	
	public void StopMove()
	{
		_startMove = false;
	}
}
