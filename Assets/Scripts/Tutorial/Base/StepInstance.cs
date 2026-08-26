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

    public bool TryGoNextStep(out StepInstance nextStep)
    {
        ExitStep();

        StepData nextData = stepData.GetNextStep();
        if (nextData == null)
        {
            nextStep = null;
            return false;
        }

        nextStep = new StepInstance(nextData);
        return true;
    }

    private void ExitStep()
    {
        stepData.ExitStep();
    }
}
