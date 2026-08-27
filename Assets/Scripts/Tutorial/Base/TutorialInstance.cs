using UnityEngine;

public class TutorialInstance
{
    TutorialData data;
    StepInstance currentStep;

    bool isTutorialCompleted = false;
    public bool IsTutorialCompleted => isTutorialCompleted;

    public TutorialInstance(TutorialData data)
    {
        this.data = data;
        currentStep = new StepInstance(data.firstStep);
    }

    public void StartTutorial()
    {
        if (data.firstStep == null)
        {
            Debug.LogError("TutorialInstance: Attempted to start tutorial with missing first step reference.");
        }

        // Enter the first step
        currentStep.EnterStep();
        SubscribeToCriterion();
    }

    private void GoNextStep()
    {
        UnsubscribeToCriterion();

        if (currentStep.TryGoNextStep(out currentStep))
        {
            currentStep.EnterStep();
            SubscribeToCriterion();
        }
        else
        {
            // No next step, the tutorial is complete
            CompleteTutorial();
        }
    }

    private void CompleteTutorial()
    {
        isTutorialCompleted = true;
    }

    private void SubscribeToCriterion()
    {
        GetCriterionMetEvent().Subscribe(GoNextStep);
    }

    private void UnsubscribeToCriterion()
    {
        GetCriterionMetEvent().Unsubscribe(GoNextStep);
    }

    private EventVoid GetCriterionMetEvent()
    {
        EventVoid onCriterionMetEvent = currentStep.stepData.criterion;
        if (onCriterionMetEvent == null)
        {
            Debug.LogWarning("TutorialInstance: Current step is missing event reference." + currentStep.stepData.name);
        }

        return onCriterionMetEvent;
    }
}
