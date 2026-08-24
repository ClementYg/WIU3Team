using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneralUISystem : PersistentSingleton<GeneralUISystem>
{
    [Header("General UI System")]
    [SerializeField] Image page;
    [SerializeField] TextMeshProUGUI header;
    [SerializeField] TextMeshProUGUI body;

    [Header("Event Channels")]
    [SerializeField] EventUIPage onDisplayPageRequestedEvent;
    [SerializeField] EventBool onToggledGeneralUIEventBool;

    private void OnEnable()
    {
        onDisplayPageRequestedEvent.Subscribe(DisplayUIPage);
    }

    private void OnDisable()
    {
        onDisplayPageRequestedEvent.Unsubscribe(DisplayUIPage);
    }

    private void DisplayUIPage(UIPage toDisplay)
    {
        if (toDisplay == null) return;

        // Set UI
        page.color = toDisplay.pageColor;

        header.text = toDisplay.header.text;
        header.color = toDisplay.header.textColor;

        body.text = toDisplay.body.text;
        body.color = toDisplay.body.textColor;

        // Raise the event for UIGeneralManager
        onToggledGeneralUIEventBool.Raise(true);
    }
}
