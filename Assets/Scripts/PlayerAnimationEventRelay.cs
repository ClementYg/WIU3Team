using UnityEngine;

public class PlayerAnimationEventRelay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [Header("Event Channels")]
    [SerializeField] private EventVoid OnComboWindowOpenEvent;
    [SerializeField] private EventVoid OnAttackCheckEvent;
    [SerializeField] private EventVoid OnAttackEndEvent;

    public void ComboWindowOpen() => OnComboWindowOpenEvent.Raise();
    public void AttackCheck() => OnAttackCheckEvent.Raise();
    public void AttackEnd() => OnAttackEndEvent.Raise();

    private void Awake()
    {
        animator.SetFloat("IdleMult", 0.8f);
        animator.SetFloat("AttackMult", 1.75f);
    }
}
