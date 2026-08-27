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
    [SerializeField] EventVoid onStartTrialEvent;
    [SerializeField] EventVoid onTimerStartedEvent;
    [SerializeField] EventVoid onTimerEndedEvent;
    [SerializeField] EventVoid onTrialFailedConvoEndedEvent;
    [SerializeField] EventVector3 onSetPlayerPositionEvent;
    [SerializeField] EventVoid onRequestTriggerZonePreviousPosEvent;

    public override void EnterStep()
    {
        onStartTrialEvent.Subscribe(StartTrial);
        onTimerEndedEvent.Subscribe(EndTrial);
        onTrialFailedConvoEndedEvent.Subscribe(ResetTrial);

        onDisplayTimerRequestedEvent.Raise(timer);
    }

    public override void ExitStep()
    {
        onStartTrialEvent.Unsubscribe(StartTrial);
        onTimerEndedEvent.Unsubscribe(EndTrial);
        onTrialFailedConvoEndedEvent.Unsubscribe(ResetTrial);

        onToggledTimerEventBool.Raise(false);
    }

    private void StartTrial()
    {
        onTimerStartedEvent.Raise();
    }

    private void EndTrial()
    {
        // Trial ended by timer, failed. Restart.
        CutsceneManager.Instance.Play(failedTrialCutscene);
    }

    private void ResetTrial()
    {
        onSetPlayerPositionEvent.Raise(playerStartPosition);
        onRequestTriggerZonePreviousPosEvent.Raise();
    }
}
