using UnityEngine;
using UnityEngine.UI;

public class ItemInteract : Interactable
{
    [Header("Dependencies")]
    [SerializeField] private ItemInstance item;
    [SerializeField] private Inventory inventory;
    
    protected override void Start()
    {
        if (useDefaultValues)
        {
            fadeSpeed = 8f;
            moveSpeed = 2f;
            maxDividerDistance = 0.15f;
            textContent = "Collect";
            fontSize = 1f;
            initialDistanceFromCenter = 0f;
        }
        
        base.Start();
    }
    
    public override void Interact()
    {
        if (player != null)
        {
            if (player.TryGetComponent<ItemPickUp>(out ItemPickUp itemPickup))
            {
                if (itemPickup == null || itemPickup.PickUp(item) == false) return;

                Destroy(gameObject);
            }
        }
    }
}
