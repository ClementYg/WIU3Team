using UnityEngine;

public abstract class StepData : ScriptableObject
{
    [Header("Step Data")]
    public StepInstruction instruction;
    public StepSuccessCriterion criterion;
    public StepData nextStep;

    public abstract void EnterStep();
}
