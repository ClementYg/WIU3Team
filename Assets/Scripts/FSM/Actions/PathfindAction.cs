using UnityEngine;
using Pathfinding;

[CreateAssetMenu(fileName = "Pathfind", menuName = "ScriptableObjects/FSM/Actions/Pathfind")]
public class PathfindAction : StateAction
{
    public override void Act(StateController controller)
    {
        EnemyBlackboard blackboard = controller.GetCached<EnemyBlackboard>();
        AIDestinationSetter aiDestSetter = controller.GetCached<AIDestinationSetter>();
        if (blackboard.target == null)
        {
            aiDestSetter.target = null;
        }
        else
        {
            aiDestSetter.target = blackboard.target;
        }
    }
}
