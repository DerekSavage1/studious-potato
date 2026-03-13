using Godot;
using System;

public partial class StepTargetContainer : Node3D
{
    [Export] public float offset {get; set;} = 20f;
    Node3D parent;
    Vector3 prevPos;



    public override void _Ready()
    {
        parent = GetParentNode3D();
        prevPos = parent.GlobalPosition;

    }

    public override void _Process(double delta)
    {
        HandleMovement(delta);
    }

    private void HandleMovement(double delta)
    {
        var velocity = parent.GlobalPosition - prevPos;
        GlobalPosition = parent.GlobalPosition + velocity * offset;
        prevPos = parent.GlobalPosition;
    }
}
