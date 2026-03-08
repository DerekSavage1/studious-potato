using Godot;

public partial class PlayerRayCast : RayCast3D
{
    public override void _PhysicsProcess(double delta)
    {


        if (IsColliding())
        {

            var collider = GetCollider();

			if(collider is Node3D)
			{
				GD.Print("Colliding with %s", ((Node3D) collider).GetType().ToString());
			}
	

            if (collider is Interactable interactable)
            {

                if (Input.IsActionJustPressed("interact"))
                {
                    interactable.Interact();
                }
            }
        } else
		{
			GD.Print("Not Colliding");
		}
    }
}