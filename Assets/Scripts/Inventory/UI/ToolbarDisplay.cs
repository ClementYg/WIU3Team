using UnityEngine;
using UnityEngine.InputSystem;

public class ToolbarDisplay : InventoryDisplay
{
    // Store a reference to the red selection slot
    [SerializeField] SelectionSlot slctnSlot;

    // Store the selected slot index
    int selectedSlotIndex = 0;

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

    private void Awake()
    {
        // Check if the toolbar display has more than one inventory row
        if (rows.Count > 1)
        {
            Debug.LogError("ToolbarDisplay: Toolbar cannot have more than one inventory row.");
            return;
        }

        // At the start, parent the selection to the first slot
        slctnSlot.MoveSelectionSlot(rows[0].slots[0]);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlotSelection();
    }

    public InventorySlot GetSelectedSlot()
    {
        return slctnSlot.SelectedSlot;
    }

    private void UpdateSlotSelection()
    {
        // Use keyboard to select a slot
        for (int i = 0; i < rows[0].slots.Count; ++i)
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
            if (selectedSlotIndex < rows[0].slots.Count - 1) ++selectedSlotIndex;
        }
        else if (mouseScroll.y < 0f)
        {
            // Scrolled down
            if (selectedSlotIndex > 0) --selectedSlotIndex;
        }

        // Move the selection slot accordingly
        slctnSlot.MoveSelectionSlot(rows[0].slots[selectedSlotIndex]);
    }

#if UNITY_EDITOR
    [ContextMenu("Find Toolbar References")]
    protected void FindToolbarReferences()
    {
        Transform rowTransform = transform.Find("Toolbar Row");

        // Add the inventory row
        rows.Clear();
        InventoryRow tlbRow = rowTransform.GetComponent<InventoryRow>();
        rows.Add(tlbRow);

        // Get the selection slot
        slctnSlot = null;
        Transform slotTransform = rowTransform.Find("Selection Slot");
        SelectionSlot slotComp = slotTransform.GetComponent<SelectionSlot>();
        slctnSlot = slotComp;
    }
#endif
}
