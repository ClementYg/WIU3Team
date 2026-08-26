using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item")]
    public ItemInstance item;

    [Header("Event Channels")]
    [SerializeField] EventVoid onItemCollectedEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ItemPickup itmPickUp))
        {
            if (item == null || itmPickUp.PickUp(item) == false) return;

            // Raise the event if have
            if (onItemCollectedEvent != null)
            {
                onItemCollectedEvent.Raise();
            }

            Destroy(gameObject);
        }
    }
}
