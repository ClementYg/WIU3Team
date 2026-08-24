using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemInstance item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ItemPickup itmPickUp))
        {
            if (item == null || itmPickUp.PickUp(item) == false) return;

            Destroy(gameObject);
        }
    }
}
