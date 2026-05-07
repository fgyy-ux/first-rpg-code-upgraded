using Godot;

public class HealthBar : TextureProgress
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