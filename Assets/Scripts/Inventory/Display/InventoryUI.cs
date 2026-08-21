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
    ItemInstance carriedItem = null;
    [SerializeField] GameObject itemToCarry;
    [SerializeField] SlotUI UIToCarry;
    bool isPointerCarryingItem = false;

    private void Awake()
    {
        // Subscribe to event channels
        OnInventoryClickEvent.Subscribe(CheckIsLift);
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

    public bool RemoveItem(ItemInstance itemName)
    {
        foreach (InventoryDisplay display in displays)
        {
            if (display.RemoveItem(itemName)) return true;
        }

        return false;
    }

    public bool RemoveStack(ItemInstance itemName)
    {
        foreach (InventoryDisplay display in displays)
        {
            if (display.RemoveStack(itemName)) return true;
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
        Debug.Log("InventoryUI: checking is lift");

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
        Debug.Log("InventoryUI: lifting the item");

        if (!slotClicked.IsOccupied)
        {
            // Nothing there to lift
            isPointerCarryingItem = !isPointerCarryingItem;
            return;
        }

        UIToCarry.itemImage.sprite = slotClicked.UI.itemImage.sprite;
        UIToCarry.itemImage.enabled = true;
        UIToCarry.itemImage.transform.SetParent(itemToCarry.transform);

        UIToCarry.quantityText.text = slotClicked.UI.quantityText.text;
        UIToCarry.quantityText.enabled = true;
        UIToCarry.quantityText.transform.SetParent(itemToCarry.transform);

        slotClicked.ClearUI();
        carriedItem = slotClicked.itemDisplayed;
        RemoveStack(slotClicked.itemDisplayed);
    }

    private void PlaceItem(InventorySlot slotClicked)
    {
        Debug.Log("InventoryUI: placing item");

        if (slotClicked.IsOccupied)
        {
            // For now, don't place on an occupied slot
            isPointerCarryingItem = !isPointerCarryingItem;
            return;
        }

        slotClicked.SetUI(UIToCarry.itemImage.sprite, int.Parse(UIToCarry.quantityText.text));

        UIToCarry.itemImage.enabled = false;
        UIToCarry.quantityText.enabled = false;
        slotClicked.itemDisplayed = carriedItem;
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
