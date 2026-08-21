using UnityEngine;
using UnityEngine.InputSystem;

public class UIInventoryManager : PersistentSingleton<UIInventoryManager>
{
    [Header("Dependencies")]
    [SerializeField] private UIFader canvasFader;
    [SerializeField] private UIFader inventoryRowsFader;
    
    [Header("Event Channels")]
    [SerializeField] private EventBool onToggledInventoryEvent;
    [SerializeField] private EventVoid onDialogueStartedEvent;
    [SerializeField] private EventVoid onDialogueEndedEvent;

    private void OnEnable()
    {
        onToggledInventoryEvent.Subscribe(OnToggledInventory);
        onDialogueStartedEvent.Subscribe(OnDialogueStarted);
        onDialogueEndedEvent.Subscribe(OnDialogueEnded);
    }

    private void OnDisable()
    {
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
