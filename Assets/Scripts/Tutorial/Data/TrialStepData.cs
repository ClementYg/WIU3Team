using UnityEngine;

[CreateAssetMenu(fileName = "TrialStepData", menuName = "ScriptableObjects/Tutorial/Data/TrialStepData")]
public class TrialStepData : StepData
{
    [Header("Timer")]
    public UITimer timer;

    [Header("Event Channels")]
    [SerializeField] EventUITimer onDisplayTimerRequestedEvent;
    [SerializeField] EventBool onToggledTimerEventBool;
    [SerializeField] EventVoid onStartTrialEvent;
    [SerializeField] EventVoid onTimerStartedEvent;
    [SerializeField] EventVoid onTimerEndedEvent;

    public override void EnterStep()
    {
        onStartTrialEvent.Subscribe(StartTrial);
        onTimerEndedEvent.Subscribe(EndTrial);

        onDisplayTimerRequestedEvent.Raise(timer);
    }

    public override void ExitStep()
    {
        onStartTrialEvent.Unsubscribe(StartTrial);
        onTimerEndedEvent.Unsubscribe(EndTrial);

        onToggledTimerEventBool.Raise(false);
    }

    private void StartTrial()
    {
        onTimerStartedEvent.Raise();
    }

    private void EndTrial()
    {
        // Trial ended by timer, failed. Restart.
    }
}
