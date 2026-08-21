using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "ScriptableObjects/FSM/Actions/MeleeAttack")]
public class MeleeAttackAction : StateAction
{
    public float hitboxRadius = 0.5f;
    public LayerMask attackMask;
    
    public override void Act(StateController controller)
    {
        EnemyBlackboard blackboard = controller.GetCached<EnemyBlackboard>();
        blackboard.lastAttackTime -= Time.deltaTime;
        if (blackboard.lastAttackTime > 0f) return;

        Collider2D hitTarget = Physics2D.OverlapCircle(blackboard.attackPoint.position, hitboxRadius, attackMask);
        if (hitTarget != null)
        {
            Health health = hitTarget.GetComponent<Health>();
            if (health != null) health.Damage(blackboard.enemyData.baseDamage);
        }

        blackboard.lastAttackTime = blackboard.attackCooldown;
    }
}
