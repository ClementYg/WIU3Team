using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] InventoryDisplay invDisplay;
    [SerializeField] ToolbarDisplay tlbDisplay;

    [Header("Modifiers")]
    [SerializeField] int maxItems = 36;

    public readonly List<ItemInstance> items = new();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public bool AddItem(ItemInstance item)
    {
        if (items.Count >= maxItems) return false;

        items.Add(item);
        tlbDisplay.AddItem(item);

        return true;
    }

    public void RemoveItem(int index)
    {
        items.RemoveAt(index);
    }

    public void RemoveItem(string itemName)
    {
        for (int i = 0; i < items.Count; ++i)
        {
            if (items[i].itemData.itemName == itemName)
            {
                items.RemoveAt(i);
                tlbDisplay.RemoveItem(itemName);
                return;
            }
        }
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
