using Godot;
using System;

public partial class ElevatorButton : Interactable
{
	[Export]
	public ElevatorDoor door {get; set;}

	 [Export]
	 public ElevatorLight light {get; set;}

	public override void Interact()
	{
		
		GD.Print("Parent is %s", door.GetType().ToString());
		GD.Print("Door is %s", door.ToString());

		if(door is ElevatorDoor)
			door.ToggleDoor();
			
		light.illuminate();


	}
}
