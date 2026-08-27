using UnityEngine;

[CreateAssetMenu(fileName = "ItemQuestEffect", menuName = "ScriptableObjects/Inventory/Effects/ItemQuestEffect")]
public class ItemQuestEffect : ItemEffect
{
    [Header("Item Quest Effect")]
    public bool canSubmitItem = false;

    [Header("Event Channels")]
    [SerializeField] EventVoid onSubmitItemEvent;

    public override void Use(GameObject user, ComponentCache userCache)
    {
        if (canSubmitItem == false) return;
        onSubmitItemEvent.Raise();
    }
}
