using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Header("Row Displays")]
    // Order the displays by item fill precedence, i.e. When both displays are empty,
    // the preferred display for an item to be added to should be serialized first.
    [SerializeField] List<InventoryDisplay> displays = new();

    [Header("Event Channels")]
    [SerializeField] EventInventorySlot OnInventoryClickEvent;

    // For lifting and placing items in the display
    [Header("Carrying Items")]
    [SerializeField] GameObject itemToCarry;
    [SerializeField] SlotUI UIToCarry;

    ItemInstance carriedItem = null;
    bool isPointerCarryingItem = false;

    private void OnEnable()
    {
        OnInventoryClickEvent.Subscribe(CheckIsLift);
    }

    private void OnDisable()
    {
        OnInventoryClickEvent.Unsubscribe(CheckIsLift);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPointerCarryingItem)
        {
            // The UI should follow pointer position
            Vector2 pointerPos = Mouse.current.position.ReadValue();
            itemToCarry.transform.position = pointerPos;
        }
    }

    public void InitUI(List<ItemInstance> items)
    {
        // This function is called from Inventory
        if (items.Count > 0)
        {
            foreach (ItemInstance item in items)
            {
                AddItem(item);
            }
        }
    }

    public bool AddItem(ItemInstance item)
    {
        foreach (InventoryDisplay display in displays)
        {
            if (display.AddItem(item)) return true;
        }

        return false;
    }

    public bool RemoveItem(ItemInstance item)
    {
        foreach (InventoryDisplay display in displays)
        {
            if (display.RemoveItem(item)) return true;
        }

        return false;
    }

    public bool RemoveStack(ItemInstance item)
    {
        foreach (InventoryDisplay display in displays)
        {
            if (display.RemoveStack(item)) return true;
        }

        return false;
    }

    public InventorySlot GetSelectedSlot()
    {
        foreach (InventoryDisplay display in displays)
        {
            if (display is ToolbarDisplay tlbDisplay)
            {
                return tlbDisplay.GetSelectedSlot();
            }
        }

        return null;
    }

    public string GetSelectedItemName()
    {
        InventorySlot selectedSlot = GetSelectedSlot();
        return selectedSlot.itemDisplayed.itemData.itemName;
    }

    private void CheckIsLift(InventorySlot slotClicked)
    {
        isPointerCarryingItem = !isPointerCarryingItem;
        if (isPointerCarryingItem)
        {
            LiftItem(slotClicked);
        }
        else
        {
            PlaceItem(slotClicked);
        }
    }

    private void LiftItem(InventorySlot slotClicked)
    {
        if (!slotClicked.IsOccupied)
        {
            // Nothing there to lift
            isPointerCarryingItem = !isPointerCarryingItem;
            return;
        }

        // Set the UI to carry
        UIToCarry.SetUI(
            itemToCarry.transform,
            slotClicked.UI.itemImage.sprite, slotClicked.UI.quantityText.text,
            true
        );

        // Clear the slot clicked UI
        slotClicked.ClearUI();

        // Set the carried item
        carriedItem = slotClicked.itemDisplayed;

        // Visually remove the stack
        RemoveStack(slotClicked.itemDisplayed);
    }

    private void PlaceItem(InventorySlot slotClicked)
    {
        if (slotClicked.IsOccupied)
        {
            // For now, don't place on an occupied slot
            isPointerCarryingItem = !isPointerCarryingItem;
            return;
        }

        // Set the slot clicked back
        slotClicked.SetUI(UIToCarry.itemImage.sprite, int.Parse(UIToCarry.quantityText.text));
        slotClicked.itemDisplayed = carriedItem;

        // Set the UI to carry
        UIToCarry.itemImage.enabled = false;
        UIToCarry.quantityText.enabled = false;

        // Set the carried item
        carriedItem = null;
    }

#if UNITY_EDITOR
    [ContextMenu("Find All Row Displays")]
    private void FindAllRowDisplays()
    {
        displays.Clear();

        GameObject canvas = GameObject.Find("Inventory Canvas");
        InventoryDisplay[] compDisplays = canvas.transform.GetComponents<InventoryDisplay>();

        // Look for the toolbar display and add it first
        foreach (InventoryDisplay display in compDisplays)
        {
            if (display is ToolbarDisplay tlbDisplay)
            {
                displays.Add(tlbDisplay);
            }
        }

        // Add the rest of the inventory displays
        foreach (InventoryDisplay display in compDisplays)
        {
            if (display is not ToolbarDisplay)
            {
                displays.Add(display);
            }
        }
    }

    private void OnValidate()
    {
        if (displays.Count <= 0)
        {
            Debug.LogWarning("InventoryUI: Please add references to the displays.");
            return;
        }
    }
#endif
}
