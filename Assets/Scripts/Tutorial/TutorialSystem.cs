using UnityEngine;
using System.Collections.Generic;

public class TutorialSystem : Singleton<TutorialSystem>
{
    [Header("Tutorial")]
    public List<StepInstance> steps;

    [Header("Event Channels")]
    [SerializeField] EventVoid onTriggerTutorialEvent;

    int currentStepIndex = 0;

    private void OnEnable()
    {
        onTriggerTutorialEvent.Subscribe(StartTutorial);
    }

    private void OnDisable()
    {
        onTriggerTutorialEvent.Unsubscribe(StartTutorial);
    }

    private void StartTutorial()
    {
        if (steps.Count <= 0) return;

        // Enter the first step
        StepInstance currentStep = steps[currentStepIndex];
        currentStep.EnterStep();
        currentStep.SubscribeToCriterion();
    }

    private void OnStepCompleted()
    {
        if (currentStepIndex < 0 || currentStepIndex >= steps.Count) return;

        // Complete the step
        StepInstance currentStep = steps[currentStepIndex];
        currentStep.UnsubscribeFromCriterion();
        ++currentStepIndex;
    }
}
