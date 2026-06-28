using Godot;

public partial class InventoryUI : Control
{
    [Export] public Inventory PlayerInventory;
    [Export] public PackedScene SlotPreFab;

    override public void _Ready()
    {
        if (PlayerInventory == null)
        {
            GD.PrintErr("PlayerInventory is not assigned in InventoryUI.");
            return;
        }

        for (int i = 0; i < PlayerInventory.Size; i++)
        {
            var slotInstance = SlotPreFab.Instantiate<InventoryUI>();
            slotInstance.SlotIndex = i;
            slotInstance.PlayerInventory = PlayerInventory;
            AddChild(slotInstance);
        }
    }
}