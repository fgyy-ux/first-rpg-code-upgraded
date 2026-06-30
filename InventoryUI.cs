using Godot;

public partial class InventoryUI : Control
{
    [Export] public Inventory PlayerInventory;
    [Export] public PackedScene SlotPreFab;

    public override void _Ready()
    {
        if (PlayerInventory == null)
        {
            GD.PrintErr("PlayerInventory is not assigned in InventoryUI.");
            return;
        }

        if (SlotPreFab == null)
        {
            GD.PrintErr("SlotPreFab is not assigned in InventoryUI.");
            return;
        }

        for (int i = 0; i < PlayerInventory.Size; i++)
        {
            var slotInstance = SlotPreFab.Instance() as InventorySlot;
            if (slotInstance == null)
            {
                GD.PrintErr("The assigned slot prefab does not contain an InventorySlot root node.");
                return;
            }

            slotInstance.SlotIndex = i;
            slotInstance.PlayerInventory = PlayerInventory;
            AddChild(slotInstance);
        }
    }
}