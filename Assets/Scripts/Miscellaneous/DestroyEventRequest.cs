using UnityEngine;

public class DestroyEventRequest : MonoBehaviour
{
    [SerializeField] private EventVoid onDestroyedEvent;
    [SerializeField] private bool destroyOnTrigger;

    private void OnDestroy()
    {
        onDestroyedEvent.Raise();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!destroyOnTrigger) return;
        if (collision.TryGetComponent(out ItemPickup itmPickUp))
        {
            Destroy(gameObject);
        }
    }
}
