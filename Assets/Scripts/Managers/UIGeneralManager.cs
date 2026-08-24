using UnityEngine;

public class UIGeneralManager : PersistentSingleton<UIGeneralManager>
{
    [Header("Event Channels")]
    [SerializeField] EventUIPage OnDisplayPageRequestedEvent;

    private void OnEnable()
    {
        OnDisplayPageRequestedEvent.Subscribe(DisplayUIPage);
    }

    private void OnDisable()
    {
        OnDisplayPageRequestedEvent.Unsubscribe(DisplayUIPage);
    }

    private void DisplayUIPage(UIPage toDisplay)
    {

    }
}
