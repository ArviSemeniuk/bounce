using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Signal]
	public delegate void StartGameEventHandler();
	[Signal]
	public delegate void StopGameEventHandler();

	public const float Speed = 300.0f;
	public const float JumpVelocity = -900.0f;
	public const float RollingSpeed = 400f;
	public const float Gravity = 1100f;
	
	private Vector2 _velocity;
	
	
	public override void _Ready()
	{
		var screenNotifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
		screenNotifier.ScreenExited += OnVisibleOnScreenNotifier2DScreenExited;
	}
	
	
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		EmitSignal(SignalName.StopGame);
	}
	
	
	public override void _PhysicsProcess(double delta)
	{
		//Vector2 velocity = Velocity;

		// Add the gravity.
		_velocity.Y += Gravity * (float)delta;

		if (Mathf.Abs(_velocity.X) < RollingSpeed)
			_velocity.X = RollingSpeed;
			
		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept"))
		{
			_velocity.Y = JumpVelocity;
			EmitSignal(SignalName.StartGame);
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			_velocity.X = direction.X * Speed;
		}
		else
		{
			_velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		
		// Movement
		Velocity = _velocity;
		MoveAndSlide();
		
		// Number of collisions
		var collisionCount = GetSlideCollisionCount();
				
		if (collisionCount == 0)
		{
			Rotation += 0.05f;
		}
		
		// Make player bounce
		for (int i = 0; i < collisionCount; i++)
		{
			// Iterate through each collision (usually only 1 in my case) 
			var collision = GetSlideCollision(i);
			var normal = collision.GetNormal();
			
			if (normal.Y < -0.7f)
			{
				// Bounce upward (reflect)
				_velocity = _velocity.Bounce(normal);
			}
			else if (Mathf.Abs(normal.X) > 0.7f)
			{
				// Side collision → reverse X a bit so it slides off
				_velocity.X = -_velocity.X * 0.8f;
			}
		}
		
		Velocity = _velocity;
	}
	
	
	public void Start(Vector2 position)
	{
		Position = position;
	}
}
