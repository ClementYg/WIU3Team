using UnityEngine;

[CreateAssetMenu(fileName = "StepCriterion", menuName = "ScriptableObjects/Tutorial/StepCriterion")]
public class StepCriterion : ScriptableObject
{
    [Header("Event Channels")]
    public EventVoid onCriterionMetEvent;
}
