using UnityEngine;

public class ItemInteract : Interactable
{
    [Header("Dependencies")]
    [SerializeField] private ItemInstance item;
    [SerializeField] private Inventory inventory;
    [SerializeField] private ComponentCache playerCache;
    
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
        if (player == null) return;

        ItemPickup itmPickup = playerCache.Get<ItemPickup>();
        if (itmPickup == null || itmPickup.PickUp(item) == false) return;

        Destroy(gameObject);
    }
}
