using UnityEngine;

[System.Serializable]
public class StepInstance
{
    [Header("Step")]
    public StepData stepData;

    public StepInstance(StepData stepData)
    {
        this.stepData = stepData;
    }

    public void EnterStep()
    {
        stepData.EnterStep();
    }

    public void ExitStep()
    {
        stepData.ExitStep();
    }
}
