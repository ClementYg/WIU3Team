using UnityEngine;

[CreateAssetMenu(fileName = "CutsceneStepData", menuName = "ScriptableObjects/Tutorial/Data/CutsceneStepData")]
public class CutsceneStepData : StepData
{
    [Header("Cutscenes")]
    public Cutscene cutscene;

    public override void EnterStep()
    {
        // Start the cutscene
        CutsceneManager.Instance.Play(cutscene);
    }

    public override void ExitStep()
    {
        
    }
}
