using Godot;

public class ItemData : Resource
{
    [Export] public string Id = "";
    [Export] public string DisplayName = "";
    [Export] public bool Stackable = true;
    [Export] public int MaxStack = 99;
}
