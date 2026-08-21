using UnityEngine;

[CreateAssetMenu(fileName = "HealthBelowThreshold", menuName = "ScriptableObjects/FSM/Decisions/HealthBelowThreshold")]
public class HealthBelowThresholdDecision : StateDecision
{
    [Range(0f, 1f)] public float threshold = 0.5f;

    public override bool Decide(StateController controller)
    {
        Health health = controller.GetCached<Health>();
        if (health == null) return false;

        return health.HPRatio <= threshold;
    }
}
