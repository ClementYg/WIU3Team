using UnityEngine;
using System.Collections.Generic;

public class InventoryRow : MonoBehaviour
{
    // Keep a list of all the slots in the row
    [Header("Inventory Slots")]
    public List<InventorySlot> slots = new();

    [Header("Testing")]
    [SerializeField] bool isCapacityFullAtStart = false;
    [SerializeField] bool isDisplayedAtStart = true;

    [HideInInspector] public int rowID;

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
                if (slot.IsOccupied) ++capacity;
                Debug.Log("is occupied: " + slot.IsOccupied);
            }

            Debug.Log("current capacity: " + capacity);
            return capacity;
        }
    }

    public bool IsCapacityFull => (CurrRowCapacity >= maxRowCapacity);

    private void Awake()
    {
        //if (isCapacityFullAtStart)
        //{
        //    foreach (InventorySlot slot in slots)
        //    {
        //        slot.IsOccupied = true;
        //    }
        //}

        // Assign an ID to each slot
        for (int i = 0; i < slots.Count; ++i)
        {
            slots[i].slotID = i;
        }

        // Update visibility at start
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
            if (slots[i].IsOccupied &&
                slots[i].itemDisplayed.itemData.itemName == item.itemData.itemName)
            {
                // Stack the item, just increment the quantity
                InventorySlot stackedSlot = slots[i];

                ++stackedSlot.itemDisplayed.stackCount;
                stackedSlot.UI.quantityText.text = stackedSlot.itemDisplayed.stackCount.ToString();

                slots[i] = stackedSlot;

                return;
            }
            else if (!slots[i].IsOccupied)
            {
                // Display the new item at this unoccupied slot
                InventorySlot newSlot = slots[i];
                SlotUI newSlotUI = newSlot.UI;

                newSlotUI.itemImage.sprite = item.itemData.itemImage;
                newSlotUI.itemImage.enabled = true;
                newSlotUI.quantityText.text = "1";
                newSlotUI.quantityText.enabled = true;

                newSlot.itemDisplayed = new(item.itemData, item.itemEffect)
                {
                    currentDurability = item.currentDurability,
                    stackCount = 1
                };

                slots[i] = newSlot;

                return;
            }
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
