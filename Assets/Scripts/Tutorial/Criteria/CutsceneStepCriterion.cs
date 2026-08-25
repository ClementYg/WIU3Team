using UnityEngine;

[CreateAssetMenu(fileName = "CutsceneStepCriterion", menuName = "ScriptableObjects/Tutorial/Criteria/CutsceneStepCriterion")]
public class CutsceneStepCriterion : StepCriterion
{
    [Header("Event Channels")]
    public EventVoid onCutsceneEndedEvent;

    public void OnCutsceneEnded()
    {
        Debug.Log("CutsceneStepCriterion: step completed");
        isCriterionMet = true;
    }
}
