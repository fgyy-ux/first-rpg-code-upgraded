using Godot;

public class InventoryUI : Control
{
    public Inventory PlayerInventory;
    public PackedScene SlotPreFab;
    public Control GridContainer;

    public override void _Ready()
    {
        PlayerInventory = GetNode<Inventory>("/root/World/Player/Inventory");

        SlotPreFab = GD.Load<PackedScene>("res://InventorySlot.tscn");

        GridContainer = GetNode<Control>("/root/World/CanvasLayer/InventoryHUD/InventoryUI/PanelContainer/ScrollContainer/GridContainer");

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

        GD.Print($"[DEBUG] Initializing inventory UI with {PlayerInventory.Size} slots.");
        for (int i = 0; i < PlayerInventory.Size; i++)
        {
            var slotInstance = SlotPreFab.Instance() as InventorySlot;

            if (slotInstance != null)
            {
                slotInstance.SlotIndex = i;
                   GridContainer.AddChild(slotInstance);
            }
            else
            {
                GD.PrintErr("Failed to instance InventorySlot from SlotPreFab.");
            }
        }
    }
}