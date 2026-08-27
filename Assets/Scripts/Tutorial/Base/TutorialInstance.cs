using UnityEngine;

public class TutorialInstance
{
    TutorialData data;
    StepInstance currentStep;

    int currentStepIndex = 0;

    bool isTutorialCompleted = false;
    public bool IsTutorialCompleted => isTutorialCompleted;

    public TutorialInstance(TutorialData data)
    {
        if (data.steps.Count <= 0)
        {
            Debug.LogError("TutorialInstance: TutorialData does not have any steps.");
            return;
        }

        this.data = data;
        currentStep = new(data.steps[currentStepIndex]);
    }

    public void StartTutorial()
    {
        // Add an item at the start if needed
        data.AddItemAtSlot();

        // Enter the step that we want to start at
        currentStepIndex = data.startAtStep;
        currentStep = new(data.steps[currentStepIndex]);
        currentStep.EnterStep();
        SubscribeToCriterion();
    }

    private void GoNextStep()
    {
        // Unsubscribe from this function
        UnsubscribeFromCriterion();

        ++currentStepIndex;
        if (currentStepIndex > data.steps.Count - 1)
        {
            // No more steps left, tutorial is complete
            currentStep.ExitStep();
            CompleteTutorial();
            return;
        }

        // Go to the next step
        currentStep.ExitStep();
        currentStep = new(data.steps[currentStepIndex]);
        currentStep.EnterStep();

        // Subscribe to the new step's event
        SubscribeToCriterion();
    }

    private void CompleteTutorial()
    {
        data.onTutorialCompletedEvent.Raise();
        isTutorialCompleted = true;
    }

    private void SubscribeToCriterion()
    {
        currentStep.stepData.SubscribeToCriterion(GoNextStep);
    }

    private void UnsubscribeFromCriterion()
    {
        currentStep.stepData.UnsubscribeFromCriterion(GoNextStep);
    }
}
