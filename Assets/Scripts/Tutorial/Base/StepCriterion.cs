using UnityEngine;

public abstract class StepCriterion : ScriptableObject
{
    [Header("Step Criterion")]
    [HideInInspector] public bool isCriterionMet = false;
}
