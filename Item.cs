using Godot;
using System;
using System.Net;

public class Item : Area2D
{
    [Export] public string ItemName = "Coin";
    [Export] public bool canPickUp = true;
    [Export] public float interactRange = 40.0f;
    [Export] public int healAmount = 0;

    public void TryInteract(Player player)
    {
        if (player == null)
        {
            if (canPickUp == false)
            {
                float distance = GlobalPosition.DistanceTo(player.GlobalPosition);
                if (interactRange <= 40.0)
                {
                    canPickUp = true;
                    GD.Print("You picked up a new item: Coin" + ItemName);
                    QueueFree();
                }
            }
        }
    }
}

