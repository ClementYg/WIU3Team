using UnityEngine;
using System.Collections.Generic;

public class TutorialSystem : Singleton<TutorialSystem>
{
    [Header("Tutorial")]
    public List<StepInstance> steps;

    [Header("Event Channels")]
    [SerializeField] EventVoid onTriggerTutorialEvent;

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
        if (steps.Count > 0)
        {
            // Enter the first step
            steps[0].EnterStep();
        }
    }
}
