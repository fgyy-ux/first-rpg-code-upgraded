using Godot;
using System.Collections.Generic;

public class Inventory : Node
{
    [Export] public int Size = 20;

    public List<InventorySlot> Slots = new List<InventorySlot>();

    public override void _Ready()
    {
        for (int i = 0; i < Size; i++)
        {
            Slots.Add(new InventorySlot());
        }
    }

    public bool AddItem(ItemData item, int amount)
    {
        return false;
    }
}
