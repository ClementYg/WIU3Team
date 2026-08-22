using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EntityData enemyData;
    [SerializeField] private Slider enemyHPBar;
    private float targetHP;

    [Header("Event Channels")]
    [SerializeField] private EventGameObjectFloat OnEnemyHPChangedEvent;

    private void OnEnable()
    {
        OnEnemyHPChangedEvent.Subscribe(OnEnemyChangedHP);
    }

    private void OnDisable()
    {
        OnEnemyHPChangedEvent.Unsubscribe(OnEnemyChangedHP);
    }

    private void Start()
    {
        targetHP = enemyHPBar.value = enemyHPBar.maxValue = enemyData.maxHP;
    }

    private void Update()
    {
        if (enemyHPBar.value == targetHP) return;
        enemyHPBar.value = Mathf.MoveTowards(enemyHPBar.value, targetHP, 30f * Time.deltaTime);
    }

    private void OnEnemyChangedHP(GameObject enemy, float newHP)
    {
        if (enemy == this.gameObject)
        {
            targetHP = newHP;
        }
    }
}
