using Godot;

/**
* SoundManager.cs
* This script is responsible for managing the sounds in the game.
* To use, add this script as an autoload in the project settings.
* By: JAESU (hadences)
*/
public partial class SoundManager : Node2D
{

    public static SoundManager Instance { get; private set; }

    private static AudioStreamPlayer audioStreamPlayer;

    public override void _Ready()
    {
        Instance = this;
        registerSounds();
        audioStreamPlayer = new AudioStreamPlayer();
        AddChild(audioStreamPlayer);
    }

    /**
     * Register the sounds in the game.
     *
     * declare all the sounds in the game here.
     */
    public void registerSounds(){
        jumpSound = GD.Load<AudioStreamWav>("res://assets/sounds/jump.wav");
        deathSound = GD.Load<AudioStreamWav>("res://assets/sounds/death.wav");
        hurtSound = GD.Load<AudioStreamWav>("res://assets/sounds/hurt.wav");
        transitionSound = GD.Load<AudioStreamWav>("res://assets/sounds/transition.wav");
        scoreSound = GD.Load<AudioStreamWav>("res://assets/sounds/score.wav");
    }

    /**
     * Play the sound.
     *
     * since the instance is now global, we can call this function from anywhere in the game.
     *
     * @param sound The sound to play.
     * @param pitch The pitch of the sound.
     * @param volume The volume of the sound.
     */
    public void playSound(AudioStream sound, float pitch, float volume){
        audioStreamPlayer.PitchScale = pitch;
        audioStreamPlayer.VolumeDb = volume;
        audioStreamPlayer.Stream = sound;
        audioStreamPlayer.Play();
    }

    public AudioStream jumpSound { get; private set; }

    public AudioStream deathSound { get; private set; }

    public AudioStream hurtSound { get; private set; }

    public AudioStream transitionSound { get; private set; }

    public AudioStream scoreSound { get; private set; }
}