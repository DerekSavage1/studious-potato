
using System.Runtime.CompilerServices;
using Godot;

public partial class ElevatorLight : MeshInstance3D
{

	[Export]
	public Color color {get; set;}
    StandardMaterial3D material;

    private bool isOn = false;
    public override void _Ready()
    {
        material = new StandardMaterial3D
        {
            AlbedoColor = color,
            EmissionEnabled = true,
            Emission = color,                 // this is the important part
            EmissionEnergyMultiplier = 10.0f,  // increase brightness
            EmissionOperator = BaseMaterial3D.EmissionOperatorEnum.Add
        };

    }

    public void illuminate()
    {
        GD.Print("AAAHHHHHHHHHHH");
        if(isOn)
        {
            MaterialOverride = null;
        } else
        {
            MaterialOverride = material;
        }

        isOn = !isOn;
    }

}