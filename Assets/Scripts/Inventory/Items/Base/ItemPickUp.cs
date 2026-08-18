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
            if (inventory = null) return;
            inventory.UseSelectedItem(this.gameObject);
        }
    }

    public bool PickUp(ItemInstance item)
    {
        return inventory.AddItem(item);
    }
}
