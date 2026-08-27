using UnityEngine;
using System.Collections.Generic;

public class InventoryDisplay : MonoBehaviour
{
    // Keep a list of inventory rows
    [Header("Inventory Rows")]
    public List<InventoryRow> rows = new();

    public bool AddItem(ItemInstance item)
    {
        foreach (InventoryRow row in rows)
        {
            if (row.IsCapacityFull == false)
            {
                row.AddItem(item);
                return true;
            }
        }

        return false;
    }

    public bool AddItemAtSlot(ItemInstance item, int rowIndex, int slotIndex)
    {
        if (rowIndex < 0 || rowIndex > rows.Count - 1) return false;

        InventoryRow row = rows[rowIndex];
        row.AddItemAtSlot(item, slotIndex);

        return true;
    }

    public bool RemoveItem(ItemInstance item)
    {
        // Check if the item exists in any of the rows and remove it if so
        foreach (InventoryRow row in rows)
        {
            for (int i = 0; i < row.slots.Count; ++i)
            {
                if (row.slots[i].IsOccupied &&
                    row.slots[i].itemDisplayed == item)
                {
                    row.RemoveItem(i);
                    return true;
                }
            }
        }

        return false;
    }

    public bool RemoveStack(ItemInstance itemName)
    {
        foreach (InventoryRow row in rows)
        {
            for (int i = 0; i < row.slots.Count; ++i)
            {
                if (row.slots[i].IsOccupied &&
                    row.slots[i].itemDisplayed == itemName)
                {
                    InventorySlot toEmpty = row.slots[i];
                    row.EmptySlot(ref toEmpty);
                    return true;
                }
            }
        }

        return false;
    }

    public bool CheckIsCapacityFull()
    {
        foreach (InventoryRow row in rows)
        {
            if (row.IsCapacityFull == false) return false;
        }

        return true;
    }

#if UNITY_EDITOR
    [ContextMenu("Find All Inventory Rows")]
    private void FindAllInventoryRows()
    {
        rows.Clear();

        Transform rowsTransform = transform.Find("Inventory Rows");
        for (int i = 0; i < rowsTransform.childCount; ++i)
        {
            Transform rowTransform = rowsTransform.GetChild(i);
            InventoryRow newRow = rowTransform.GetComponent<InventoryRow>();
            rows.Add(newRow);
        }
    }
#endif
}
