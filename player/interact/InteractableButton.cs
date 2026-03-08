using Godot;

public partial class InteractableButton : Interactable
{

    [Signal]
    public delegate void PressedEventHandler();
	
	[Export]
	public ElevatorDoor door {get; set;}

    public override void Interact()
	{

		GD.Print("Parent is %s", door.GetType().ToString());
        GD.Print("Door is %s", door.ToString());

        // Connect the C# signal directly
        if(door is ElevatorDoor)
			((ElevatorDoor) door).ToggleDoor();
	}

}