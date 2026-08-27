using UnityEngine;

public class ClosedDoor : MonoBehaviour
{
    [Header("Closed Door")]
    [SerializeField] Collider2D doorCollider;
    [SerializeField] SpriteRenderer doorRenderer;
    [SerializeField] Sprite openDoor;
    [SerializeField] float doorXOffset = 0.75f;

    [Header("Event Channels")]
    [SerializeField] EventVoid onUnlockDoorEvent;

    private void OnEnable()
    {
        onUnlockDoorEvent.Subscribe(UnlockDoor);
    }

    private void OnDisable()
    {
        onUnlockDoorEvent.Unsubscribe(UnlockDoor);
    }

    private void UnlockDoor()
    {
        // Turn the collider off
        doorCollider.enabled = false;

        // Change the sprite
        doorRenderer.sprite = openDoor;

        // Move the door
        Vector3 newPosition = transform.position;
        newPosition.x += doorXOffset;
        transform.position = newPosition;
    }
}
