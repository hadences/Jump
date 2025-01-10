using Godot;
using System;
using System.Collections.Generic;

/**
 * ObstacleSpawner.cs
 * This script is responsible for spawning obstacles in the game.
 */
public partial class ObstacleSpawner : Node2D
{

	[Export]
	public PackedScene obstacleScene;

	[Export]
	public float spawnInterval = 1.0f; // in Seconds

	public Game game { get; set; }

	private Timer timer;
	private List<Obstacle> obstacles = new List<Obstacle>();

    public override void _Ready()
    {
		// Create a new timer.
		timer = new Timer();
		AddChild(timer);

		timer.WaitTime = spawnInterval;
		timer.OneShot = false;

		// Connect the timeout signal of the timer to the onTimerTimeout method.
		timer.Timeout += () => onTimerTimeout();
    }

	public void startSpawning(){
		timer.Start();
	}

	public void stopSpawning(){
		timer.Stop();

		foreach(Obstacle obstacle in obstacles){
			obstacle.speed = 0;
		}
	}

	public void clearObstacles(){
		foreach(Obstacle obstacle in obstacles){
			obstacle.QueueFree();
		}

		obstacles.Clear();
	}

    public void spawnObstacle()
	{
		// Create a new instance of the obstacle scene.
		Obstacle obstacle = (Obstacle)obstacleScene.Instantiate();
		AddChild(obstacle);

		// Add the obstacle to the list of obstacles.
		obstacles.Add(obstacle);

		// set the game
		obstacle.game = game;

		// Set the position of the obstacle.
		Vector2 spawnPos = this.GlobalPosition;
		
		// Set the obstacle position to the spawn position.
		obstacle.GlobalPosition = spawnPos;
	}

	public void onTimerTimeout()
	{
		spawnObstacle();
	}
}
