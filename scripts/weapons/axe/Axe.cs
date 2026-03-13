using Godot;
using System;

public partial class Axe : Node3D
{
    private AnimationPlayer anim;
    [Export] private RayCast3D ray;
    [Export] private AudioStreamPlayer3D splat;
    [Export] private AudioStreamPlayer3D swoosh;
    [Export] private AudioStreamPlayer3D thud;


    private bool isSwinging;
    private bool hasHit;

    private float minPitch = 0.7f;
    private float maxPitch = 1.3f;

    private void PlayRandomPitch(AudioStreamPlayer3D player)
    {
        if (player == null) return;
        player.PitchScale = (float)GD.RandRange(minPitch, maxPitch);
        player.Play();
    }
    public override void _Ready()
    {

        anim = GetNode<AnimationPlayer>("animations");
        hasHit = false;
        anim.AnimationFinished += OnAnimationFinished;
    }

    public override void _Process(double delta)
    {


        if(Input.IsActionJustPressed("attack"))
        {
            if(!isSwinging)
            {
                PlayRandomPitch(swoosh);
                Swing();
            }
        }
        
        if(isSwinging && !hasHit && ray.IsColliding())
        {
            hasHit = true;
            var collider = ray.GetCollider();
            
            if(collider is Enemy enemy)
            {
                PlayRandomPitch(splat);
                enemy.Hit(10f);
            }

            PlayRandomPitch(thud);
            //would be nice to fade out swoosh slowly here
        }

    }

    public void Swing()
    {
        anim.Play("swing");
        isSwinging = true;
    }

    private void FadeOutSwoosh(float duration = 0.05f) // 50 ms
    {
        if (!swoosh.Playing) return;

        var tween = GetTree().CreateTween();
        tween.TweenProperty(swoosh, "volume_db", -80f, duration)
            .SetTrans(Tween.TransitionType.Linear)
            .SetEase(Tween.EaseType.In);

        // Wrap the method in a Callable
        tween.TweenCallback(Callable.From(() => swoosh.Stop()));
    }
    // Callback for when any animation finishes
    private void OnAnimationFinished(StringName animName)
    {
        if (animName == "swing")
            isSwinging = false;
            hasHit = false;
    }
}