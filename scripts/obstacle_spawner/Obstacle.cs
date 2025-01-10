using Godot;
using System;

public partial class Obstacle : Area2D
{

	[Export]
	public float speed = 100.0f;

	public Game game { get; set; }

	public override void _PhysicsProcess(double delta)
	{
		// move the obstacle to the left.
		Vector2 velocity = new Vector2(-speed, 0);
		this.Position += velocity * (float)delta;
	}

	// called whenever a player collides with the score area.
	// this will tell the game to increment the score.
	public void onScoreCollision(Area2D area){
		if(area.GetParent() is not Player) return;
		if(game == null) return;

		game.incrementScore();
	}

	// trigger the player collision event whenever a player hits the obstacle.
	public void onAreaEntered(Area2D area)
	{
		if(area.GetParent() is Player player){
			player.triggerPlayerCollisionEvent();
			player.damage(1);
		}
	}
}
