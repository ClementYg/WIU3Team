using UnityEngine;

public class TriggerZone : TutorialTeleporter
{
    [Header("Event Channels")]
    [SerializeField] EventVoid onEnteredTriggerZoneEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onEnteredTriggerZoneEvent.Raise();

            if (HasDoneLastTeleport)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
