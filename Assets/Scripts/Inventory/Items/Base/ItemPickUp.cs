using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickUp : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] Inventory inventory;
    [SerializeField] ToolbarDisplay tlbDisplay;

    // Update is called once per frame
    void Update()
    {
        InputAction interactAction = InputSystem.actions.FindAction("Interact");
        if (interactAction.WasPressedThisFrame())
        {
            // Get the name of the selected item
            string itemName = tlbDisplay.GetSelectedItemName();
            if (itemName == "") return; // No item is selected

            // Get the item instance
            ItemInstance item = inventory.GetItem(itemName);
            if (item == null) return;

            // Remove the item from the inventory
            inventory.RemoveItem(itemName);

            if (item.itemEffect == null) return;

            item.itemEffect.Use(this.gameObject);
        }
    }

    public bool PickUp(ItemInstance item)
    {
        return inventory.AddItem(item);
    }
}
