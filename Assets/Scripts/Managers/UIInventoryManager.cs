using UnityEditor.XR;
using UnityEngine;

public class UIInventoryManager : PersistentSingleton<UIInventoryManager>
{
    [Header("UI Faders")]
    [SerializeField] private UIFader canvasFader;
    [SerializeField] private UIFader inventoryRowsFader;
    
    [Header("Event Channels")]
    [SerializeField] private EventBool onToggledInventoryEvent;
    [SerializeField] private EventVoid onDialogueStartedEvent;
    [SerializeField] private EventVoid onDialogueEndedEvent;
    [SerializeField] private EventBool onToggledBestiaryEvent;

    private void OnEnable()
    {
        onToggledBestiaryEvent.Subscribe(OnToggledBestiary);
        onToggledInventoryEvent.Subscribe(OnToggledInventory);
        onDialogueStartedEvent.Subscribe(OnDialogueStarted);
        onDialogueEndedEvent.Subscribe(OnDialogueEnded);
    }

    private void OnDisable()
    {
        onToggledBestiaryEvent.Unsubscribe(OnToggledBestiary);
        onToggledInventoryEvent.Unsubscribe(OnToggledInventory);
        onDialogueStartedEvent.Unsubscribe(OnDialogueStarted);
        onDialogueEndedEvent.Unsubscribe(OnDialogueEnded);
    }

    private void OnToggledInventory(bool isEnabled)
    {
        if (isEnabled)
        {
            inventoryRowsFader.FadeIn();
        }
        else
        {
            inventoryRowsFader.FadeOut();
        }
    }

    private void OnToggledBestiary(bool isEnabled)
    {
        if (isEnabled)
        {
            canvasFader.FadeOut();
            inventoryRowsFader.FadeOut();
        }
        else
        {
            canvasFader.FadeIn();
        }
      
    }
    private void OnDialogueStarted()
    {
        canvasFader.FadeOut();
        inventoryRowsFader.FadeOut();
    }

    private void OnDialogueEnded()
    {
        canvasFader.FadeIn();
    }
}
