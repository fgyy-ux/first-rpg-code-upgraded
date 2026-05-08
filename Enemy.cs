using Godot;
using System;

public class Enemy : KinematicBody2D
{
    [Export] public int maxHP = 50;

    public float Speed = 100.0f;
    public float StopDistance = 5.0f;
    public float SightRange = 250.0f;
    public float SpotTimeChase = 7.0f;
    public float SpotDecaySpeed = 2.0f;
    public float TouchDistance = 50.0f;

    private int currentHP;
    private Player player;
    private Vector2 velocity = Vector2.Zero;
    private float spotMeter = 0.0f;
    private bool isChasing = false;

    public float AttackRange = 25.0f;
    public int AttackDmg = 20;
    public float AttackCooldown = 2.0f;
    private float AttackTimer = 0.0f;

    private ProgressBar hpBar;


    public override void _Ready()
    {
        hpBar = GetNode<ProgressBar>("HealthBar");
        hpBar.MaxValue = maxHP;
        hpBar.Value = currentHP;

        player = GetParent().GetNode<Player>("Player");
        currentHP = maxHP;
        AddToGroup("enemies");
        GD.Print("Enemy spawned. HP:" + currentHP);
    }

    public override void _PhysicsProcess(float delta)
    {
        if (player == null)
            return;
 
        float distanceToPlayer = GlobalPosition.DistanceTo(player.GlobalPosition);

        AttackTimer -= delta;
        
        if (distanceToPlayer <= SightRange)
        {
            spotMeter += delta;

            if (spotMeter >= SpotTimeChase)
            {
                isChasing = true;
            }
        }
        else
        {
            spotMeter -= delta * SpotDecaySpeed;

            if (spotMeter < 0)
            {
                spotMeter = 0;
            }
        }

        if (isChasing && distanceToPlayer > StopDistance)
        {
            Vector2 direction = GlobalPosition.DirectionTo(player.GlobalPosition);
            velocity = direction * Speed;
            velocity = MoveAndSlide(velocity);

            if (distanceToPlayer <= AttackRange && AttackTimer <= 0)
            {
                AttackPlayer();
                AttackTimer = AttackCooldown;
            }
        }
        else
        {
            velocity = Vector2.Zero;
        }
    }

    public void takeDmg(int amount)
    {
        currentHP = currentHP - amount;
        GD.Print("Enemy HP: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GD.Print("Enemy died.");
        QueueFree();
    }
    private void AttackPlayer()
    {
        if (player == null)
        return;

        float distance = GlobalPosition.DistanceTo(player.GlobalPosition);
        if (distance <= AttackRange)
        {
            player.takeDmg(AttackDmg);
            GD.Print("You got attacked: " + AttackDmg);
            if (player.currentHP <= 0)
            {
                GetTree().ReloadCurrentScene();
            }
        }
    }
}
