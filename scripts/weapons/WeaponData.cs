using Godot;
using System;

[GlobalClass]
public partial class WeaponData : Resource
{
    [Export] public string WeaponName { get; set; }
    [Export] public float Damage { get; set; }
    [Export] public PackedScene WeaponModel { get; set; }
    [Export] public Vector3 WeaponPosition { get; set; }

    [Export(PropertyHint.Range, "0,360,0.1,radians_as_degrees")]
    public float RotX { get; set; }

    [Export(PropertyHint.Range, "0,360,0.1,radians_as_degrees")]
    public float RotY { get; set; }

    [Export(PropertyHint.Range, "0,360,0.1,radians_as_degrees")]
    public float RotZ { get; set; }

    public Vector3 WeaponRotation => new Vector3(RotX, RotY, RotZ);

    public WeaponData()
    {
        WeaponName = "Name";
        Damage = 10f;
        WeaponModel = null;
        WeaponPosition = Vector3.Zero;
    }
}
