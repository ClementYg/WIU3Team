using UnityEngine;

[CreateAssetMenu(fileName = "ChargeEnded", menuName = "ScriptableObjects/FSM/Decisions/ChargeEnded")]
public class ChargeEndedDecision : StateDecision
{
    public override bool Decide(StateController controller)
    {
        Debug.Log(controller.GetCached<EnemyBlackboard>().chargeProgress);
        return controller.GetCached<EnemyBlackboard>().chargeProgress >= 1;
    }
}
