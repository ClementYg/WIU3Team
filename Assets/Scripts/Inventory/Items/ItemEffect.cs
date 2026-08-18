using UnityEngine;

[CreateAssetMenu(fileName = "ItemEffect", menuName = "Scriptable Objects/Inventory/ItemEffect")]
public abstract class ItemEffect : ScriptableObject
{
    public abstract void Use(GameObject user);
}
