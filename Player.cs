using Godot;
using System;

public class Player : KinematicBody2D

{
    [Export] public float Speed = 200.0f;
    private Vector2 _velocity = Vector2.Zero;
    public int maxHP = 100;
    public int currentHP = 100;
    public float attackRange = 80f;
    public int attackDmg = 10;
    public float interactRange = 40.0f;

    public float AttackCooldown = 2.0f;
    private float AttackTimer = 0f;

    private TextureProgress hpBar;

    public override void _Ready()
    {
        hpBar = GetTree().Root
        .GetNode<TextureProgress>("World/CanvasLayer/GameUI/HealthBar");
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 input = Vector2.Zero;
        
        AttackTimer -= delta;
        // Используем стандартные стрелочки Godot
        input.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        input.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

        // Нормализуем, чтобы по диагонали не бегал быстрее
        _velocity = input.Normalized() * Speed;

        // Двигаем тело
        _velocity = MoveAndSlide(_velocity);

        if (Input.IsActionJustPressed("interact"))
            {
                TryInteract();
            }
        if (Input.IsActionJustPressed("attack") && AttackTimer <= 0)
        {
          TryAttack();
          AttackTimer = AttackCooldown;
        }
    }
    public void takeDmg(int amount)
    {
        currentHP -= amount;
        hpBar.Value = currentHP;
        GD.Print("HP:" + currentHP);
        if (currentHP <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        GD.Print("You died");
    }
    public void TryAttack()
    {
        var enemies = GetTree().GetNodesInGroup("enemies");

        foreach (Node enemyNode in enemies)
        {
            Enemy enemy = enemyNode as Enemy;
            if (enemy == null)
                continue;

            float distance = GlobalPosition.DistanceTo(enemy.GlobalPosition);
            if (distance <= attackRange)
            {
                enemy.takeDmg(attackDmg);
                GD.Print("Enemy is attacked for: " + attackDmg);
                break;
            }
        }
    }
    public void TryInteract()
    {
        var items = GetTree().GetNodesInGroup("items");

        foreach (Node itemNode in items)
        {
            Item item = itemNode as Item;
            if (item == null)
            continue;

            float distance = GlobalPosition.DistanceTo(item.GlobalPosition);
            if (distance <= interactRange && item.canPickUp)
            {
                GD.Print("You picked up a new item:" + item.ItemName);
                item.QueueFree();
                break;
            }
        }
    }
}
