using UnityEngine;

public abstract class StepData : ScriptableObject
{
    [Header("Step Data")]
    public StepInstruction instruction; // Not in use

    [Header("Event Channels")]
    public EventVoid criterion;

    public abstract void EnterStep();
    public abstract void ExitStep();

    public void SubscribeToCriterion(System.Action toSubscribe)
    {
        criterion.Subscribe(toSubscribe);
    }

    public void UnsubscribeFromCriterion(System.Action toUnsubscribe)
    {
        criterion.Unsubscribe(toUnsubscribe);
    }
}
