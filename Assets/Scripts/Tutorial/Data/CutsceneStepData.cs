using UnityEngine;

[CreateAssetMenu(fileName = "CutsceneStepData", menuName = "ScriptableObjects/Tutorial/CutsceneStepData")]
public class CutsceneStepData : StepData
{
    [Header("Cutscenes")]
    public Cutscene startCutscene;
    public Cutscene endCutscene;

    public override void EnterStep()
    {
        // Start the cutscene
        CutsceneManager.Instance.Play(startCutscene);
    }
}
