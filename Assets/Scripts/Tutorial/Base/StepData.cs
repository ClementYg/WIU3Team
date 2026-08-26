using UnityEngine;

public abstract class StepData : ScriptableObject
{
    [Header("Step Data")]
    public StepInstruction instruction;
    public StepCriterion criterion;
    public StepData nextStep;

    public abstract void EnterStep();
    public abstract void ExitStep();
    
    public StepData GetNextStep()
    {
        return nextStep;
    }
}
