using Godot;




public partial class Enemy : CharacterBody3D
{
    private const float SPEED = 4.0f;
    private const float ATTACK_RANGE = 2.0f;
    private const float DAMAGE = 2.0f;
    private Vector3 velocity;

    Player player;
    [Export] public NodePath playerPath;
    [Export] public AnimationTree animTree;
    [Export] public CollisionShape3D collisionShape;
    private NavigationAgent3D navAgent;
    private float hp;

    public override void _Ready()
    {
        
        foreach(Node child in GetChildren())
        {
            if(child is NavigationAgent3D)
            {
                navAgent = (NavigationAgent3D) child;
                break;
            }
        }

        if(navAgent == null)
        {
            GD.PrintErr("Enemy says: No NavigationAgent3D Child! I need one!");
        }
        
        player = GetNode<Player>(playerPath);
        velocity = Vector3.Zero;

        animTree = GetNode<AnimationTree>("AnimationTree");
         
    }

    public void HitPlayer()
    {  
        
        if(!targetInRange())
        {
            GetNode<AudioStreamPlayer3D>("HitSwing").Play();
            return;
        }
        var dir = GlobalPosition.DirectionTo(player.GlobalPosition);
        player.Hit(DAMAGE, dir);
        
        GetNode<AudioStreamPlayer3D>("HitImpact").Play();
    }

    public override void _PhysicsProcess(double delta)
    {
        var stateMachine = animTree.Get("parameters/playback").As<AnimationNodeStateMachinePlayback>();
        string state = stateMachine.GetCurrentNode().ToString();

        Vector3 vec = new Vector3(player.GlobalPosition.X, GlobalPosition.Y, player.GlobalPosition.Z);
        LookAt(vec, Vector3.Up);

        GD.Print(state);
        switch(state)
        {
            case "run":
                run(); 
                animTree.Set("parameters/conditions/Attack", targetInRange());
            break;
            case "idle":
                animTree.Set("parameters/conditions/Run", true);
            break;
            case "hit":
                
            break;
            case "death":
            break;
            case "attack":
                animTree.Set("parameters/conditions/Run", !targetInRange());
                
            break;

            default:
            break;
        }

    }

    private void run()
    {
        navAgent.Set("target_position", player.GlobalPosition);
        Vector3 nextNavPoint = navAgent.GetNextPathPosition();
        GD.Print(nextNavPoint);
        Velocity = (nextNavPoint - GlobalPosition).Normalized() * SPEED;
        MoveAndSlide();
    }

    private bool targetInRange()
    {
        return GlobalPosition.DistanceTo(player.GlobalPosition) < ATTACK_RANGE;
    }

}
