using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Inventory : PersistentSingleton<Inventory>
{
    [Header("Inventory")]
    public List<ItemInstance> inventoryItems = new();
    [SerializeField] int maxCapacity = 36;

    [Header("UI")]
    [SerializeField] InventoryUI invUI;

    [Header("Testing")]
    [SerializeField] List<StartItem> startItems = new();

    public bool IsInventoryFull => (inventoryItems.Count >= maxCapacity);

    public UnityEvent onInventoryFull;
    public UnityEvent onInventoryFreed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        string selectedItemName = invUI.GetSelectedItemName();
        if (selectedItemName == null) return false;

        ItemInstance item = GetItem(selectedItemName);
        if (item == null) return false;

        if (item.itemEffect == null) return false;
        item.itemEffect.Use(user);

        bool shouldReduceStack = false;
        
        if (item.itemData.hasDurability)
        {
            item.TakeDurabilityDamage(durabilityDamage);

            if (item.isBroken && item.itemData.isStackable)
            {
                shouldReduceStack = true;
            }
        }
        else if (item.itemData.isStackable)
        {
            shouldReduceStack = true;
        }
        else
        {
            RemoveItem(item);
        }

        if (shouldReduceStack)
        {
            --item.stackCount;
            if (item.stackCount <= 0)
            {
                RemoveItem(item);
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
            }
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
            onInventoryFreed?.Invoke();
        }
    }

    private void RemoveItem(string itemName)
    {
        RemoveItem(GetItem(itemName));
    }
}
