using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "ChargeAttack", menuName = "ScriptableObjects/FSM/Actions/ChargeAttack")]
public class ChargeAttack : StateAction
{
    public float chargeDist;
    public bool HorizontalLock;
    public override void Act(StateController controller)
    {
        float ChargingProg;
        Vector2 StartingChPos, ChargingPos;
        EnemyBlackboard blackboard = controller.GetCached<EnemyBlackboard>();

        ChargingProg = blackboard.chargeProgress;
        StartingChPos = blackboard.ChargeStartPosition;
        ChargingPos = blackboard.ChargeTargetPosition;

        if (ChargingProg < 1 && ChargingProg >= 0)
        {
            blackboard.chargeProgress += Time.deltaTime/2;
            controller.transform.position = Vector2.Lerp(StartingChPos, ChargingPos, 1 - math.cos((ChargingProg * math.PI) / 2));
            ChargingProg = math.clamp(ChargingProg + (Time.deltaTime * 1.5f), 0, 1);
        }
        else 
        {
            blackboard.ChargeStartPosition = StartingChPos = controller.transform.position;
            blackboard.ChargeTargetPosition = ChargingPos = StartingChPos + (new Vector2(blackboard.target.position.x, blackboard.target.position.y) - StartingChPos).normalized * chargeDist;
            if (HorizontalLock)
            {
                blackboard.ChargeTargetPosition = ChargingPos = StartingChPos + (new Vector2(blackboard.target.position.x, blackboard.transform.position.y) - StartingChPos).normalized * chargeDist;
            }
            RaycastHit2D hit2D = new RaycastHit2D();
            if (hit2D = Physics2D.Raycast(StartingChPos, (ChargingPos-StartingChPos).normalized, chargeDist, LayerMask.GetMask("Ground")))
            {
                blackboard.ChargeTargetPosition = hit2D.point;
            }
            blackboard.chargeProgress = 0;
        }


    }
}
