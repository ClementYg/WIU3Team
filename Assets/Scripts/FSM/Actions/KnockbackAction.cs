using UnityEngine;

[CreateAssetMenu(fileName = "Knockback", menuName = "ScriptableObjects/FSM/Actions/Knockback")]
public class KnockbackAction : StateAction
{
    public float knockbackForce = 5f;

    public override void Act(StateController controller)
    {
        EnemyBlackboard blackboard = controller.GetCached<EnemyBlackboard>();
        Transform target = blackboard.target;
        if (target == null) return;

        Rigidbody2D rb = controller.GetCached<Rigidbody2D>();
        Vector2 direction = (controller.transform.position - target.position).normalized;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }
}
