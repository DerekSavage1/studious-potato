using Godot;

public partial class PlayerRayCast : RayCast3D
{
    public override void _PhysicsProcess(double delta)
    {
        if (IsColliding())
        {
            var collider = GetCollider();

            if (collider is Interactable interactable)
            {
                if (Input.IsActionJustPressed("interact"))
                {
                    interactable.Interact();
                }
            }
        }
    }
}