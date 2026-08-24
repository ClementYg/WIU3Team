using UnityEngine;

[CreateAssetMenu(fileName = "ItemEffect", menuName = "ScriptableObjects/Inventory/ItemEffect")]
public abstract class ItemEffect : ScriptableObject
{
    public abstract void Use(GameObject user, ComponentCache userCache);
}
