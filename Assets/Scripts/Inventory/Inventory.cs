using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] InventoryUI invUI;

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
        invUI.InitUI(items);
    }

    public bool AddItem(ItemInstance item)
    {
        if (items.Count >= maxItems)
        {
            Debug.Log("Inventory: Max capacity reeached.");
            return false;
        }

        items.Add(item);
        invUI.AddItem(item);

        return true;
    }

    public void RemoveItem(string itemName)
    {
        for (int i = 0; i < items.Count; ++i)
        {
            if (items[i].itemData.itemName == itemName)
            {
                ItemInstance item = items[i];

                items.RemoveAt(i);
                invUI.RemoveItem(item);

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
