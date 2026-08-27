using UnityEngine; 

public class UIBestiaryManager : MonoBehaviour
{
    [Header("UI Faders")]
    [SerializeField] UIFader canvasFader;

    [Header("Event Channels")]
    [SerializeField] EventBool onToggledBestiaryEvent;
    [SerializeField] EventVoid onClosedBestiaryEvent;

    private void OnEnable()
    {
        onToggledBestiaryEvent.Subscribe(OnToggledBestiary);
    }

    private void OnDisable()
    {
        onToggledBestiaryEvent.Unsubscribe(OnToggledBestiary);
    }

    private void OnToggledBestiary(bool isEnabled)
    {
        if (isEnabled)
        {
            canvasFader.FadeIn();
        }
        else
        {
            canvasFader.FadeOut();

            // Raise the event for the tutorial
            onClosedBestiaryEvent.Raise();
        }
    }
}
