using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
}
