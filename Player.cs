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

    private ProgressBar hpBar;
    private ProgressBar staminaBar;
    public float maxStamina = 100.0f;
    public float currentStamina = 100.0f;
    public float attackStaminaCost = 40.0f;
    public float staminaRegen = 5.0f; 

    private Inventory inventory;

    public override void _Ready()
    {
        hpBar = GetTree().Root
        .GetNode<ProgressBar>("World/CanvasLayer/GameUI/HealthBar");

        staminaBar = GetTree().Root
        .GetNode<ProgressBar>("World/CanvasLayer/GameUI/StaminaBar");

        inventory = GetNode<Inventory>("Inventory");
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 input = Vector2.Zero;
        
        AttackTimer -= delta;
        currentStamina += staminaRegen * delta;
        
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }

        staminaBar.Value = currentStamina;

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
            if (currentStamina < attackStaminaCost)
            {
                GD.Print("Not enough stamina");
                return;
            }
            currentStamina -= attackStaminaCost;
            staminaBar.Value = currentStamina;

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
                if (inventory.AddItem(item.Data, item.Amount))
                {
                    GD.Print("Picked up: " + item.Data.DisplayName);
                    item.QueueFree();
                }
                else
                {
                    GD.Print("Inventory full.");
                }
            }
        }
    }
}
