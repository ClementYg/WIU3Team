using UnityEngine;

[CreateAssetMenu(fileName = "TrialStepData", menuName = "ScriptableObjects/Tutorial/Data/TrialStepData")]
public class TrialStepData : StepData
{
    [Header("Timed Trial")]
    public UITimer timer;
    public Cutscene failedTrialCutscene;
    public Vector3 playerStartPosition;
    
    [Header("Event Channels")]
    [SerializeField] EventUITimer onDisplayTimerRequestedEvent;
    [SerializeField] EventBool onToggledTimerEventBool;
    [SerializeField] EventVoid onEnteredTriggerZoneEvent;
    [SerializeField] EventVoid onTimerStartedEvent;
    [SerializeField] EventVoid onTimerEndedEvent;
    [SerializeField] EventVoid onTrialFailedConvoEndedEvent;
    [SerializeField] EventVector3 onSetPlayerPositionEvent;
    [SerializeField] EventVoid onRequestTriggerZonePreviousPosEvent;

    bool hasTrialStarted = false;

    public override void EnterStep()
    {
        onEnteredTriggerZoneEvent.Subscribe(EnteredTriggerZone);
        onTimerEndedEvent.Subscribe(EndTrial);
        onTrialFailedConvoEndedEvent.Subscribe(ResetTrial);

        onDisplayTimerRequestedEvent.Raise(timer);

        hasTrialStarted = false;
    }

    public override void ExitStep()
    {
        onEnteredTriggerZoneEvent.Unsubscribe(EnteredTriggerZone);
        onTimerEndedEvent.Unsubscribe(EndTrial);
        onTrialFailedConvoEndedEvent.Unsubscribe(ResetTrial);

        onToggledTimerEventBool.Raise(false);
    }

    private void EnteredTriggerZone()
    {
        if (hasTrialStarted == false)
        {
            // Start the trial
            onTimerStartedEvent.Raise();
            hasTrialStarted = true;
        }
        else
        {
            // Player succeeded the trial
            criterion.Raise();
        }
    }

    private void EndTrial()
    {
        // Trial ended by timer, failed. Restart.
        CutsceneManager.Instance.Play(failedTrialCutscene);
        hasTrialStarted = false;
    }

    private void ResetTrial()
    {
        onSetPlayerPositionEvent.Raise(playerStartPosition);
        onRequestTriggerZonePreviousPosEvent.Raise();
    }
}
