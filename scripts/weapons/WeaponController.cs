using Godot;
using System;

[GlobalClass]
public partial class WeaponController : Node3D
{
    [Export] public WeaponData CurrentWeapon;
    [Export] public Node3D WeaponModelParent;

    private Node3D currentWeaponModel;

    public override void _Ready()
    {
        UpdateWeaponModel();
    }

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
            UpdateWeaponModel();
    }

    private void UpdateWeaponModel()
    {
        if (currentWeaponModel != null)
        {
            currentWeaponModel.QueueFree();
            currentWeaponModel = null;
        }

        if (CurrentWeapon != null && CurrentWeapon.WeaponModel != null && WeaponModelParent != null)
        {
            currentWeaponModel = (Node3D)CurrentWeapon.WeaponModel.Instantiate();
            WeaponModelParent.AddChild(currentWeaponModel);

            currentWeaponModel.Position = CurrentWeapon.WeaponPosition;
            currentWeaponModel.Rotation = CurrentWeapon.WeaponRotation;
        }
    }
}