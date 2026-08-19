using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Header("Dependencies")]
    // Order the displays by item fill precedence, i.e. When both displays are empty,
    // the preferred display for an item to be added to should be serialized first.
    [SerializeField] List<ItemDisplay> displays = new();

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
        foreach (ItemDisplay display in displays)
        {
            if (display.TryAddItem(item)) return true;
        }

        return false;
    }

    public bool RemoveItem(string itemName)
    {
        foreach (ItemDisplay display in displays)
        {
            if (display.TryRemoveItem(itemName)) return true;
        }

        return false;
    }

    public bool RemoveStack(string itemName)
    {
        foreach (ItemDisplay display in displays)
        {
            if (display.TryRemoveStack(itemName)) return true;
        }

        return false;
    }

    public string GetSelectedItemName()
    {
        foreach (ItemDisplay display in displays)
        {
            if (display is ToolbarDisplay tlbDisplay)
            {
                return tlbDisplay.GetSelectedItemName();
            }
        }

        return null;
    }

#if UNITY_EDITOR
    [ContextMenu("Find All Item Displays")]
    private void FindAllItemDisplays()
    {
        displays.Clear();

        GameObject canvas = GameObject.Find("Canvas");
        
        // Add the toolbar display first, it gets item fill precedence
        ToolbarDisplay tlbDisplay = canvas.transform.GetComponent<ToolbarDisplay>();
        displays.Add(tlbDisplay);

        // Add the inventory displays
        InventoryDisplay invDisplay = canvas.transform.GetComponent<InventoryDisplay>();
        displays.Add(invDisplay);
    }
#endif
}
