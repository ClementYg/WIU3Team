using UnityEngine;

public class UIGeneralManager : PersistentSingleton<UIGeneralManager>
{
    [Header("UI Faders")]
    [SerializeField] UIFader canvasFader;
    [SerializeField] UIFader pageFader;
    [SerializeField] UIFader iconFader;

    [Header("Event Channels")]
    [SerializeField] EventBool onToggledPageEventBool;
    [SerializeField] EventBool onToggledIconEventBool;

    private void OnEnable()
    {
        onToggledPageEventBool.Subscribe(OnToggledPage);
        onToggledIconEventBool.Subscribe(OnToggledIcon);
    }

    private void OnDisable()
    {
        onToggledPageEventBool.Unsubscribe(OnToggledPage);
        onToggledIconEventBool.Unsubscribe(OnToggledIcon);
    }

    private void OnToggledPage(bool isEnabled)
    {
        if (isEnabled)
        {
            pageFader.FadeIn();
        }
        else
        {
            pageFader.FadeOut();
        }

        OnToggledGeneralUI(isEnabled);
    }

    private void OnToggledIcon(bool isEnabled)
    {
        if (isEnabled)
        {
            iconFader.FadeIn();
        }
        else
        {
            iconFader.FadeOut();
        }

        OnToggledGeneralUI(isEnabled);
    }

    private void OnToggledGeneralUI(bool isEnabled)
    {
        if (isEnabled)
        {
            canvasFader.FadeIn();
        }
        else
        {
            canvasFader.FadeOut();
        }
    }
}
