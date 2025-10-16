using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -700.0f;
	public const float BounceFactor = 1.0f; // 1.0 = perfect bounce, 0 = no bounce


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
			Rotation += 0.1f; 
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		
		Velocity = velocity;
		MoveAndSlide();
		
		// Make player bounce
		GD.Print("GetSlideCollisionCount: ", GetSlideCollisionCount());

		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			GD.Print("i: ", i);
			var collision = GetSlideCollision(i);
			
			GD.Print("collision: ", collision);

			var normal = collision.GetNormal();
			
			GD.Print("normal: ", normal);
			GD.Print("normal.Y: ", normal.Y);

			// If we collided with something from below or the side
			if (normal.Y < 0.0f)
			{
				// Bounce upward
				velocity = velocity.Bounce(normal);
				Velocity = velocity;
			}
		}
	}
	
	
	public void Start(Vector2 position)
	{
		Position = position;
	}
}
