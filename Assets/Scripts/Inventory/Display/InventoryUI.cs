using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Header("Row Displays")]
    // Order the displays by item fill precedence, i.e. When both displays are empty,
    // the preferred display for an item to be added to should be serialized first.
    [SerializeField] List<RowDisplay> displays = new();

    public void InitUI(List<ItemInstance> items)
    {
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

#if UNITY_EDITOR
    [ContextMenu("Find All Row Displays")]
    private void FindAllRowDisplays()
    {
        displays.Clear();

        GameObject canvas = GameObject.Find("Canvas");

        // Add the toolbar display first, it gets item fill precedence
        if (canvas.transform.TryGetComponent<ToolbarDisplay>(out ToolbarDisplay tlbDisplay))
        {
            displays.Add(tlbDisplay);
        }
        else
        {
            Debug.LogWarning("InventoryUI: Failed to find toolbar display component.");
        }

        // Add the inventory display
        if (canvas.transform.TryGetComponent<InventoryDisplay>(out InventoryDisplay invDisplay))
        {
            displays.Add(invDisplay);
        }
        else
        {
            Debug.LogWarning("InventoryUI: Failed to find inventory display component.");
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
