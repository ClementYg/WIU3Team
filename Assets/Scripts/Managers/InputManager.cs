using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [Header("Event Channels")]
    [SerializeField] private EventBool onToggledInventoryEvent;
    [SerializeField] private EventVoid onDialogueStartedEvent;
    [SerializeField] private EventVoid onDialogueEndedEvent;

    private bool toggledInventory = true;

    private void OnEnable()
    {
        onDialogueStartedEvent.Subscribe(OnDialogueStarted);
        onDialogueEndedEvent.Subscribe(OnDialogueEnded);
    }

    private void OnDisable()
    {
        onDialogueStartedEvent.Unsubscribe(OnDialogueStarted);
        onDialogueEndedEvent.Unsubscribe(OnDialogueEnded);
    }

    private void OnDialogueStarted()
    {
        InputSystem.actions.FindActionMap("ModeSwitch").Disable();
        InputSystem.actions.FindActionMap("Player").Disable();
        InputSystem.actions.FindActionMap("UI").Disable();
        InputSystem.actions.FindActionMap("Dialogue").Enable();
    }

    private void OnDialogueEnded()
    {
        InputSystem.actions.FindActionMap("ModeSwitch").Enable();
        InputSystem.actions.FindActionMap("Player").Enable();
        InputSystem.actions.FindActionMap("UI").Disable();
        InputSystem.actions.FindActionMap("Dialogue").Disable();
    }

    protected override void Awake()
    {
        base.Awake();
        InputSystem.actions.FindActionMap("ModeSwitch").Enable();
        InputSystem.actions.FindActionMap("Player").Enable();
        InputSystem.actions.FindActionMap("UI").Disable();
        InputSystem.actions.FindActionMap("Dialogue").Disable();
    }

    private void Update()
    {
        if (InputSystem.actions["ToggleInventory"].WasPressedThisFrame())
        {
            toggledInventory = !toggledInventory;
            onToggledInventoryEvent.Raise(toggledInventory);
            if (toggledInventory)
            {
                InputSystem.actions.FindActionMap("Player").Disable();
                InputSystem.actions.FindActionMap("UI").Enable();
            }
            else
            {
                InputSystem.actions.FindActionMap("Player").Enable();
                InputSystem.actions.FindActionMap("UI").Disable();
            }
        }
    }
}
