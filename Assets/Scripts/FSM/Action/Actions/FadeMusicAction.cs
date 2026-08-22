using UnityEngine;

[CreateAssetMenu(fileName = "FadeMusic", menuName = "ScriptableObjects/FSM/Actions/FadeMusic")]
public class FadeMusicAction : StateAction
{
    public float duration = 1f;
    
    public override void Act(StateController controller)
    {
        AudioManager.Instance.FadeOutBGM(duration);
    }
}
