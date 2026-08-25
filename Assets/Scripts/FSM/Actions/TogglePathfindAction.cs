using Pathfinding;
using UnityEngine;

[CreateAssetMenu(fileName = "TogglePathfind", menuName = "ScriptableObjects/FSM/Actions/TogglePathfind")]
public class TogglePathfindAction : StateAction
{
    public bool togglePathfind = false;
    public override void Act(StateController controller)
    {
        AIPath aiPath = controller.GetCached<AIPath>();
        if (togglePathfind)
        {
            aiPath.canMove = true;
        }
        else
        {
            aiPath.canMove = false;
        }
    }
}
