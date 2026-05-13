using Godot;
using System;
using System.Collections.Generic;

public class ItemData : Resource
{
    public string Id = "";
    public string DisplayName = "";
    public bool Stackable = true;
    public int MaxStack = 99;
}

public class InventorySlot
{
    public ItemData Item;
    public int amount;
    public bool IsEmpty => Item == null || amount <= 0;
}

public class Inventory : Node
{
    public int Size = 20;
    public List<InventorySlot> Slots = new List<InventorySlot>();
}