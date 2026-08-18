using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class Toolbar : MonoBehaviour
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

    // Keep a list of all the boxes in the toolbar
    [SerializeField] List<ToolbarBox> boxes = new();

    // Store a reference to the red selection box
    [SerializeField] Image redSelectionBox;

    int selectedBoxIndex = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBoxSelection();
    }

    private void UpdateBoxSelection()
    {
        // Use keyboard to select a box
        for (int i = 0; i < boxes.Count; ++i)
        {
            if (Keyboard.current[toolbarKeys[i]].wasPressedThisFrame)
            {
                selectedBoxIndex = i;
            }
        }

        // Use scroll wheel to select a box
        Vector2 mouseScroll = Mouse.current.scroll.ReadValue();
        if (mouseScroll.y > 0f)
        {
            // Scrolled up
            if (selectedBoxIndex < boxes.Count - 1) ++selectedBoxIndex;

        }
        else if (mouseScroll.y < 0f)
        {
            // Scrolled down
            if (selectedBoxIndex > 0) --selectedBoxIndex;
        }

        // Move the selection box accordingly
        redSelectionBox.rectTransform.SetParent(
            boxes[selectedBoxIndex].boxImage.rectTransform, false
        );
    }

    public void AddItem(ItemInstance item)
    {
        for (int i = 0; i < boxes.Count; ++i)
        {
            if (boxes[i].isOccupied && boxes[i].itemName == item.itemData.itemName)
            {
                // Stack the item, just increment the quantity
                ToolbarBox stackedBox = boxes[i];

                ++stackedBox.itemQuantity;
                stackedBox.quantityText.text = stackedBox.itemQuantity.ToString();

                boxes[i] = stackedBox;

                return;
            }
            else if (!boxes[i].isOccupied)
            {
                // Display the new item at this unoccupied box
                ToolbarBox newBox = boxes[i];

                newBox.itemImage.sprite = item.itemData.itemImage;
                newBox.itemImage.enabled = true;
                newBox.quantityText.text = "1";
                newBox.quantityText.enabled = true;

                newBox.itemName = item.itemData.itemName;
                newBox.itemQuantity = 1;
                newBox.isOccupied = true;

                boxes[i] = newBox;

                return;
            }
        }
    }

    public void RemoveItem(string itemName)
    {
        for (int i = 0; i < boxes.Count; ++i)
        {
            // Loop through all the boxes and look for the item that we want to remove
            if (!(boxes[i].isOccupied && boxes[i].itemName == itemName)) continue;

            ToolbarBox box = boxes[i];

            --box.itemQuantity;
            if (box.itemQuantity <= 0)
            {
                // No more of this item, unoccupy the box
                box.itemName = "";
                box.itemImage.enabled = false;
                box.quantityText.enabled = false;
                box.isOccupied = false;
            }
            else
            {
                // Just update the text of the box to display the new quantity
                box.quantityText.text = box.itemQuantity.ToString();
            }

            boxes[i] = box;

            return;
        }
    }

    public string GetSelectedItemName()
    {
        ToolbarBox selectedBox = boxes[selectedBoxIndex];
        return selectedBox.itemName;
    }
}
