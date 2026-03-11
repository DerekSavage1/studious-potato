using Godot;
using System;
using System.Linq.Expressions;


public partial class LegController : Fabrik3D
{

    [Export] float stepOffset{get; set;} = 3f; 
    private Vector3 desiredPosition;
    private Vector3 footPosition;
    private Marker3D IK_marker;
    private StepRay stepRay;
    private bool isStepping;

    public override void _Ready()
    {
        int foundCount = 0;

        foreach (Node child in GetChildren())
        {
            if (child is Marker3D node && node.TopLevel)
            {
                IK_marker = node;
                foundCount++;
            } else if(child is StepRay node1)
            {
                stepRay = node1;
            }
        }

        if (foundCount == 0)
        {
            GD.PrintErr("No IK marker canidates found among children.");
        }
        else if (foundCount > 1)
        {
            GD.PrintErr("Multiple IK marker canidates found! Only one is allowed Why are you trying to confuse me?");
        }

        if(stepRay == null)
        {
            GD.PrintErr("Spider Leg Has No StepRay Child");
            return;
        }
    }

    public override void _Process(double delta)
    {
        if(isStepping)
            return;

        desiredPosition = stepRay.getStepTargetPosition();
        

        if(footPosition.DistanceTo(desiredPosition) > stepOffset)
        {
            Step();
        } else
        {
            IK_marker.Position = footPosition;
        }
    }    

    public void Step()
    {
        isStepping = true;                    
        var halfway = (footPosition + desiredPosition) / 2;

        Tween tween = GetTree().CreateTween(); 
        tween.TweenProperty(IK_marker, "global_position", halfway + ((Node3D) Owner).Basis.Y, .1);
        tween.TweenProperty(IK_marker, "global_position", desiredPosition, .1);
        tween.TweenCallback(Callable.From(() => isStepping = false));

        footPosition = desiredPosition;
    }
}
