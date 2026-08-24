using UnityEngine;

[CreateAssetMenu(fileName = "PlayAnimation", menuName = "ScriptableObjects/FSM/Actions/PlayAnimation")]
public class PlayAnimationAction : StateAction
{
    public string animationName;

    public override void Act(StateController controller)
    {
        Animator animator = controller.GetCached<Animator>();
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName(animationName))
        {
            animator.Play(animationName);
        }
    }
}
