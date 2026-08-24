using UnityEngine;

[CreateAssetMenu(fileName = "StartTimer", menuName = "ScriptableObjects/FSM/Actions/StartTimer")]
public class StartTimerAction : StateAction
{
    public float timerDuration = 1f;

    public override void Act(StateController controller)
    {
        controller.StartCoroutine(TimerEnd(controller));
    }

    private System.Collections.IEnumerator TimerEnd(StateController controller)
    {
        yield return new WaitForSeconds(timerDuration);
        EnemyBlackboard blackboard = controller.GetCached<EnemyBlackboard>();
        blackboard.timerEnded = true;
    }
}
