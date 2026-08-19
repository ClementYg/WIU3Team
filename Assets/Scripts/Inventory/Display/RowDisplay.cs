using UnityEngine;

public abstract class RowDisplay : MonoBehaviour
{
    public abstract bool TryAddItem(ItemInstance item);
    public abstract bool TryRemoveItem(string itemName);
    public abstract bool TryRemoveStack(string itemName);
}
