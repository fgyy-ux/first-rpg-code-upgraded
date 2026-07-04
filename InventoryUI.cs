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

        GridContainer = GetNode<Control>("/root/World/CanvasLayer/CanvasLayer/InventoryUI");

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
           object rawInstance = SlotPreFab.Instance();
           GD.Print($"[DEBUG] Godot returned instance of type: {rawInstance.GetType()}");
           InventorySlot slotInstance = rawInstance as InventorySlot;
           if (slotInstance == null)
           {
               GD.PrintErr("Failed to cast the instance to InventorySlot.");
               return;
           }
        }
    }
}