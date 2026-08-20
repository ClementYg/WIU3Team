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

    [Header("Testing")]
    [SerializeField] List<ItemInstance> itemsToAddAtStart = new();

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
        foreach (ItemInstance item in itemsToAddAtStart)
        {
            items.Add(item);
        }

        // Initialise UI
        invUI.InitUI(items);
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

        //FB: I cant lie i cant really see if the item stacking is implemented here? It sort of works in game but it might be display thing too
        //Just need to add this section at the top so that itll add to the stack, but doesnt split the stack yet. 
        //if (item.itemData.isStackable)
        //{
        //    //search thru entire inventory
        //    foreach (ItemInstance item_ in items)
        //    {
        //        if (item_.itemData == item.itemData && item_.stackCount < item_.itemData.maxStackSize)
        //        {
        //            item_.AddStack(item.stackCount, out int leftover);

        //            if (leftover <= 0)
        //            {
        //                //everything fit into the existing stack, nothing new to add
        //                return true;
        //            }

        //            item.stackCount = leftover;
        //        }    
        //    }

        //}

        return true;
    }

    public bool UseSelectedItem(GameObject user, int durabilityDamage = 0)
    {
        string selectedItemName = invUI.GetSelectedItemName();
        if (selectedItemName == null) return false;

        ItemInstance item = GetItem(selectedItemName);
        if (item == null) return false;

        //FB: Can possibly add one more check for itemEffect == null
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

    private void RemoveItem(ItemInstance item)
    {
        // Add that players cannot delete key items (Do a check!)

        bool wasItemFull = IsFull;

        items.Remove(item);
        invUI.RemoveItem(item);

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
