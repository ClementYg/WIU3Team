using UnityEngine;
using System.Collections.Generic;

public class InventoryRow : MonoBehaviour
{
    // Keep a list of all the slots in the row
    [Header("Inventory Slots")]
    public List<InventorySlot> slots = new();

    [HideInInspector] public int rowID;

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

    private void Awake()
    {
        // Assign an ID to each slot
        for (int i = 0; i < slots.Count; ++i)
        {
            slots[i].slotID = i;
        }
    }

    public void AddItem(ItemInstance item)
    {
        int itemsToAdd = item.stackCount;

        if (item.itemData.isStackable)
        {
            for (int i = 0; i < slots.Count && itemsToAdd > 0; ++i)
            {
                if (slots[i].IsOccupied &&
                    slots[i].itemDisplayed.itemData.isStackable &&
                    slots[i].itemDisplayed.itemData.itemName == item.itemData.itemName)
                {
                    InventorySlot stackedSlot = slots[i];
                    stackedSlot.itemDisplayed.AddStack(itemsToAdd, out int excess);
                    stackedSlot.UI.quantityText.text =stackedSlot.itemDisplayed.stackCount.ToString();
                    slots[i] = stackedSlot;

                    itemsToAdd = excess;
                }
            }

            // Create new stacks for remaining items
            while (itemsToAdd > 0)
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
                        $"Inventory full. Remaining: {itemsToAdd}"
                    );

                    return;
                }

                int stackAmount = Mathf.Min(itemsToAdd, item.itemData.maxStackSize);
                newSlot.itemDisplayed =
                    new ItemInstance(item.itemData, item.itemEffect)
                    {
                        currentDurability = item.currentDurability,
                        stackCount = stackAmount
                    };

                newSlot.UI.itemImage.sprite = item.itemData.itemImage;
                newSlot.UI.itemImage.enabled = true;
                newSlot.UI.quantityText.text = stackAmount.ToString();
                newSlot.UI.quantityText.enabled = true;

                itemsToAdd -= stackAmount;
            }

            return;
        }
        for (int i = 0; i < slots.Count; ++i)
        {
            if (slots[i].IsOccupied) continue;

            InventorySlot newSlot = slots[i];

            newSlot.itemDisplayed = new ItemInstance(item.itemData, item.itemEffect)
                {
                    currentDurability = item.currentDurability,
                    stackCount = 1
                };

            newSlot.UI.itemImage.sprite = item.itemData.itemImage;
            newSlot.UI.itemImage.enabled = true;
            newSlot.UI.quantityText.text = "1";
            newSlot.UI.quantityText.enabled = true;

            slots[i] = newSlot;

            return;
        }

        Debug.LogWarning("Inventory full. Could not add non-stackable item.");
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
