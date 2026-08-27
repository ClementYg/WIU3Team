using UnityEngine;

public class Receptionist : MonoBehaviour
{
    [Header("Central Hub Teleportation")]
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 playerNewPosition;

    [Header("Event Channels")]
    [SerializeField] EventVoid onTutorialCompletedEvent;

    private void OnEnable()
    {
        onTutorialCompletedEvent.Subscribe(TeleportToCentralHub);
    }

    private void OnDisable()
    {
        onTutorialCompletedEvent.Unsubscribe(TeleportToCentralHub);
    }

    private void TeleportToCentralHub()
    {
        playerTransform.position = playerNewPosition;
    }
}
