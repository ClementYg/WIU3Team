using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttackAnim", menuName = "ScriptableObjects/FSM/Actions/MeleeAttackAnim")]
public class MeleeAttackAnim : StateAction
{
    public float hitboxRadius = 0.5f;
    public LayerMask attackMask;
    
    public override void Act(StateController controller)
    {
        EnemyBlackboard blackboard = controller.GetCached<EnemyBlackboard>();

        if (blackboard.AtkAnimTrig)
        {        
            Collider2D hitTarget = Physics2D.OverlapCircle(blackboard.attackPoint.position, hitboxRadius, attackMask);
            if (hitTarget != null)
            {
                Health health = hitTarget.GetComponent<Health>();
                if (health != null) health.Damage(blackboard.enemyData.baseDamage);
            }
            blackboard.AtkAnimTrig = false;
        }

    }
}
