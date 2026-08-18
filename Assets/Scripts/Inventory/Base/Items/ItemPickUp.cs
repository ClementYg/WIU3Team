using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickUp : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] Inventory inventory;
    //[SerializeField] Toolbar toolbar;
    //[SerializeField] AlertManager alertMan;

    // Update is called once per frame
    void Update()
    {
        InputAction interactAction = InputSystem.actions.FindAction("Interact");
        if (interactAction.WasPressedThisFrame())
        {
            // Get the name of the selected item
            //string itemName = toolbar.GetSelectedItemName();
            //if (itemName == "") return; // No item is selected

            // Get the item instance
            //ItemInstance item = inventory.GetItem(itemName);
            //if (item == null) return;

            // Remove the item from the inventory
            //inventory.RemoveItem(itemName);

            //if (item.itemEffect == null) return;

            //item.itemEffect.Use(this.gameObject);
        }
    }

    public void PickUp(ItemInstance item)
    {
        inventory.AddItem(item);
        inventory.DisplayItems();
    }
}
