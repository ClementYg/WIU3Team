using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] InventoryDisplay invDisplay;
    [SerializeField] ToolbarDisplay tlbDisplay;

    [Header("Modifiers")]
    [SerializeField] int maxItems = 36;

    public List<ItemInstance> items = new();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (items.Count > 0)
        {
            foreach (ItemInstance item in items)
            {
                // Update the displays
                if (tlbDisplay.CheckIsCapacityFull() == false)
                {
                    tlbDisplay.AddItem(item);
                }
                else if (invDisplay.CheckIsCapacityFull() == false)
                {
                    invDisplay.AddItem(item);
                }
            }
        }
    }

    public bool AddItem(ItemInstance item)
    {
        if (items.Count >= maxItems)
        {
            Debug.Log("Inventory: Max capacity reeached.");
            return false;
        }

        items.Add(item);
        
        // Update the displays
        if (tlbDisplay.CheckIsCapacityFull() == false)
        {
            tlbDisplay.AddItem(item);
        }
        else if (invDisplay.CheckIsCapacityFull() == false)
        {
            invDisplay.AddItem(item);
        }

        return true;
    }

    public void RemoveItem(string itemName)
    {
        for (int i = 0; i < items.Count; ++i)
        {
            if (items[i].itemData.itemName == itemName)
            {
                items.RemoveAt(i);
                
                // Update the displays
                if (tlbDisplay.RemoveItem(itemName) == false)
                {
                    invDisplay.RemoveItem(itemName);
                }

                return;
            }
        }
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
}
