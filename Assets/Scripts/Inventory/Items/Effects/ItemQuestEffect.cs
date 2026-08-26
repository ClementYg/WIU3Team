using UnityEngine;

[CreateAssetMenu(fileName = "ItemQuestEffect", menuName = "ScriptableObjects/Inventory/Effects/ItemQuestEffect")]
public class ItemQuestEffect : ItemEffect
{
    public override void Use(GameObject user, ComponentCache userCache)
    {
        // Check if we are within range of the Quest NPC. If so, submit this item to them.
    }
}
