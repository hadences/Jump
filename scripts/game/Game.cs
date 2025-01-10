using Godot;
using System;

/**
 * Game.cs
 * This script is responsible for managing the game.
 *
 * Main game script 
 */
public partial class Game : Node2D
{

	private const String SaveFilePath = "user://save_game.dat";

	[ExportCategory("References")]
	[Export] public Player player;
	[Export] public ObstacleSpawner obstacleSpawner;
	[Export] public Hud hud;
	[Export] public Chroma chromaTransitioner;

	private int currentScore = 0;
	private int highScore = 0;

	private bool gameStarted = false;
	private bool gameEnded = false;


	public override void _Ready()
	{
		loadHighscore();
		hud.showMainScreen(true);
		hud.showDeathScreen(false);
		obstacleSpawner.game = this;
		hud.updateHighScore(highScore);

	}

	private void loadHighscore(){
		var file = FileAccess.Open(SaveFilePath, FileAccess.ModeFlags.Read);

		if(file != null){
			highScore = (int) file.Get32();
			file.Close();
		}else{
			highScore = 0;
		}
	}

	private void saveHighScore(){
		var file = FileAccess.Open(SaveFilePath, FileAccess.ModeFlags.Write);

		if(file != null){
			file.Store32((uint) highScore);
			file.Close();
		}
	}

	public void startGame(){
		player.setHealth(player.maxHealth);
		player.Visible = true;
		player.canMove = true;
		hud.showMainScreen(false);
		obstacleSpawner.startSpawning();
	}

	public void resetGame() {
		gameStarted = false;
		gameEnded = false;
		hud.showMainScreen(true);
		hud.showDeathScreen(false);
		player.canMove = false;
		obstacleSpawner.clearObstacles();
	}

	public async void onPlayerJumped() {
		if(!gameStarted){
			gameStarted = true;
			startGame();
		}

		if(gameEnded){
			chromaTransitioner.triggerTransitionIn(Chroma.TransitionDirection.DIAGONAL);
			
			SoundManager.Instance.playSound(SoundManager.Instance.transitionSound, 1.0f, -5.0f);

        	await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
			
			// reset the score.
			updateHighScore();

			currentScore = 0;
			hud.updateScore(currentScore);

			resetGame();
			// trigger the transition out after 1 second.
        	await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
			chromaTransitioner.triggerTransitionout();
		}
	}

	public void updateHighScore(){
		if(currentScore > highScore){
			highScore = currentScore;
			hud.updateHighScore(highScore);
			saveHighScore();
		}
	}

	public void incrementScore(){
		currentScore++;
		SoundManager.Instance.playSound(SoundManager.Instance.scoreSound, 2.0f, -15.0f);
		hud.updateScore(currentScore);
	}

	public void onPlayerDeath() {
		hud.showDeathScreen(true);
		obstacleSpawner.stopSpawning();
		player.canMove = false;
		gameEnded = true;
	}
}
