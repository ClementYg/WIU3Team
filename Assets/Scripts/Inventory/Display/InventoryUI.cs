using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Header("Row Displays")]
    // Order the displays by item fill precedence, i.e. When both displays are empty,
    // the preferred display for an item to be added to should be serialized first.
    [SerializeField] List<RowDisplay> displays = new();

    [Header("Event Channels")]
    [SerializeField] EventInventorySlot OnInventoryClickEvent;

    // For lifting and placing items in the display
    [Header("Carrying Items")]
    [SerializeField] GameObject itemToCarry;
    [SerializeField] SlotUI UIToCarry;
    bool isPointerCarryingItem = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
        foreach (RowDisplay display in displays)
        {
            if (display.TryAddItem(item)) return true;
        }

        return false;
    }

    public bool RemoveItem(string itemName)
    {
        foreach (RowDisplay display in displays)
        {
            if (display.TryRemoveItem(itemName)) return true;
        }

        return false;
    }

    public bool RemoveStack(string itemName)
    {
        foreach (RowDisplay display in displays)
        {
            if (display.TryRemoveStack(itemName)) return true;
        }

        return false;
    }

    public string GetSelectedItemName()
    {
        foreach (RowDisplay display in displays)
        {
            if (display is ToolbarDisplay tlbDisplay)
            {
                return tlbDisplay.GetSelectedItemName();
            }
        }

        return null;
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
    //Place and lift isnt really working I think?
    //Most likely need a reference to Inventory and have a removeItem there
    private void LiftItem(InventorySlot slotClicked)
    {
        RemoveStack(slotClicked.itemName);
        UIToCarry = slotClicked.UI;
        UIToCarry.itemImage.enabled = true;
        UIToCarry.quantityText.enabled = true;
    }
    //Same thing for Place item, need to reference Inventory to place item in the different slot.
    private void PlaceItem(InventorySlot slotClicked)
    {
        UIToCarry.itemImage.enabled = false;
        UIToCarry.quantityText.enabled = false;
        UIToCarry = null;
    }

#if UNITY_EDITOR
    [ContextMenu("Find All Row Displays")]
    private void FindAllRowDisplays()
    {
        displays.Clear();

        GameObject canvas = GameObject.Find("Inventory Canvas");

        // Add the toolbar display first, it gets item fill precedence
        if (canvas.transform.TryGetComponent<ToolbarDisplay>(out ToolbarDisplay tlbDisplay))
        {
            displays.Add(tlbDisplay);
        }
        
        // Add the inventory display
        if (canvas.transform.TryGetComponent<InventoryDisplay>(out InventoryDisplay invDisplay))
        {
            displays.Add(invDisplay);
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
