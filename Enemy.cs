using Godot;
using System;

public class Enemy : KinematicBody2D

{
    [Export] public int maxHP = 50;
    private int currentHP;


    // Создаём противника, придаём значения здоровия и добавляем в группу противников
    public override void _Ready()
    {
        currentHP = maxHP;
        AddToGroup("enemies");
        GD.Print("Enemy spwned. HP:" + currentHP);
    }
    public void takeDmg (int amount)  // Даём противнику способность терять здоровие, когда он принимает урон
    {
        currentHP = currentHP - amount;
        GD.Print("Enemy HP:" + currentHP);
        if (currentHP <= 0);
        {
            Die();
        }
    }
    // Создаём функцию смерти и удаление противника со сцены после смерти (безопасно удаляется, без вреда к окружению, с помощью QueueFree()
    private void Die()
    {
        GD.Print("Enemy died.");
        QueueFree();
    }
    
}
