using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ToolbarDisplay : InventoryDisplay
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

    // Store a reference to the red selection slot
    [SerializeField] Image redSelectionSlot;

    // To track the index of the selected slot
    int selectedSlotIndex = 0;

    private void Awake()
    {
        // Check if the toolbar display has more than one inventory row
        if (rows.Count > 1)
        {
            Debug.LogError("ToolbarDisplay: Toolbar cannot have more than one inventory row.");
            return;
        }

        // Move the selection slot accordingly
        MoveSelectionSlot(rows[0].slots[selectedSlotIndex].slotRectTransform);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlotSelection();
    }

    public bool TryGetSelectedItemName(out string itemName)
    {
        if (rows[0].slots[selectedSlotIndex].itemDisplayed == null)
        {
            itemName = null;
            return false;
        }

        itemName = rows[0].slots[selectedSlotIndex].itemDisplayed.itemData.itemName;
        return true;
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
        MoveSelectionSlot(rows[0].slots[selectedSlotIndex].slotRectTransform);
    }

    private void MoveSelectionSlot(Transform newParent)
    {
        redSelectionSlot.rectTransform.SetParent(newParent, false);

        Vector3 newPos = redSelectionSlot.rectTransform.localPosition;
        newPos = new Vector3(0f, 0f, 0f);
        redSelectionSlot.rectTransform.localPosition = newPos;
    }

#if UNITY_EDITOR
    [ContextMenu("Find Toolbar Row")]
    protected void FindToolbarRow()
    {
        rows.Clear();

        Transform rowTransform = transform.Find("Toolbar Row");
        InventoryRow newRow = rowTransform.GetComponent<InventoryRow>();
        
        rows.Add(newRow);
    }
#endif
}
