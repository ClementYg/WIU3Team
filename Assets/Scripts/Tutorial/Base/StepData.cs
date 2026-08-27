using UnityEngine;

public abstract class StepData : ScriptableObject
{
    [Header("Step Data")]
    public StepInstruction instruction; // Not in use

    [Header("Event Channels")]
    public EventVoid criterion;

    public abstract void EnterStep();
    public abstract void ExitStep();
}
