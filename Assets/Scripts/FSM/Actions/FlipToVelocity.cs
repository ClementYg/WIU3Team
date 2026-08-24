using UnityEngine;

[CreateAssetMenu(fileName = "FlipToVelocity", menuName = "ScriptableObjects/FSM/Actions/FlipToVelocity")]
public class FlipToVelocity : StateAction
{
    public override void Act(StateController controller)
    {
        EnemyBlackboard blackboard = controller.GetCached<EnemyBlackboard>();
        Vector3 currentScale = controller.transform.localScale;
        float baseX = Mathf.Abs(currentScale.x);
        bool facesRight = blackboard.spriteDefaultFacesRight;

        if (blackboard.rb.linearVelocityX >= 0.01f)
        {
            controller.transform.localScale = new Vector3(facesRight ? baseX : -baseX, currentScale.y, currentScale.z);
        }
        else if (blackboard.rb.linearVelocityX <=  -0.01f)
        {
            controller.transform.localScale = new Vector3(facesRight ? -baseX : baseX, currentScale.y, currentScale.z);
        }
    }
}
