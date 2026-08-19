using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InventoryDisplay : ItemDisplay
{
    // Keep a list of inventory rows
    [Header("Inventory Rows")]
    [SerializeField] List<InventoryRow> rows = new();

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
