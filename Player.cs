using Godot;
using System;

public class Player : KinematicBody2D

{
    [Export] public float Speed = 200.0f;
    private Vector2 _velocity = Vector2.Zero;
    public int maxHP = 100;
    public int currentHP = 100;
    public float attackRange = 50f;
    public int attackDmg = 10;
    public float interactRange = 40.0f;

    public override void _PhysicsProcess(float delta)
    {
        Vector2 input = Vector2.Zero;
        
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

    }
    public void takeDmg(int amount)
    {
        currentHP -= amount;
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
                GD.Print("Enemy is attacked for:" + attackDmg);
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
