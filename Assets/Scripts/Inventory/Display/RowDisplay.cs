using UnityEngine;

public abstract class RowDisplay : MonoBehaviour
{
    public abstract bool TryAddItem(ItemInstance item);
    public abstract bool TryRemoveItem(ItemInstance itemName);
    public abstract bool TryRemoveStack(ItemInstance itemName);
}
