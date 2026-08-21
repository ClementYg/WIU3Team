using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickUp : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] Inventory inventory;

    // Update is called once per frame
    void Update()
    {
        InputAction interactAction = InputSystem.actions.FindAction("Use");
        if (interactAction.WasPressedThisFrame())
        {
            Debug.Log("ItemPickUp: detected click");
            if (inventory == null) return;
            inventory.UseSelectedItem(this.gameObject);
        }
    }

    public bool PickUp(ItemInstance item)
    {
        return inventory.AddItem(item);
    }
}
