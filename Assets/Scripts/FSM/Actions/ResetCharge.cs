using Pathfinding;
using UnityEngine;

[CreateAssetMenu(fileName = "ResetCharge", menuName = "ScriptableObjects/FSM/Actions/ResetCharge")]
public class ResetCharge : StateAction
{
    public override void Act(StateController controller)
    {
        EnemyBlackboard blackboard = controller.GetCached<EnemyBlackboard>();
        blackboard.chargeProgress = -1;
        controller.GetComponent<AIPath>().SearchPath();
    }
}
