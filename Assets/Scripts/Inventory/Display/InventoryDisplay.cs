using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InventoryDisplay : ItemDisplay
{
    // Keep a list of inventory rows
    [SerializeField] List<InventoryRow> rows = new();

    bool isDisplaying = false;

    // Update is called once per frame
    void Update()
    {
        bool isToggleDisplayPressed = InputSystem.actions["ToggleInventory"].WasPressedThisFrame();
        if (isToggleDisplayPressed)
        {
            isDisplaying = !isDisplaying;

            foreach (InventoryRow row in rows)
            {
                row.gameObject.SetActive(isDisplaying);
            }
        }
    }

    public override bool TryAddItem(ItemInstance item)
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

    public override bool TryRemoveItem(string itemName)
    {
        // Check if the item exists in any of the rows
        foreach (InventoryRow row in rows)
        {
            for (int i = 0; i < row.slots.Count; ++i)
            {
                if (row.slots[i].isOccupied && row.slots[i].itemName == itemName)
                {
                    row.RemoveItem(i);
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
}
