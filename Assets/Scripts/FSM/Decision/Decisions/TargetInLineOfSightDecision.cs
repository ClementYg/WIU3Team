using UnityEngine;

[CreateAssetMenu(fileName = "TargetInLineOfSight", menuName = "ScriptableObjects/FSM/Decisions/TargetInLineOfSight")]
public class TargetInLineOfSightDecision : StateDecision
{
    public LayerMask obstructionMask;

    public override bool Decide(StateController controller)
    {
        Transform target = controller.GetCached<EnemyBlackboard>().target;
        if (target == null) return false;

        Vector2 origin = controller.transform.position;
        Vector2 direction = (Vector2)target.position - origin;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction.normalized, direction.magnitude, obstructionMask);

        return hit.collider == null;
    }
}
