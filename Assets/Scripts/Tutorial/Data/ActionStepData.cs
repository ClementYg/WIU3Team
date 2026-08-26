using UnityEngine;

[CreateAssetMenu(fileName = "ActionStepData", menuName = "ScriptableObjects/Tutorial/Data/ActionStepData")]
public class ActionStepData : StepData
{
    [Header("Action Guide")]
    public UIIcon guide;

    [Header("Event Channels")]
    [SerializeField] EventUIIcon onDisplayIconRequestedEvent;
    [SerializeField] EventBool onToggledIconEventBool;

    public override void EnterStep()
    {
        // Raise the event to display the action guide
        onDisplayIconRequestedEvent.Raise(guide);
    }

    public override void ExitStep()
    {
        // Raise the event to stop displaying the action guide
        onToggledIconEventBool.Raise(false);
    }
}
