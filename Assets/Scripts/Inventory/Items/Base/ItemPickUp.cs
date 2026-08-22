using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour
{
    [Header("Item Pickup")]
    [SerializeField] Inventory inventory;
    [SerializeField] ComponentCache cache;

    // Update is called once per frame
    void Update()
    {
        InputAction interactAction = InputSystem.actions.FindAction("Use");
        if (interactAction.WasPressedThisFrame())
        {
            if (inventory == null) return;
            inventory.UseSelectedItem(this.gameObject, cache, 1);
        }
    }

    public bool PickUp(ItemInstance item)
    {
        return inventory.AddItem(item);
    }
}
