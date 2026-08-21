using UnityEngine;

[CreateAssetMenu(fileName = "ResetAnimation", menuName = "ScriptableObjects/FSM/Actions/ResetAnimation")]
public class ResetAnimationAction : StateAction
{
    public override void Act(StateController controller)
    {
        EnemyBlackboard blackboard = controller.GetCached<EnemyBlackboard>();
        blackboard.animationFinished = false;
    }
}
