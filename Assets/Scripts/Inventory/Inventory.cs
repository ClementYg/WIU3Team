using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [Header("Inventory")]
    public List<ItemInstance> items = new();
    [SerializeField] int maxCapacity = 36;

    [Header("UI")]
    [SerializeField] InventoryUI invUI;

    [Header("Event Channels")]
    [SerializeField] EventInventorySlot OnInventoryClickEvent;

    [Header("Testing")]
    [SerializeField] int numberOfItemsAtStart = 0;

    public bool IsFull => (items.Count >= maxCapacity);

    public UnityEvent onInventoryFull;
    public UnityEvent onInventoryFreed;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Default initialise the number of items in the inventory at the start of the game
        if (numberOfItemsAtStart > 0)
        {
            for (int i = 0; i < numberOfItemsAtStart; ++i)
            {
                items.Add(new ItemInstance(ScriptableObject.CreateInstance<ItemData>()));
            }
        }

        // Initialise UI
        invUI.InitUI(items);

        // Subscribe to event channels
        OnInventoryClickEvent.Subscribe(TryLiftPlaceItem);
    }

    public bool AddItem(ItemInstance item)
    {
        if (IsFull)
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

        items.Add(item);

        return true;
    }

    public bool UseSelectedItem(GameObject user, int durabilityDamage = 0)
    {
        string selectedItemName = invUI.GetSelectedItemName();
        if (selectedItemName == null) return false;

        ItemInstance item = GetItem(selectedItemName);
        if (item == null) return false;

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
        foreach (ItemInstance item in items)
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

        foreach (ItemInstance item in items)
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
        foreach (ItemInstance item in items)
        {
            Debug.Log($"Item Name: {item.itemData.itemName}");
        }
    }

    public ItemInstance GetItem(int index)
    {
        if (items.Count <= 0) return null;
        return items[index];
    }

    public ItemInstance GetItem(string itemName)
    {
        if (items.Count <= 0) return null;

        for (int i = 0; i < items.Count; ++i)
        {
            if (items[i].itemData.itemName == itemName)
            {
                return items[i];
            }
        }

        return null;
    }

    private void TryLiftPlaceItem(InventorySlot slotClicked)
    {
        invUI.RemoveStack(slotClicked.itemName);
    }

    private void RemoveItem(ItemInstance item)
    {
        // Add that players cannot delete key items (Do a check!)

        bool wasItemFull = IsFull;

        items.Remove(item);
        invUI.RemoveItem(item.itemData.itemName);

        if (wasItemFull && !IsFull)
        {
            onInventoryFreed?.Invoke();
        }
    }

    private void RemoveItem(string itemName)
    {
        RemoveItem(GetItem(itemName));
    }
}
