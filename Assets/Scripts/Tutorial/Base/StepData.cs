using UnityEngine;

public abstract class StepData : ScriptableObject
{
    [Header("Step Data")]
    public StepInstruction instruction;
    public StepData nextStep;

    [Header("Event Channels")]
    public EventVoid criterion;

    public abstract void EnterStep();
    public abstract void ExitStep();
    
    public StepData GetNextStep()
    {
        return nextStep;
    }
}
