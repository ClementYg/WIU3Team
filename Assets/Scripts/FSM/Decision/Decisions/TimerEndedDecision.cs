using UnityEngine;

[CreateAssetMenu(fileName = "TimerEnded", menuName = "ScriptableObjects/FSM/Decisions/TimerEnded")]
public class TimerEndedDecision : StateDecision
{
    public override bool Decide(StateController controller)
    {
        return controller.GetCached<EnemyBlackboard>().timerEnded;
    }
}
