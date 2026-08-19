using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private AudioClip swordSwing;
    [SerializeField] private AudioClip swordHit;

    [Header("Event Channels")]
    [SerializeField] private EventVoid OnComboWindowOpenEvent;
    [SerializeField] private EventFloat OnTriggerAttackEvent;
    [SerializeField] private EventVoid OnAttackCheckEvent;
    [SerializeField] private EventVoid OnAttackEndEvent;
    [SerializeField] private EventFloatFloat OnDamageBoostEvent;
    [SerializeField] private EventAudioClip OnSFXRequestEvent;
    private Coroutine damageBoostRoutine;
    
    [Header("Attack Attributes")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 0.6f;
    [SerializeField] private LayerMask attackLayer;
    private float currentDamage;
    private float extraDamage = 0;
    private bool isAttacking = false;
    private bool queuedAttack = false;
    private bool comboWindowOpen = false;
    private int comboStep = 0;

    public bool IsAttacking => isAttacking;

    private void OnEnable()
    {
        OnComboWindowOpenEvent.Subscribe(OnComboWindowOpen);
        OnTriggerAttackEvent.Subscribe(OnTriggerAttack);
        OnAttackCheckEvent.Subscribe(OnAttackCheck);
        OnAttackEndEvent.Subscribe(OnAttackEnd);
        OnDamageBoostEvent.Subscribe(OnDamageBoost);
    }

    private void OnDisable()
    {
        OnComboWindowOpenEvent.Unsubscribe(OnComboWindowOpen);
        OnTriggerAttackEvent.Unsubscribe(OnTriggerAttack);
        OnAttackCheckEvent.Unsubscribe(OnAttackCheck);
        OnAttackEndEvent.Unsubscribe(OnAttackEnd);
        OnDamageBoostEvent.Unsubscribe(OnDamageBoost);
    }

    private void Awake()
    {
        currentDamage = playerData.baseDamage;
    }

    private void Update()
    {
        UpdateAttackPoint(sprite.flipX);
    }

    private void UpdateAttackPoint(bool facingLeft)
    {
        Vector3 position = attackPoint.localPosition;
        position.x = facingLeft ? -Mathf.Abs(position.x) : Mathf.Abs(position.x);
        attackPoint.localPosition = position;
    }

    private void OnTriggerAttack(float additionalDamage)
    {
        extraDamage = additionalDamage;
        if (!isAttacking)
        {
            OnSFXRequestEvent.Raise(swordSwing);
            StartAttack();
        }
        else if (comboWindowOpen)
        {
            OnSFXRequestEvent.Raise(swordSwing);
            queuedAttack = true;
        }
    }

    private void StartAttack()
    {
        isAttacking = true;
        comboStep = 0;
        queuedAttack = false;
        comboWindowOpen = false;

        animator.SetInteger("ComboStep", comboStep);
        animator.SetBool("IsAttacking", true);
        animator.SetTrigger("AttackTrigger");
    }

    public void OnComboWindowOpen()
    {
        comboWindowOpen = true;
    }

    public void OnAttackCheck()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, attackLayer);
        foreach (var hit in hits)
        {
            Health hpComponent = hit.GetComponent<Health>();
            if (hpComponent != null)
            {
                OnSFXRequestEvent.Raise(swordHit);
                hpComponent.Damage(currentDamage + extraDamage);
            }
        }
    }

    public void OnAttackEnd()
    {
        comboWindowOpen = false;
        
        if (queuedAttack)
        {
            queuedAttack = false;
            comboStep = 1 - comboStep;
            animator.SetInteger("ComboStep", comboStep);
            animator.SetTrigger("AttackTrigger");
            isAttacking = false;
        }
        else
        {
            isAttacking = false;
            comboStep = 0;
            animator.SetInteger("ComboStep", 0);
            animator.SetBool("IsAttacking", false);
        }
    }

    private void OnDamageBoost(float duration, float multiplier)
    {
        if (damageBoostRoutine != null)
        {
            StopCoroutine(damageBoostRoutine);
        }

        damageBoostRoutine = StartCoroutine(DamageBoostRoutine(duration, multiplier));
    }

    private System.Collections.IEnumerator DamageBoostRoutine(float duration, float multiplier)
    {
        currentDamage = playerData.baseDamage * multiplier;
        yield return new WaitForSeconds(duration);
        currentDamage = playerData.baseDamage;
        damageBoostRoutine = null;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }
}
