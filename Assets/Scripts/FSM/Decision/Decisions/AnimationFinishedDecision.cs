using UnityEngine;

[CreateAssetMenu(fileName = "AnimationFinished", menuName = "ScriptableObjects/FSM/Decisions/AnimationFinished")]
public class AnimationFinishedDecision : StateDecision
{
    public override bool Decide(StateController controller)
    {
        EnemyBlackboard blackboard = controller.GetCached<EnemyBlackboard>();
        return blackboard.animationFinished;
    }
}
