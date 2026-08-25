using UnityEngine;

[System.Serializable]
public class StepInstance
{
    [Header("Step")]
    public StepData stepData;

    public bool IsStepCompleted => (stepData.criterion.isCriterionMet);

    public void EnterStep()
    {
        stepData.EnterStep();
    }

    public void SubscribeToCriterion()
    {
        if (stepData.criterion is CutsceneStepCriterion criterion)
        {
            Debug.Log("StepInstance: criterion subscribed");
            criterion.onCutsceneEndedEvent.Subscribe(criterion.OnCutsceneEnded);
        }
    }

    public void UnsubscribeFromCriterion()
    {
        if (stepData.criterion is CutsceneStepCriterion criterion)
        {
            criterion.onCutsceneEndedEvent.Unsubscribe(criterion.OnCutsceneEnded);
        }
    }
}
