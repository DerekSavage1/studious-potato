using Godot;
using System;
using System.Diagnostics;

public partial class FpCam : Node3D
{

	public override void _Ready() {
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}
	private float @sensitivity = 0.2f;

	public override void _Input(InputEvent @event) {
		if(@event is InputEventMouseMotion motion) {
			Node3D playerBody = GetParent<Node3D>();
			Node3D camera = this;

			// PlayerBody (Node3D)         rotate x
 			// 	└── CameraPivot (Node3D)   rotate y
	  		// 		└── Camera3D

			Debug.Print("Hello");
			
			playerBody.RotateY(Mathf.DegToRad(-motion.Relative.X * sensitivity));
			camera.RotateX(Mathf.DegToRad(-motion.Relative.Y * sensitivity));

			Vector3 rot = Rotation;
			rot.X = Mathf.Clamp(rot.X, Mathf.DegToRad(-90.0f), Mathf.DegToRad(90.0f));
			Rotation = rot;
		}

	}
}
