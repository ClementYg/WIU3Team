using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Inventory : PersistentSingleton<Inventory>
{
    [Header("Inventory")]
    [SerializeField] int maxStackCapacity = 36;

    [Header("UI")]
    [SerializeField] InventoryUI invUI;

    [Header("Testing")]
    [SerializeField] List<StartItem> startItems = new();

    List<ItemInstance> inventoryItems = new();
    public int currInvCapacity => inventoryItems.Count;
    public bool IsInventoryFull => (currInvCapacity >= maxStackCapacity);

    public UnityEvent onInventoryFull;
    public UnityEvent onInventoryFreed;

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
            onInventoryFull?.Invoke();
            Debug.Log("Inventory: Max capacity reached.");
            return false;
        }

        if (invUI.AddItem(item) == false)
        {
            Debug.Log("Inventory: UI could not display item.");
            return false;
        }

        inventoryItems.Add(item);

        return true;
    }

    public bool UseSelectedItem(GameObject user, int durabilityDamage = 0)
    {
        if (!TryGetSelectedOccupiedSlot(out InventorySlot selectedSlot)) return false;
        
        ItemInstance selectedItem = selectedSlot.itemDisplayed;

        // Use the item
        if (selectedItem.itemEffect != null)
        {
            selectedItem.itemEffect.Use(user);
        }
        else
        {
            Debug.LogWarning("Inventory: Failed to use selected item.");
            return false;
        }

        // Update inventory and UI
        bool shouldReduceStack = false;
        
        if (selectedItem.itemData.hasDurability)
        {
            selectedItem.TakeDurabilityDamage(durabilityDamage);

            if (selectedItem.isBroken && selectedItem.itemData.isStackable)
            {
                shouldReduceStack = true;
            }
        }
        else if (selectedItem.itemData.isStackable)
        {
            shouldReduceStack = true;
        }
        else
        {
            RemoveItem(selectedItem);
        }

        if (shouldReduceStack)
        {
            --selectedItem.stackCount;
            if (selectedItem.stackCount <= 0)
            {
                // Item has been used up, remove it
                RemoveItem(selectedItem);
            }
            else
            {
                // Update the quantity text
                invUI.UpdateSlotUI(selectedSlot.UI, selectedItem.stackCount.ToString());
            }
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
        foreach (StartItem item in startItems)
        {
            for (int i = 0; i < item.numberToAdd; ++i)
            {
                AddItem(item.instance);
                Debug.Log("add item called");
            }
        }

        Debug.Log("current capacity: " + currInvCapacity);
    }

    private void RemoveItem(ItemInstance item)
    {
        // Add that players cannot delete key items (Do a check!)

        bool wasInventoryFull = IsInventoryFull;

        inventoryItems.Remove(item);
        invUI.RemoveItem(item);

        if (wasInventoryFull && !IsInventoryFull)
        {
            onInventoryFreed?.Invoke();
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
