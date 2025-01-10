using Godot;
using System;

public partial class Chroma : CanvasLayer
{

	[Export] private AnimationPlayer animationPlayer;
	[Export] private ColorRect screen;

	public enum TransitionDirection{
		HORIZONTAL = 0,
		VERTICAL = 1,
		DIAGONAL = 2
	}

	public void triggerTransitionIn(TransitionDirection direction){
		Variant value = (int)direction;
		screen.Material.Set("shader_parameter/direction", value);
		animationPlayer.Play("TransitionIn");
	}

	public void triggerTransitionout(){
		animationPlayer.Play("TransitionOut");
	}
}
