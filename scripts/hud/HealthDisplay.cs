using Godot;
using System;
using System.Collections.Generic;

public partial class HealthDisplay : Control
{
	[Export] public Player player;
	[Export] public PackedScene heart;
	[Export] public float heartSpacing = 16.0f;

	private List<TextureRect> hearts = new List<TextureRect>();
	private Vector2 startPosition = Vector2.Zero;

	public override void _Ready()
	{
		startPosition = GlobalPosition;
		updateHealthDisplay();
	}

	public void updateHealthDisplay(){
		// Get the health of the player.
		int health = player.getHealth();

		if(health < 0) return;

		if(hearts.Count > health){
			// Remove the hearts.
			for(int i = hearts.Count - 1; i >= health; i--){
				hearts[i].QueueFree();
				hearts.RemoveAt(i);
			}
		}

		if(hearts.Count < health){
			// Add the hearts.
			for(int i = hearts.Count; i < health; i++){
				TextureRect heartInstance = (TextureRect)heart.Instantiate();
				hearts.Add(heartInstance);
				AddChild(heartInstance);
				heartInstance.GlobalPosition = startPosition + new Vector2(i * heartSpacing, 0);
			}
		}
	}

}
