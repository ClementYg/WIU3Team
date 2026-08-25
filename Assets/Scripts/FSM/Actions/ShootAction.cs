using UnityEngine;

[CreateAssetMenu(fileName = "Shoot", menuName = "ScriptableObjects/FSM/Actions/Shoot")]
public class ShootAction : StateAction
{
    public float shotForce = 100f;
    public bool useAttackPointPosition = false;

    public override void Act(StateController controller)
    {
        EnemyBlackboard blackboard = controller.GetCached<EnemyBlackboard>();
        blackboard.lastAttackTime -= Time.deltaTime;
        //if (blackboard.lastAttackTime > 0f) return;

        var projectilePrefab = blackboard.projectile;
        if (projectilePrefab == null) return;

        var newProjectile = Instantiate(projectilePrefab);
        ProjectileController projectileController = newProjectile.GetComponent<ProjectileController>();
        if (projectileController != null) projectileController.damageAmount = blackboard.enemyData.baseDamage;

        if (useAttackPointPosition && blackboard.attackPoint != null) newProjectile.transform.position = blackboard.attackPoint.position;
        else newProjectile.transform.position = controller.transform.position;
        
        newProjectile.transform.rotation = Quaternion.identity;
        Vector2 targetDirection = Vector2.zero;

        if (useAttackPointPosition) targetDirection = (Vector2)(blackboard.target.position - blackboard.attackPoint.position).normalized;
        else targetDirection = (Vector2)(blackboard.target.position - controller.transform.position).normalized;
        
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        rb.AddForce(targetDirection * shotForce, ForceMode2D.Impulse);

        blackboard.lastAttackTime = blackboard.attackCooldown;
    }
}
