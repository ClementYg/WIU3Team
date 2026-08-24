using UnityEngine;

[CreateAssetMenu(fileName = "StepData", menuName = "ScriptableObjects/Tutorial/StepData")]
public class StepData : ScriptableObject
{
    [Header("Step Data")]
    public StepInstruction instruction;
    public StepSuccessCriterion criterion;
    public StepData nextStep;

    [Header("Cutscenes")]
    public Cutscene startCutscene;
    public Cutscene endCutscene;
}
