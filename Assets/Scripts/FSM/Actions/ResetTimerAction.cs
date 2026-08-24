using UnityEngine;

[CreateAssetMenu(fileName = "ResetTimer", menuName = "ScriptableObjects/FSM/Actions/ResetTimer")]
public class ResetTimerAction : StateAction
{
    public override void Act(StateController controller)
    {
        EnemyBlackboard blackboard = controller.GetCached<EnemyBlackboard>();
        blackboard.timerEnded = false;
    }
}
