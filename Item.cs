using Godot;
using System;
using System.Net;

public class Item : Area2D
{
    [Export] public string ItemName = "Coin";
    [Export] public bool canPickUp = true;
    [Export] public float interactRange = 40.0f;

    public override void _Ready()
    {
        AddToGroup("items");
    }
    public void TryInteract(Player player)
    {
        if (player == null) return;
        {
            if (canPickUp == true)
            {
                float distance = GlobalPosition.DistanceTo(player.GlobalPosition);
                if (distance <= interactRange)
                {
                    canPickUp = true;
                    GD.Print("You picked up a new item: " + ItemName);
                    QueueFree();
                }
            }
        }
    }
}

