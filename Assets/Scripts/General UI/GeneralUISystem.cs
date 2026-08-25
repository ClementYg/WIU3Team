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
    [SerializeField] EventBool onToggledGeneralUIEventBool;

    private void OnEnable()
    {
        onDisplayPageRequestedEvent.Subscribe(DisplayUIPage);
        onDisplayIconRequestedEvent.Subscribe(DisplayUIIcon);
    }

    private void OnDisable()
    {
        onDisplayPageRequestedEvent.Unsubscribe(DisplayUIPage);
        onDisplayIconRequestedEvent.Unsubscribe(DisplayUIIcon);
    }

    // Update is called once per frame
    void Update()
    {
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
        Debug.Log("Raised");
        onToggledGeneralUIEventBool.Raise(true);
    }

    private void DisplayUIIcon(UIIcon toDisplay)
    {
        if (toDisplay == null) return;

        // Set UI
        iconImage.sprite = toDisplay.sprite;
        iconTransform.localPosition = toDisplay.position;
        iconTransform.localScale = toDisplay.scale;

        // Raise the event for UIGeneralManager
        onToggledGeneralUIEventBool.Raise(true);
    }
}
