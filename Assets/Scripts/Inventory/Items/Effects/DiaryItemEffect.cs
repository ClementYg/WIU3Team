using UnityEngine;

[CreateAssetMenu(fileName = "DiaryItemEffect", menuName = "ScriptableObjects/Inventory/Effects/DiaryItemEffect")]
public class DiaryItemEffect : ItemPageEffect
{
    public override void Use(GameObject user, ComponentCache userCache)
    {
        if (toDisplay == null) return;
        onDisplayPageRequestedEvent.Raise(toDisplay);
    }
}
