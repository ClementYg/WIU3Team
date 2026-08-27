using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneralUISystem : PersistentSingleton<GeneralUISystem>
{
    [Header("UI Page")]
    [SerializeField] Image page;
    [SerializeField] TextMeshProUGUI header;
    [SerializeField] TextMeshProUGUI body;

    [Header("UI Icon")]
    [SerializeField] Image iconImage;
    [SerializeField] Transform iconTransform;

    [Header("Event Channels")]
    [SerializeField] EventUIPage onDisplayPageRequestedEvent;
    [SerializeField] EventUIIcon onDisplayIconRequestedEvent;
    [SerializeField] EventUITimer onDisplayTimerRequestedEvent;
    [SerializeField] EventBool onToggledPageEventBool;
    [SerializeField] EventBool onToggledIconEventBool;
    [SerializeField] EventBool onToggledTimerEventBool;

    private void OnEnable()
    {
        onDisplayPageRequestedEvent.Subscribe(DisplayUIPage);
        onDisplayIconRequestedEvent.Subscribe(DisplayUIIcon);
        onDisplayTimerRequestedEvent.Subscribe(DisplayUITimer);
    }

    private void OnDisable()
    {
        onDisplayPageRequestedEvent.Unsubscribe(DisplayUIPage);
        onDisplayIconRequestedEvent.Unsubscribe(DisplayUIIcon);
        onDisplayTimerRequestedEvent.Unsubscribe(DisplayUITimer);
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
        onToggledPageEventBool.Raise(true);
    }

    private void DisplayUIIcon(UIIcon toDisplay)
    {
        if (toDisplay == null) return;

        // Set UI
        iconImage.sprite = toDisplay.sprite;
        iconTransform.localPosition = toDisplay.position;
        iconTransform.localScale = toDisplay.scale;

        // Raise the event for UIGeneralManager
        onToggledIconEventBool.Raise(true);
    }

    private void DisplayUITimer(UITimer toDisplay)
    {
        if (toDisplay == null) return;

        // Raise the event for UIGeneralManager
        onToggledTimerEventBool.Raise(true);
    }
}
