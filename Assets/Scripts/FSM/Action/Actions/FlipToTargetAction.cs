using UnityEngine;

[CreateAssetMenu(fileName = "FlipToTarget", menuName = "ScriptableObjects/FSM/Actions/FlipToTarget")]
public class FlipToTargetAction : StateAction
{
    public override void Act(StateController controller)
    {
        EnemyBlackboard blackboard = controller.GetCached<EnemyBlackboard>();
        Transform target = blackboard.target;
        if (target == null) return;

        float horiDiff = (target.position - controller.transform.position).x;
        Vector3 currentScale = controller.transform.localScale;
        float baseX = Mathf.Abs(currentScale.x);
        bool facesRight = blackboard.spriteDefaultFacesRight;

        if (horiDiff >= 0.01f)
        {
            controller.transform.localScale = new Vector3(facesRight ? baseX : -baseX, currentScale.y, currentScale.z);
        }
        else if (horiDiff <= -0.01f)
        {
            controller.transform.localScale = new Vector3(facesRight ? -baseX : baseX, currentScale.y, currentScale.z);
        }
    }
}
