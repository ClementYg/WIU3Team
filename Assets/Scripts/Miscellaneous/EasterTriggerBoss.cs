using UnityEngine;

public class EasterTriggerBoss : MonoBehaviour
{
    [SerializeField] private EventVoid onDieBossEvent;
    [SerializeField] private GameObject bossEnemy;
    [SerializeField] private AudioClip bossTrack;
    [SerializeField] private AudioClip pastBgm;
    [SerializeField] private Interaction interaction;
    private bool hasTriggeredBoss = false;

    private void OnEnable()
    {
        onDieBossEvent.Subscribe(OnBossDie);
    }

    private void OnDisable()
    {
        onDieBossEvent.Unsubscribe(OnBossDie);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggeredBoss) return;
        hasTriggeredBoss = true;
        if (collision.gameObject.CompareTag("Player"))
        {
            bossEnemy.SetActive(true);
            AudioManager.Instance.PlayBGM(bossTrack);
        }
    }

    private void OnBossDie()
    {
        AudioManager.Instance.StartFadeBGM();
        interaction.Do();
    }
}
