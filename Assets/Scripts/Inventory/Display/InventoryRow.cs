using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryRow : MonoBehaviour
{
    // Keep a list of all the slots in the row
    [Header("Inventory Slots")]
    public List<InventorySlot> slots = new();

    [Header("Testing")]
    [SerializeField] bool isCapacityFullAtStart = false;
    [SerializeField] bool isDisplayedAtStart = true;

    bool isDisplaying = true;

    // Used to track the current capacity
    public readonly int maxRowCapacity = 12;
    
    public int CurrRowCapacity
    {
        get
        {
            int capacity = 0;

            foreach (InventorySlot slot in slots)
            {
                if (slot.isOccupied) ++capacity;
            }

            return capacity;
        }
    }

    public bool IsCapacityFull => (CurrRowCapacity >= maxRowCapacity);

    private void Awake()
    {
        if (isCapacityFullAtStart)
        {
            foreach (InventorySlot slot in slots)
            {
                slot.isOccupied = true;
            }
        }

        isDisplaying = isDisplayedAtStart;
        if (!isDisplaying)
        {
            gameObject.SetActive(false);
        }
    }

    public void AddItem(ItemInstance item)
    {
        for (int i = 0; i < slots.Count; ++i)
        {
            if (slots[i].isOccupied && slots[i].itemName == item.itemData.itemName)
            {
                // Stack the item, just increment the quantity
                InventorySlot stackedSlot = slots[i];

                ++stackedSlot.itemQuantity;
                stackedSlot.quantityText.text = stackedSlot.itemQuantity.ToString();

                slots[i] = stackedSlot;

                return;
            }
            else if (!slots[i].isOccupied)
            {
                // Display the new item at this unoccupied slot
                InventorySlot newSlot = slots[i];

                newSlot.itemImage.sprite = item.itemData.itemImage;
                newSlot.itemImage.enabled = true;
                newSlot.quantityText.text = "1";
                newSlot.quantityText.enabled = true;

                newSlot.itemName = item.itemData.itemName;
                newSlot.itemQuantity = 1;
                newSlot.isOccupied = true;

                slots[i] = newSlot;

                return;
            }
        }
    }

    public void RemoveItem(int itemIndex)
    {
        InventorySlot slot = slots[itemIndex];

        --slot.itemQuantity;
        if (slot.itemQuantity <= 0)
        {
            // No more of this item, unoccupy the slot
            slot.itemName = null;
            slot.itemImage.enabled = false;
            slot.quantityText.enabled = false;
            slot.isOccupied = false;
        }
        else
        {
            // Just update the text of the slot to display the new quantity
            slot.quantityText.text = slot.itemQuantity.ToString();
        }

        slots[itemIndex] = slot;
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
