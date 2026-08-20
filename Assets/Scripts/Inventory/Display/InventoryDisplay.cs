using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InventoryDisplay : RowDisplay
{
    // Keep a list of inventory rows
    [Header("Inventory Rows")]
    public List<InventoryRow> rows = new();

    bool isDisplaying = false;

    // Update is called once per frame
    void Update()
    {
        bool isToggleDisplayPressed = InputSystem.actions["ToggleInventory"].WasPressedThisFrame();
        if (isToggleDisplayPressed)
        {
            ToggleInventoryDisplay();
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
        // Check if the item exists in any of the rows and remove it if yes
        foreach (InventoryRow row in rows)
        {
            for (int i = 0; i < row.slots.Count; ++i)
            {
                if (row.slots[i].IsOccupied &&
                    row.slots[i].itemDisplayed.itemData.itemName == itemName)
                {
                    row.RemoveItem(i);
                    return true;
                }
            }
        }

        return false;
    }

    public override bool TryRemoveStack(string itemName)
    {
        foreach (InventoryRow row in rows)
        {
            for (int i = 0; i < row.slots.Count; ++i)
            {
                if (row.slots[i].IsOccupied &&
                    row.slots[i].itemDisplayed.itemData.itemName == itemName)
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

    private void ToggleInventoryDisplay()
    {
        // Toggle the inventory display
        isDisplaying = !isDisplaying;

        foreach (InventoryRow row in rows)
        {
            row.gameObject.SetActive(isDisplaying);
        }

        // Toggle input action maps
        InputActionMap playerMap = InputSystem.actions.FindActionMap("Player");
        InputActionMap UIMap = InputSystem.actions.FindActionMap("UI");

        if (isDisplaying)
        {
            playerMap.Disable();
            UIMap.Enable();
        }
        else
        {
            playerMap.Enable();
            UIMap.Disable();
        }
    }
}
