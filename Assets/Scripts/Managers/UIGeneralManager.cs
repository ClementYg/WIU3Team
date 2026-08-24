using UnityEngine;

public class UIGeneralManager : PersistentSingleton<UIGeneralManager>
{
    [Header("UI Faders")]
    [SerializeField] UIFader canvasFader;

    [Header("Event Channels")]
    [SerializeField] EventBool onToggledGeneralUIEventBool;

    private void OnEnable()
    {
        onToggledGeneralUIEventBool.Subscribe(OnToggledGeneralUI);
    }

    private void OnDisable()
    {
        onToggledGeneralUIEventBool.Unsubscribe(OnToggledGeneralUI);
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
