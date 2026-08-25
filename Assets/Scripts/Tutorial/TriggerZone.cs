using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [Header("Event Channels")]
    [SerializeField] EventVoid onEnteredTriggerZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onEnteredTriggerZone.Raise();
        }
    }
}
