using Godot;

public class InventorySlot : Panel
{
    public ItemData Item;
    public int Amount;
    public int SlotIndex;
    public Inventory PlayerInventory;

    public bool IsEmpty => Item == null || Amount <= 0;
}
