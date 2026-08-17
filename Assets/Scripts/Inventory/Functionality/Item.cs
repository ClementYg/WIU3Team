using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemInstance item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ItemPickUp itmPickUp))
        {
            if (item == null) return;

            itmPickUp.PickUp(item);
            Destroy(gameObject);
        }
    }
}
