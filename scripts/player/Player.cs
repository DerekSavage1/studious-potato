using Godot;
using System;
using System.ComponentModel;

/*
   C#: 	Classes, export variables and methods use PascalCase, 
		private fields use _camelCase,
		local variables and parameters use camelCase (See C# style guide).
*/
public partial class Player : CharacterBody3D
{

	[Export] public NodePath hpLabelPath;
	private Label hpLabel;
	private const float Speed = 3.0f;
	private const float JumpVelocity = 4.5f;

	private float hp = 10f;
	private const float PUSHBACK = 8.0f;
	private Vector3 velocity;

	

    public override void _Ready()
    {


		hpLabel = GetNode<Label>(hpLabelPath);

		GD.Print(hpLabel);
    }



	public override void _PhysicsProcess(double delta)
	{

		float Speed;
		if(Input.IsActionPressed("sprint")) {
			Speed = 5.0f;
		} else {
			Speed = 3f;
		}
		
		hpLabel.Text = "HP: " + hp;
		velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("left", "right", "forward", "backward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void Hit(float damage, Vector3 direction)
	{
		hp -= damage;
		velocity += direction * PUSHBACK;
	}
}
