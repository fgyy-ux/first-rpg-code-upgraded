public class InventorySlot
{
    public ItemData Item;
    public int Amount;

    public bool IsEmpty => Item == null || Amount <= 0;
}
