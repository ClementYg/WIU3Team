using UnityEngine;

public abstract class ItemDisplay : MonoBehaviour
{
    public abstract bool TryAddItem(ItemInstance item);
    public abstract bool TryRemoveItem(string itemName);
}
