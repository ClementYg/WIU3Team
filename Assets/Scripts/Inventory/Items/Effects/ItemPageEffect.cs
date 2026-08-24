using UnityEngine;

[CreateAssetMenu(fileName = "ItemPageEffect", menuName = "ScriptableObjects/Inventory/Effects/ItemPageEffect")]
public abstract class ItemPageEffect : ItemEffect
{
    [Header("Item Page Effect")]
    public UIPage toDisplay; // Displayed when the item is used

    [Header("Event Channels")]
    public EventUIPage onDisplayPageRequestedEvent;

    public void RaiseEvent()
    {
        if (toDisplay == null || onDisplayPageRequestedEvent == null) return;

        // Raise the event to request a page display
        onDisplayPageRequestedEvent.Raise(toDisplay);
    }
}
