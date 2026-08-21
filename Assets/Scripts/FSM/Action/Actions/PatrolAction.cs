using UnityEngine;

[CreateAssetMenu(fileName = "Patrol", menuName = "ScriptableObjects/FSM/Actions/Patrol")]
public class PatrolAction : StateAction
{
    public override void Act(StateController controller)
    {
        
        EnemyBlackboard blackboard = controller.GetCached<EnemyBlackboard>();
        Rigidbody2D rb = controller.GetCached<Rigidbody2D>();

        if (blackboard.waypoints == null || blackboard.waypoints.Count == 0) return;
        if (Vector2.Distance(controller.transform.position, blackboard.waypoints[blackboard.currentWaypointIndex]) < 0.25f)
        {
            blackboard.currentWaypointIndex = (blackboard.currentWaypointIndex + 1) % blackboard.waypoints.Count;
        }
        else
        {
            Vector2 direction = (blackboard.waypoints[blackboard.currentWaypointIndex] - controller.transform.position).normalized;
            rb.AddForce(direction * blackboard.moveForce);
            if (rb.linearVelocity.magnitude > blackboard.maxMoveSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * blackboard.maxMoveSpeed;
            }
        }
    }
}
