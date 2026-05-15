using Godot;

public class Item : Area2D
{
    [Export] public ItemData Data;
    [Export] public int Amount = 1;
    [Export] public bool canPickUp = true;
    [Export] public float interactRange = 40.0f;

    public override void _Ready()
    {
        AddToGroup("items");
    }
}
