using UnityEngine;
using System.Collections.Generic;

public class InventoryRow : MonoBehaviour
{
    // Keep a list of all the slots in the row
    [Header("Inventory Slots")]
    public List<InventorySlot> slots = new();

    // Used to track the current capacity
    public readonly int maxRowCapacity = 12;

    public int CurrRowCapacity
    {
        get
        {
            int capacity = 0;

            foreach (InventorySlot slot in slots)
            {
                if (slot.IsOccupied) ++capacity;
            }

            return capacity;
        }
    }

    public bool IsCapacityFull => (CurrRowCapacity >= maxRowCapacity);

    public void AddItem(ItemInstance item)
    {
        int amountToAdd = item.stackCount;
        if (amountToAdd <= 0) return;
        
        if (item.itemData.isStackable)
        {
            // Look for a possible stack
            for (int i = 0; i < slots.Count; ++i)
            {
                if (slots[i].IsOccupied &&
                    slots[i].itemDisplayed.itemData.itemName == item.itemData.itemName)
                {
                    slots[i].AddToStack(ref amountToAdd);
                }
            }

            // Create new stacks for remaining items
            while (amountToAdd > 0)
            {
                InventorySlot newSlot = null;

                for (int i = 0; i < slots.Count; ++i)
                {
                    if (!slots[i].IsOccupied)
                    {
                        newSlot = slots[i];
                        break;
                    }
                }

                if (newSlot == null)
                {
                    Debug.LogWarning(
                        $"Inventory full. Remaining: {amountToAdd}"
                    );

                    return;
                }

                newSlot.SetSlot(item);

                int stackAmount = Mathf.Min(amountToAdd, item.itemData.maxStackSize);
                amountToAdd -= stackAmount;
            }
        }
        else
        {
            // This item is not stackable, so look for an unoccupied slot to fill
            for (int i = 0; i < slots.Count; ++i)
            {
                if (slots[i].IsOccupied) continue; // Go past the occupied slots

                InventorySlot newSlot = slots[i];
                newSlot.SetSlot(item);

                return;
            }

            Debug.LogWarning("Inventory full. Could not add non-stackable item.");
        }
    }

    public void RemoveItem(int slotIndex)
    {
        InventorySlot toRemove = slots[slotIndex];

        --toRemove.itemDisplayed.stackCount;
        if (toRemove.itemDisplayed.stackCount <= 0)
        {
            EmptySlot(ref toRemove);
        }
        else
        {
            // Just update the text of the slot to display the new quantity
            toRemove.UI.quantityText.text = toRemove.itemDisplayed.stackCount.ToString();
        }

        slots[slotIndex] = toRemove;
    }

    public void EmptySlot(ref InventorySlot toEmpty)
    {
        // No more of this item, unoccupy the slot
        // Empty the reference
        toEmpty.itemDisplayed = null;

        // Empty the UI
        SlotUI toEmptyUI = toEmpty.UI;
        toEmptyUI.itemImage.enabled = false;
        toEmptyUI.quantityText.enabled = false;
    }

#if UNITY_EDITOR
    [ContextMenu("Find All Inventory Slots")]
    private void FindAllInventorySlots()
    {
        slots.Clear();

        Transform slotParentTransform = transform.Find("Inventory Slots");
        for (int i = 0; i < slotParentTransform.childCount; ++i)
        {
            Transform slotTransform = slotParentTransform.GetChild(i);
            InventorySlot newSlot = slotTransform.GetComponent<InventorySlot>();
            slots.Add(newSlot);
        }
    }
#endif
}
