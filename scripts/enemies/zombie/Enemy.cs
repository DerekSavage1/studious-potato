using Godot;
public partial class Enemy : CharacterBody3D
{
    private const float SPEED = 4.0f;
    private const float ATTACK_RANGE = 1f;
    private const float DAMAGE = 2.0f;
    private Vector3 velocity;

    Player player;
    [Export] public NodePath playerPath;
    [Export] public AnimationTree animTree;
    [Export] public CollisionShape3D collisionShape;
    [Export] public AudioStreamPlayer3D deathSound;
    [Export] public AudioStreamPlayer3D attackHitSound;
    [Export] public AudioStreamPlayer3D swingSound;


    [Export] public NavigationAgent3D navAgent;
    private float hp = 30;

    public override void _Ready()
    {
        player = GetNode<Player>(playerPath);
        velocity = Vector3.Zero;
    }

    public void HitPlayer()
    {  
    
        if(!targetInRange())
        {
            return;
        }
        var dir = GlobalPosition.DirectionTo(player.GlobalPosition);
        player.Hit(DAMAGE, dir);
        
        attackHitSound.Play();
    }

    public void Hit(float damage)
    {
        // Stop all current animations first
        var playback = (AnimationNodeStateMachinePlayback)animTree.Get("parameters/playback");

        GD.Print(hp);
        hp -= damage;
        if(hp <= 0)
        {
            // Then force the dead state
            deathSound.Play();
            playback.Travel("death");
            animTree.Set("parameters/conditions/Death", true);
        } else
        {
            animTree.Set("parameters/conditions/Hit", true);
            playback.Next();
        }

    }

    public void Swing()
    {
        swingSound.Play();
    }

    public override void _PhysicsProcess(double delta)
    {
        var stateMachine = animTree.Get("parameters/playback").As<AnimationNodeStateMachinePlayback>();
        string state = stateMachine.GetCurrentNode().ToString();

        Vector3 vec = new Vector3(player.GlobalPosition.X, GlobalPosition.Y, player.GlobalPosition.Z);

        switch(state)
        {
            case "run":
                run(); 
                animTree.Set("parameters/conditions/Attack", targetInRange());
                LookAt(vec, Vector3.Up);
            break;
            case "idle":
                animTree.Set("parameters/conditions/Run", true);
            break;
            case "hit":
                animTree.Set("parameters/conditions/Hit", false);
                LookAt(vec, Vector3.Up);
            break;
            case "death":
                collisionShape.Disabled = true;
            break;
            case "attack":
                animTree.Set("parameters/conditions/Run", !targetInRange());
                LookAt(vec, Vector3.Up);
            break;

            default:
            break;
        }

    }

    private void run()
    {
        navAgent.Set("target_position", player.GlobalPosition);
        Vector3 nextNavPoint = navAgent.GetNextPathPosition();
        Velocity = (nextNavPoint - GlobalPosition).Normalized() * SPEED;
        MoveAndSlide();
    }

    private bool targetInRange()
    {
        return GlobalPosition.DistanceTo(player.GlobalPosition) < ATTACK_RANGE;
    }

}
