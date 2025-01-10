using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Signal] public delegate void PlayerCollidedEventHandler();
	[Signal] public delegate void PlayerDiedEventHandler();
	[Signal] public delegate void PlayerJumpedEventHandler();

	[Export] public float jumpVelocity = 400.0f;
	[Export] public int maxHealth = 3;
	[Export] public int health = 3;

	public bool canMove = true;

	[ExportCategory("References")]
	[Export] public HealthDisplay healthDisplay;

	public override void _Ready()
	{
		// Invert the jump velocity as the Y axis is inverted.
		jumpVelocity *= -1; 

		// set the health to the max health.
		health = maxHealth;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump"))
		{
			EmitSignal(SignalName.PlayerJumped);
			if(IsOnFloor() && canMove){
				SoundManager.Instance.playSound(SoundManager.Instance.jumpSound, 1.5f, -5.0f);


				velocity.Y = jumpVelocity;
			}
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void triggerPlayerCollisionEvent(){
		EmitSignal(SignalName.PlayerCollided);
	}

	public void triggerPlayerDiedEvent(){
		EmitSignal(SignalName.PlayerDied);
	}

	public void setHealth(int newHealth){
		health = newHealth;
		healthDisplay.updateHealthDisplay();
	}

	public int getHealth(){
		return health;
	}

	public void damage(int damage){

		// Play the hurt sound.
		SoundManager.Instance.playSound(SoundManager.Instance.hurtSound, 1.5f, -10.0f);

		health -= damage;
		if(health <= 0){
			// Player is dead.
			SoundManager.Instance.playSound(SoundManager.Instance.deathSound, 1.5f, -5.0f);
			triggerPlayerDiedEvent();
		}

		// Update the health display.
		healthDisplay.updateHealthDisplay();
	}
}
