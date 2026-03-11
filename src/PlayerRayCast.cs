using Godot;

public partial class PlayerRayCast : RayCast3D
{
    public override void _PhysicsProcess(double delta)
    {
        if (IsColliding())
        {
            var collider = GetCollider();

            if (collider is Interactive interactable)
            {
                GD.Print("SeeButton");
                if (Input.IsActionJustPressed("interact"))
                {
                    GD.Print("PressButton");
                    interactable.Interact();
                }
            }
        }
    }
}