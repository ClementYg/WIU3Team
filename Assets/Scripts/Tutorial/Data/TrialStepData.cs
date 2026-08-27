using UnityEngine;

[CreateAssetMenu(fileName = "TrialStepData", menuName = "ScriptableObjects/Tutorial/Data/TrialStepData")]
public class TrialStepData : StepData
{
    [Header("Event Channels")]
    public EventVoid onStartTrialEvent;
    public EventBool onEndTrialEvent;

    public override void EnterStep()
    {
        onStartTrialEvent.Subscribe(StartTrial);
        onEndTrialEvent.Subscribe(EndTrial);
    }

    public override void ExitStep()
    {
        onStartTrialEvent.Unsubscribe(StartTrial);
        onEndTrialEvent.Unsubscribe(EndTrial);
    }

    private void StartTrial()
    {
        
    }

    private void EndTrial(bool isSuccess)
    {
        if (isSuccess)
        {
            criterion.Raise();
        }
    }
}
