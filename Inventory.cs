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
        if (item == null || amount <= 0)
        {
            return false;
        }

        if (item.Stackable)
        {
            foreach (InventorySlot slot in Slots)
            {
                if (!slot.IsEmpty)
                {
                    if (slot.Item.Id == item.Id)
                    {
                        if (slot.Amount < item.MaxStack)
                        {
                            int spaceLeft = item.MaxStack - slot.Amount;
                            int amountToAdd = Mathf.Min(spaceLeft, amount);

                            slot.Amount += amountToAdd;
                            amount -= amountToAdd;

                            if (amount <= 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }

        foreach (InventorySlot slot in Slots)
        {
            if (slot.IsEmpty)
            {
                slot.Item = item;
                slot.Amount = amount;
                return true;
            }
        }

        return false;
    }
}
