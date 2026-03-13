using System.Diagnostics;
using System.Security.Cryptography;
using Godot;

public partial class Spider : Node3D
{

    
    [Export] public LegController FL;
    [Export] public LegController FR;
    [Export] public LegController BR;
    [Export] public LegController BL;
    [Export] public float MoveSpeed {get; set;} = 10f;
    [Export] float TurnSpeed {get; set;} = 2f;
    [Export] float GroundOffset {get; set;} = 1f;
    public override void _Process(double delta)
    {
        ShootingStars(delta);
        HandleMovement(delta);
    }

    
    private void HandleMovement(double delta)
    {
        float dir = Input.GetAxis("forward", "backward");
        Translate(new Vector3(0, 0, -dir) * MoveSpeed * (float) delta);

        float Adir = Input.GetAxis("right", "left");
        RotateObjectLocal(Vector3.Up, Adir * TurnSpeed * (float) delta);
    }


    // can we pretend that airplanes in the night sky are like shooting stars
    // cause I could use a wish right now, wish right now, wish right now
    private void ShootingStars(double delta)
    {


        Vector3 p1 = FL.getFootPosition();
        Vector3 p2 = FR.getFootPosition();
        Vector3 p3 = BR.getFootPosition();
        Vector3 p4 = BL.getFootPosition();
        DebugDraw3D.DrawSphere(p1);
        DebugDraw3D.DrawSphere(p2);
        DebugDraw3D.DrawSphere(p3);
        DebugDraw3D.DrawSphere(p4);

        Plane plane1 = new(p1, p2, p3);
        Plane plane2 = new(p2, p3, p4);
        Vector3 avgNormal = ((plane1.Normal + plane2.Normal) / 2).Normalized();
        
        Vector3 forward = Transform.Basis.Z; // keep current forward
        Vector3 right = forward.Cross(avgNormal).Normalized();
        forward = avgNormal.Cross(right).Normalized();

        // Basis b = new Basis(right, avgNormal, forward);
        
        Basis b = BasisFromNormal(avgNormal).Orthonormalized();
        b.Orthonormalized();

        DebugDraw3D.DrawRay(GlobalPosition, b.Y, 10);


        Quaternion currentQ = Transform.Basis.GetRotationQuaternion();
        Quaternion targetQ = b.GetRotationQuaternion();

        Transform3D sleeerrrrp = new();
        sleeerrrrp.Basis.Slerp(new Basis(targetQ).Orthonormalized(), 0.5f);

        Transform.InterpolateWith(sleeerrrrp, 0.5f);
    }

    private Basis BasisFromNormal(Vector3 normal)
    {
        normal = normal.Normalized();

        Vector3 tangent;

        if (Mathf.Abs(normal.Dot(Vector3.Up)) > 0.99f)
            tangent = Vector3.Forward;
        else
            tangent = Vector3.Up;

        Vector3 x = tangent.Cross(normal).Normalized();
        Vector3 z = normal.Cross(x).Normalized();

        Basis b = new Basis(x, normal, z);
        return b;
    }
}
