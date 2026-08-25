using UnityEngine;

public enum RangeCheck { Detection, Attack};

[CreateAssetMenu(fileName = "PlayerInRange", menuName = "ScriptableObjects/FSM/Decisions/PlayerInRange")]
public class PlayerInRangeDecision : StateDecision
{
    public RangeCheck rangeType = RangeCheck.Detection;
    public override bool Decide(StateController controller)
    {
        EnemyBlackboard blackboard = controller.GetCached<EnemyBlackboard>();
        Transform target = blackboard.target;
        if (target == null) return false;
        float range = rangeType == RangeCheck.Detection ? blackboard.detectionRange : blackboard.attackRange;
        return Vector2.Distance(controller.transform.position, target.position) <= range;
    }
}
