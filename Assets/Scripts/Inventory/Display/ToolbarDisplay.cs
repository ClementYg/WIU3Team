using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Assertions.Must;

public class ToolbarDisplay : MonoBehaviour
{
    // Keep a list of all the keybinds that will be used to select a box in the toolbar
    readonly Key[] toolbarKeys =
    {
        Key.Digit1,
        Key.Digit2,
        Key.Digit3,
        Key.Digit4,
        Key.Digit5,
        Key.Digit6,
        Key.Digit7,
        Key.Digit8,
        Key.Digit9,
        Key.Digit0,
        Key.Minus,
        Key.Equals
    };

    // Keep a list of all the slots in the toolbar
    [SerializeField] List<InventorySlot> slots = new();

    // Store a reference to the red selection slot
    [SerializeField] Image redSelectionSlot;

    // To track the index of the selected slot
    int selectedSlotIndex = 0;

    // Used to track the current capacity
    int currSlotCapacity = 0;
    public readonly int maxSlotCapacity = 12;
    bool isDisplayFull = false;

    public int CurrBoxCapacity => currSlotCapacity;
    public bool IsDisplayFull => isDisplayFull; // Checked by inventory script before AddItem() is called

    // Update is called once per frame
    void Update()
    {
        UpdateSlotSelection();
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

                ++currSlotCapacity;
                if (currSlotCapacity >= maxSlotCapacity)
                {
                    isDisplayFull = true;
                }

                return;
            }
        }
    }

    public void RemoveItem(string itemName)
    {
        for (int i = 0; i < slots.Count; ++i)
        {
            // Loop through all the slots and look for the item that we want to remove
            if (!(slots[i].isOccupied && slots[i].itemName == itemName)) continue;

            InventorySlot slot = slots[i];

            --slot.itemQuantity;
            if (slot.itemQuantity <= 0)
            {
                // No more of this item, unoccupy the slot
                slot.itemName = "";
                slot.itemImage.enabled = false;
                slot.quantityText.enabled = false;
                slot.isOccupied = false;
            }
            else
            {
                // Just update the text of the slot to display the new quantity
                slot.quantityText.text = slot.itemQuantity.ToString();
            }

            slots[i] = slot;

            return;
        }
    }

    public string GetSelectedItemName()
    {
        return slots[selectedSlotIndex].itemName;
    }

    private void UpdateSlotSelection()
    {
        // Use keyboard to select a slot
        for (int i = 0; i < slots.Count; ++i)
        {
            if (Keyboard.current[toolbarKeys[i]].wasPressedThisFrame)
            {
                selectedSlotIndex = i;
            }
        }

        // Use scroll wheel to select a slot
        Vector2 mouseScroll = Mouse.current.scroll.ReadValue();
        if (mouseScroll.y > 0f)
        {
            // Scrolled up
            if (selectedSlotIndex < slots.Count - 1) ++selectedSlotIndex;

        }
        else if (mouseScroll.y < 0f)
        {
            // Scrolled down
            if (selectedSlotIndex > 0) --selectedSlotIndex;
        }

        // Move the selection box accordingly
        redSelectionSlot.rectTransform.SetParent(
            slots[selectedSlotIndex].slotImage.rectTransform, false
        );
    }

#if UNITY_EDITOR
    [ContextMenu("Find All Toolbar Slots")]
    private void FindAllToolbarSlots()
    {
        slots.Clear();

        // Get the toolbar child
        Transform tlbTransform = transform.GetChild(0);

        for (int i = 0; i < tlbTransform.childCount; ++i)
        {
            Transform slotTransform = tlbTransform.GetChild(i);

            InventorySlot newSlot = new()
            {
                slotImage = slotTransform.GetComponent<Image>(),
                itemImage = slotTransform.GetChild(0).GetComponent<Image>(),
                quantityText = slotTransform.GetChild(1).GetComponent<TextMeshProUGUI>()
            };

            // Get the red selection slot
            if (i == 0)
            {
                redSelectionSlot = slotTransform.GetChild(2).GetComponent<Image>();
            }

            slots.Add(newSlot);
        }
    }
#endif
}
