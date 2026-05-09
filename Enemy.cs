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

    public float AttackRange = 25.0f;
    public int AttackDmg = 20;
    public float AttackCooldown = 2.0f;

    private int currentHP;
    private Player player;
    private Vector2 velocity = Vector2.Zero;
    private float spotMeter = 0.0f;
    private bool isChasing = false;
    private float AttackTimer = 0.0f;
    private ProgressBar hpBar;

    public override void _Ready()
    {
        currentHP = maxHP;
        player = GetParent().GetNode<Player>("Player");
        hpBar = GetNode<ProgressBar>("HealthBar");

        hpBar.MaxValue = maxHP;
        hpBar.Value = currentHP;

        AddToGroup("enemies");
        GD.Print("Enemy spawned. HP:" + currentHP);
    }

    public override void _PhysicsProcess(float delta)
    {
        if (player == null)
            return;

        float distanceToPlayer = GlobalPosition.DistanceTo(player.GlobalPosition);

        AttackTimer -= delta;

        UpdateSpotMeter(distanceToPlayer, delta);
        MoveToPlayer(distanceToPlayer);
        TryAttackPlayer(distanceToPlayer);
    }

    private void UpdateSpotMeter(float distanceToPlayer, float delta)
    {
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
    }

    private void MoveToPlayer(float distanceToPlayer)
    {
        if (isChasing && distanceToPlayer > StopDistance)
        {
            Vector2 direction = GlobalPosition.DirectionTo(player.GlobalPosition);
            velocity = direction * Speed;
            velocity = MoveAndSlide(velocity);
        }
        else
        {
            velocity = Vector2.Zero;
        }
    }

    private void TryAttackPlayer(float distanceToPlayer)
    {
        if (isChasing && distanceToPlayer <= AttackRange && AttackTimer <= 0)
        {
            AttackPlayer();
            AttackTimer = AttackCooldown;
        }
    }

    public void takeDmg(int amount)
    {
        currentHP = Math.Max(currentHP - amount, 0);
        hpBar.Value = currentHP;

        GD.Print("Enemy HP: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void AttackPlayer()
    {
        if (player == null)
            return;

        player.takeDmg(AttackDmg);
        GD.Print("You got attacked: " + AttackDmg);

        if (player.currentHP <= 0)
        {
            GetTree().ReloadCurrentScene();
        }
    }

    private void Die()
    {
        GD.Print("Enemy died.");
        QueueFree();
    }
}
