using Godot;
using System;

public class Player : KinematicBody2D

{
    [Export] public float Speed = 200.0f;
    private Vector2 _velocity = Vector2.Zero;
    public int maxHP = 100;
    public int currentHP = 100;


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
}