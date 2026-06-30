using Godot;

public partial class InventorySlot : Control
{
    public ItemData Item;
    public int Amount;
    public int SlotIndex;
    public Inventory PlayerInventory;

    public bool IsEmpty => Item == null || Amount <= 0;
}
