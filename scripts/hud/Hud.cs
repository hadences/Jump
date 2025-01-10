using Godot;
using System;

public partial class Hud : CanvasLayer
{
	[ExportCategory("References")]
	[Export] public Control mainScreen;
	[Export] public Control deathScreen;
	[Export] public Label scoreLabel;
	[Export] public Label highScoreLabel;

	public void showMainScreen(Boolean show){
		mainScreen.Visible = show;
	}

	public void showDeathScreen(Boolean show){
		deathScreen.Visible = show;
	}

	public void updateScore(int score){
		scoreLabel.Text = score.ToString();
	}

	public void updateHighScore(int score){
		highScoreLabel.Text = "High Score : " + score.ToString();
	}

}
