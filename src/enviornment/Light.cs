

using Godot;
using Godot.Collections;

[Tool]
public partial class Light : Node3D, Reactive
{
	[Export]
	public Color color {get; set;}
    StandardMaterial3D material;

    MeshInstance3D mesh;

    private bool isOn = false;

    public void _func_godot_apply_properties(Dictionary entity_properties)
    {
        if (entity_properties.ContainsKey("emission_color"))
            color = (Color)entity_properties["emission_color"];

        if (material != null)
            material.Emission = color;
    }

    public override void _Ready()
    {

        mesh = GetChild<MeshInstance3D>(0);

        material = new StandardMaterial3D
        {
            AlbedoColor = color,
            EmissionEnabled = true,
            Emission = color,                 // this is the important part
            EmissionEnergyMultiplier = 10.0f,  // increase brightness
            EmissionOperator = BaseMaterial3D.EmissionOperatorEnum.Add
        };

    }

    public void OnInteract()
    {
        Illuminate();
    }

    public void Illuminate()
    {
        if(isOn)
        {
            mesh.MaterialOverride = null;
        } else
        {
            mesh.MaterialOverride = material;
        }

        isOn = !isOn;
    }
}
