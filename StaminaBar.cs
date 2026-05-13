using System;
using Godot;

public class StaminaBar : ProgressBar
{
    public Player player;
    public override void _Process(float delta)
    {
        if (player != null)
        {
            Value = player.currentStamina;
        }
    }
}