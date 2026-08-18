using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ToolbarDisplay : MonoBehaviour
{
    // Store a reference to the toolbar inventory row
    [SerializeField] InventoryRow toolbar;

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

    // Store a reference to the red selection slot
    [SerializeField] Image redSelectionSlot;

    // To track the index of the selected slot
    int selectedSlotIndex = 0;

    // Update is called once per frame
    void Update()
    {
        UpdateSlotSelection();
    }

    public void AddItem(ItemInstance item)
    {
        toolbar.AddItem(item);
    }

    public bool RemoveItem(string itemName)
    {
        // Check if the item exists in the toolbar
        for (int i = 0; i < toolbar.slots.Count; ++i)
        {
            if (toolbar.slots[i].isOccupied && toolbar.slots[i].itemName == itemName)
            {
                toolbar.RemoveItem(i);
                return true;
            }
        }

        return false;
    }

    public string GetSelectedItemName()
    {
        return toolbar.slots[selectedSlotIndex].itemName;
    }

    public bool CheckIsCapacityFull()
    {
        return toolbar.IsCapacityFull;
    }

    private void UpdateSlotSelection()
    {
        // Use keyboard to select a slot
        for (int i = 0; i < toolbar.slots.Count; ++i)
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
            if (selectedSlotIndex < toolbar.slots.Count - 1) ++selectedSlotIndex;

        }
        else if (mouseScroll.y < 0f)
        {
            // Scrolled down
            if (selectedSlotIndex > 0) --selectedSlotIndex;
        }

        // Move the selection box accordingly
        redSelectionSlot.rectTransform.SetParent(
            toolbar.slots[selectedSlotIndex].slotRectTransform, false
        );
    }
}
