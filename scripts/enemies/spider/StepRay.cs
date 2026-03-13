using Godot;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
public partial class StepRay : RayCast3D
{
    private Vector3 stepTargetPosition;
    public override void _PhysicsProcess(double delta)
    {
        if(IsColliding())
        {
            stepTargetPosition = GetCollisionPoint();
        }
            
    }

    public Vector3 getStepTargetPosition()
    {
        return stepTargetPosition;
    }
    
}
