using Godot;

public partial class ElevatorDoor : Node3D
{
    [Export] public AnimationPlayer animationPlayer;

    private bool isOpen = false;

    public override void _Ready()
    {

    }

    public void ToggleDoor()
    {
        GD.Print("HUGE WIN");

        if (animationPlayer == null)
        {
            GD.PrintErr("AnimationPlayer not assigned!");
            return;
        }

        GD.Print("ToggleDoor called, isOpen = ", isOpen);

        if (isOpen)
        {
            GD.Print("Playing close animation");
            animationPlayer.Play("Close");
            isOpen = false;
        }
        else
        {
            GD.Print("Playing open animation");
            animationPlayer.Play("Open");
            isOpen = true;
        }
    }
}