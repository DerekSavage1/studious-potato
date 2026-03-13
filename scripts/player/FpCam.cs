using Godot;

public partial class FpCam : Node3D
{
    private float sensitivity = 0.2f;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion motion)
        {
            Node3D playerBody = GetParent<Node3D>();

            // Yaw (left/right)
            playerBody.RotateY(Mathf.DegToRad(-motion.Relative.X * sensitivity));

            // Pitch (up/down)
            RotateX(Mathf.DegToRad(-motion.Relative.Y * sensitivity));

            Vector3 rot = Rotation;
            rot.X = Mathf.Clamp(rot.X, Mathf.DegToRad(-90f), Mathf.DegToRad(90f));
            Rotation = rot;


        }
    }
}