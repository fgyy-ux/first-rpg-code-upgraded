using Godot;

public class HealthBar : ProgressBar
{
    public Player player;
    public override void _Process(float delta)
    {
        if (player != null)
        {
            Value = player.currentHP;
        }
    }
}