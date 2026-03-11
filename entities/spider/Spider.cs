using Godot;

public partial class Spider : Node3D
{
    [Export] public float moveSpeed {get; set;} = 5f;
    [Export] float turnSpeed {get; set;} = 1f;
    [Export] float groundOffset {get; set;} = .5f;

    public override void _Process(double delta)
    {
        float dir = Input.GetAxis("forward", "backward");
        Translate(new Vector3(0, 0, -dir) * moveSpeed * (float) delta);

        float Adir = Input.GetAxis("right", "left");
        RotateObjectLocal(Vector3.Up, Adir * turnSpeed * (float) delta);
    }
    
}
