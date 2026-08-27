using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [Header("Event Channels")]
    [SerializeField] private EventBool onToggledPauseEvent;
    [SerializeField] private EventBool onToggledInventoryEvent;
    [SerializeField] private EventBool onToggledBestiaryEvent;
    [SerializeField] private EventBool onToggledQuestUIEvent;
    [SerializeField] private EventBool onToggledCutsceneModeEvent;
    [SerializeField] private EventBool onToggledUIPageEvent;
    [SerializeField] private EventBool onToggledUIIconEvent;
    [SerializeField] private EventVoid onDialogueStartedEvent;
    [SerializeField] private EventVoid onDialogueEndedEvent;

    private bool toggledPause = false;
    private bool toggledInventory = false;
    private bool toggledBestiary = false;
    private bool isCutsceneActive = false;
    private bool isGeneralUIActive = false;
    private bool shouldShowUIPage = true;

    private bool IsUIPageActive => (isGeneralUIActive && shouldShowUIPage);

    private void OnEnable()
    {
        onToggledQuestUIEvent.Subscribe(OnToggledQuestUI);
        onToggledCutsceneModeEvent.Subscribe(OnToggledCutsceneMode);
        onToggledUIPageEvent.Subscribe(OnToggledUIPage);
        onToggledUIIconEvent.Subscribe(OnToggledUIIcon);
        onDialogueStartedEvent.Subscribe(OnDialogueStarted);
        onDialogueEndedEvent.Subscribe(OnDialogueEnded);
    }

    private void OnDisable()
    {
        onToggledQuestUIEvent.Unsubscribe(OnToggledQuestUI);
        onToggledCutsceneModeEvent.Unsubscribe(OnToggledCutsceneMode);
        onToggledUIPageEvent.Unsubscribe(OnToggledUIPage);
        onToggledUIIconEvent.Unsubscribe(OnToggledUIIcon);
        onDialogueStartedEvent.Unsubscribe(OnDialogueStarted);
        onDialogueEndedEvent.Unsubscribe(OnDialogueEnded);
    }

    private void OnToggledQuestUI(bool isEnabled)
    {
        if (isEnabled)
        {
            InputSystem.actions.FindActionMap("ModeSwitch").Disable();
            InputSystem.actions.FindActionMap("Player").Disable();
            InputSystem.actions.FindActionMap("UI").Disable();
            InputSystem.actions.FindActionMap("Dialogue").Disable();
        }
        else
        {
            InputSystem.actions.FindActionMap("ModeSwitch").Enable();
            InputSystem.actions.FindActionMap("Player").Enable();
            InputSystem.actions.FindActionMap("UI").Enable();
            InputSystem.actions.FindActionMap("Dialogue").Enable();
        }
    }

    private void OnToggledCutsceneMode(bool isEnabled)
    {
        isCutsceneActive = isEnabled;
        if (isCutsceneActive)
        {
            InputSystem.actions.FindActionMap("ModeSwitch").Disable();
            InputSystem.actions.FindActionMap("Player").Disable();
            InputSystem.actions.FindActionMap("UI").Disable();
            InputSystem.actions.FindActionMap("Dialogue").Enable();
        }
        else
        {
            InputSystem.actions.FindActionMap("ModeSwitch").Enable();
            InputSystem.actions.FindActionMap("Player").Enable();
            InputSystem.actions.FindActionMap("UI").Disable();
            InputSystem.actions.FindActionMap("Dialogue").Disable();
        }
    }

    private void OnToggledUIPage(bool isEnabled)
    {
        isGeneralUIActive = isEnabled;
        if (isGeneralUIActive)
        {
            InputSystem.actions.FindActionMap("ModeSwitch").Enable();
            InputSystem.actions.FindActionMap("Player").Disable();
            InputSystem.actions.FindActionMap("UI").Disable();
            InputSystem.actions.FindActionMap("Dialogue").Disable();

            shouldShowUIPage = true;
        }
        else
        {
            InputSystem.actions.FindActionMap("ModeSwitch").Enable();
            InputSystem.actions.FindActionMap("Player").Enable();
            InputSystem.actions.FindActionMap("UI").Enable();
            InputSystem.actions.FindActionMap("Dialogue").Enable();
        }
    }

    private void OnToggledUIIcon(bool isEnabled)
    {
        isGeneralUIActive = isEnabled;
        if (isGeneralUIActive)
        {
            shouldShowUIPage = false;
        }
    }

    private void OnDialogueStarted()
    {
        if (isCutsceneActive) return;

        InputSystem.actions.FindActionMap("ModeSwitch").Disable();
        InputSystem.actions.FindActionMap("Player").Disable();
        InputSystem.actions.FindActionMap("UI").Disable();
        InputSystem.actions.FindActionMap("Dialogue").Enable();
    }

    private void OnDialogueEnded()
    {
        if (isCutsceneActive) return;

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

        if (IsUIPageActive)
        {
            if (InputSystem.actions["CloseUIPage"].WasPressedThisFrame())
            {
                onToggledUIPageEvent.Raise(false);
            }
        }

        if (InputSystem.actions["TogglePause"].WasPressedThisFrame())
        {
            toggledPause = !toggledPause;
            onToggledPauseEvent.Raise(toggledPause);
            if (toggledPause)
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

        if (InputSystem.actions["ToggleBestiary"].WasPressedThisFrame())
        {
            toggledBestiary = !toggledBestiary;
            onToggledBestiaryEvent.Raise(toggledBestiary);
            if (toggledBestiary)
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
