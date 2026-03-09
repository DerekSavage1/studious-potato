using Godot;
using Godot.Collections;

[Tool]

public partial class Button : StaticBody3D, Interactive
{
	
    [Export]
    public Godot.Collections.Array<Node> Targets { get; set; }

    public void _func_godot_apply_properties(Dictionary entity_properties)
    {
        // if (!entity_properties.ContainsKey("target"))
        //     return;

        // string targetName = (string)entity_properties["target"];

        // Targets = new Array<Node>();

        // foreach (Node node in GetTree().GetNodesInGroup(targetName))
        // {
        //     Targets.Add(node);
        // }
    }

    public void Interact()
    {
        foreach (var target in Targets)
        {
            if (target is Reactive receiver)
                receiver.OnInteract();
        }
    }
}
