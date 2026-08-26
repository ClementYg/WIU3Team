using UnityEngine;

[CreateAssetMenu(fileName = "ItemPageEffect", menuName = "ScriptableObjects/Inventory/Effects/ItemPageEffect")]
public class ItemPageEffect : ItemEffect
{
    [Header("Item Page Effect")]
    public UIPage toDisplay; // Displayed when the item is used

    [Header("Event Channels")]
    public EventUIPage onDisplayPageRequestedEvent;

    public override void Use(GameObject user, ComponentCache userCache)
    {
        if (toDisplay == null || onDisplayPageRequestedEvent == null)
        {
            Debug.LogError("ItemPageEffect: " + name + " has missing reference(s).");
            return;
        }

        onDisplayPageRequestedEvent.Raise(toDisplay);
    }

    public void RaiseEvent()
    {
        if (toDisplay == null || onDisplayPageRequestedEvent == null) return;

        // Raise the event to request a page display
        onDisplayPageRequestedEvent.Raise(toDisplay);
    }
}
