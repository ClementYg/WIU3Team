using UnityEngine;
using System.Collections.Generic;

public class Inventory : PersistentSingleton<Inventory>
{
    [Header("Inventory")]
    [SerializeField] InventoryUI invUI;
    [SerializeField] int maxStackCapacity = 36;

    [Header("Event Channels")]
    [SerializeField] EventVoid OnInventoryFullEvent;
    [SerializeField] EventVoid OnInventoryFreedEvent;

    [Header("Testing")] // This is broken, avoid using it for now
    [SerializeField] List<ItemInstance> startItems = new();

    // Inventory
    List<ItemInstance> inventoryItems = new();
    public int currInvCapacity => inventoryItems.Count;
    public bool IsInventoryFull => (currInvCapacity >= maxStackCapacity);

    protected override void Awake()
    {
        base.Awake();

        // Setup testing environment
        SetupTests();

        // Initialise UI
        invUI.InitUI(inventoryItems);
    }

    public bool AddItem(ItemInstance item)
    {
        if (IsInventoryFull)
        {
            OnInventoryFullEvent.Raise();
            return false;
        }

        if (invUI.AddItem(item) == false)
        {
            return false;
        }

        inventoryItems.Add(item);

        return true;
    }

    public bool UseSelectedItem(GameObject user, ComponentCache userCache, int durabilityDamage = 0)
    {
        if (!TryGetSelectedOccupiedSlot(out InventorySlot selectedSlot)) return false;
        
        // Get and use the item
        ItemInstance selectedItem = selectedSlot.itemDisplayed;
        if (selectedItem.itemEffect != null)
        {
            selectedItem.itemEffect.Use(user, userCache);
        }
        else
        {
            Debug.LogWarning("Inventory: Failed to use selected item.");
            return false;
        }

        // Update inventory and UI
        if (selectedItem.itemData.hasDurability)
        {
            // Take durability damage
            selectedItem.TakeDurabilityDamage(durabilityDamage);
            if (selectedItem.IsBroken && selectedItem.itemData.isStackable)
            {
                selectedSlot.ReduceStack(1);
            }
        }
        else if (selectedItem.itemData.isConsumable)
        {
            // This item is either stackable or just a singular item, reduce the stack
            selectedSlot.ReduceStack(1);
        }

        // Remove item if it has been used up
        if (selectedItem.IsFinished)
        {
            RemoveItem(selectedItem);
        }

        return true;
    }

    public bool CheckItem(string itemName)
    {
        foreach (ItemInstance item in inventoryItems)
        {
            if (item.itemData.itemName == itemName)
            {
                return true;
            }
        }

        return false;
    }

    public int GetItemQuantity(string itemName)
    {
        int itemQuantity = 0;

        foreach (ItemInstance item in inventoryItems)
        {
            if (item.itemData.itemName == itemName)
            {
                itemQuantity += item.stackCount;
            }
        }

        return itemQuantity;
    }

    public void DisplayItems()
    {
        foreach (ItemInstance item in inventoryItems)
        {
            Debug.Log($"Item Name: {item.itemData.itemName}");
        }
    }

    public ItemInstance GetItem(int index)
    {
        if (inventoryItems.Count <= 0) return null;
        return inventoryItems[index];
    }

    public ItemInstance GetItem(string itemName)
    {
        if (inventoryItems.Count <= 0) return null;

        for (int i = 0; i < inventoryItems.Count; ++i)
        {
            if (inventoryItems[i].itemData.itemName == itemName)
            {
                return inventoryItems[i];
            }
        }

        return null;
    }

    private void SetupTests()
    {
        // Setup the initial testing environment for UI
        foreach (ItemInstance item in startItems)
        {
            AddItem(item);
        }
    }

    private void RemoveItem(ItemInstance item)
    {
        // Add that players cannot delete key items (Do a check!)

        bool wasInventoryFull = IsInventoryFull;

        inventoryItems.Remove(item);
        invUI.RemoveItem(item);

        if (wasInventoryFull && !IsInventoryFull)
        {
            OnInventoryFreedEvent?.Raise();
        }
    }

    private void RemoveItem(string itemName)
    {
        RemoveItem(GetItem(itemName));
    }

    private bool TryGetSelectedOccupiedSlot(out InventorySlot selectedSlot)
    {
        // Get the selected slot
        selectedSlot = invUI.GetSelectedSlot();
        if (selectedSlot != null)
        {
            // Check if the slot is occupied
            if (selectedSlot.IsOccupied)
            {
                return true;
            }
        }

        Debug.LogWarning("Inventory: Failed to get selected occupied slot.");

        selectedSlot = null;
        return false;
    }
}
