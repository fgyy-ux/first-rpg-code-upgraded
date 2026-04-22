using Godot;
using System;

public class Enemy : KinematicBody2D

{
    [Export] public int maxHP = 50;
    private int currentHP;


    // Создаём противника, придаём значения здоровия и догруппу противников
    public override void _Ready()
    {
        currentHP = maxHP;
        AddToGrup("enemies");
        GD.Print("Enemy spwned. HP:" + currentHP);
    }
}
