using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [Header("Dependencies")]
    public EntityData data;
    public CinemachineImpulseSource impulseSource;
    public GameObject hurtParticle;
    public AudioClip hurtSFX;

    [Header("Event Channels")]
    public EventRegenerationGameObject OnRegenerationEvent;
    public EventGameObjectFloat OnEnemyChangedHPEvent;
    public EventAudioClip OnSFXRequestEvent;

    [HideInInspector] public float currentHP;
    private Coroutine regenerationRoutine;

    private void OnEnable()
    {
        if (OnRegenerationEvent != null) OnRegenerationEvent.Subscribe(OnRegeneration);
    }

    private void OnDisable()
    {
        if (OnRegenerationEvent != null) OnRegenerationEvent.Unsubscribe(OnRegeneration);
    }

    private void Awake()
    {
        currentHP = data.maxHP;
    }

    public void Damage(float amount)
    {
        EnemyBlackboard blackboard = this.gameObject.GetComponent<EnemyBlackboard>();
        if (blackboard != null)
        {
            if (blackboard.isInvulnerable) return;
        }
        
        if (amount <= 0f) return;

        if (hurtParticle != null)
        {
            var particle = Instantiate(hurtParticle, transform.position, Quaternion.identity);
            var particleSysMain = particle.GetComponent<ParticleSystem>().main;
            particleSysMain.stopAction = ParticleSystemStopAction.Destroy;
        }

        if (hurtSFX != null && OnSFXRequestEvent != null)
        {
            OnSFXRequestEvent.Raise(hurtSFX);
        }

        if (impulseSource != null) impulseSource.GenerateImpulse();
        currentHP = Mathf.Max(0f, currentHP - amount);
        if (OnEnemyChangedHPEvent != null) OnEnemyChangedHPEvent.Raise(this.gameObject, currentHP);
    }

    public void Heal(float amount)
    {
        if (amount <= 0f) return;
        currentHP = Mathf.Min(data.maxHP, currentHP + amount);
        if (OnEnemyChangedHPEvent != null) OnEnemyChangedHPEvent.Raise(this.gameObject, currentHP);
    }

    private void OnRegeneration(RegenerationInfo regenInfo, GameObject enemy)
    {
        if (this.gameObject != enemy) return;
        
        if (regenerationRoutine != null)
        {
            StopCoroutine(regenerationRoutine);
        }

        regenerationRoutine = StartCoroutine(RegenerationRoutine(regenInfo.duration, regenInfo.healInterval, regenInfo.healPerTick));
    }

    private System.Collections.IEnumerator RegenerationRoutine(float duration, float tickInterval, float healPerTick)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            yield return new WaitForSeconds(tickInterval);
            elapsed += tickInterval;
            if (IsFull) break;
            Heal(healPerTick);
        }

        regenerationRoutine = null;
    }

    private void Update()
    {
        if (currentHP <= 0)
        {
            if (transform.parent.gameObject != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

        }
    }

    public float HPRatio => data.maxHP > 0f ? currentHP / data.maxHP : 0f;

    public bool IsDead => currentHP <= 0f;

    public bool IsFull => currentHP >= data.maxHP;
}

[System.Serializable]
public class RegenerationInfo
{
    public float duration;
    public float healInterval;
    public float healPerTick;
}