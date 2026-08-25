using Pathfinding;
using UnityEngine;

[CreateAssetMenu(fileName = "SetPathfindSpeed", menuName = "ScriptableObjects/FSM/Actions/SetPathfindSpeed")]
public class SetPathfindSpeedAction : StateAction
{
    public float newSpeed;
    public bool useBlackboardSpeed = false;
    [Range(1f, 5f)] public float blackboardSpeedMultiplier;
    public override void Act(StateController controller)
    {
        EnemyBlackboard blackboard = controller.GetCached<EnemyBlackboard>();
        AIPath aiPath = controller.GetCached<AIPath>();
        if (useBlackboardSpeed)
        {
            aiPath.maxSpeed = blackboard.maxMoveSpeed * blackboardSpeedMultiplier;
        }
        else
        {
            aiPath.maxSpeed = newSpeed;
        }
    }
}
