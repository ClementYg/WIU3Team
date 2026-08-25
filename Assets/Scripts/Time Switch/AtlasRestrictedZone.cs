using UnityEngine;

public class AtlasRestrictedZone : MonoBehaviour
{
    [Header("Atlas Restricted Zone")]
    [SerializeField] SpriteRenderer sprtRenderer;

    [Header("Event Channels")]
    [SerializeField] EventVoid onPlayerEnteredResZoneEvent;
    [SerializeField] EventVoid onPlayerExitedResZoneEvent;

    private void Awake()
    {
        Color newColor = sprtRenderer.color;
        newColor.a = 0f;
        sprtRenderer.color = newColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onPlayerEnteredResZoneEvent.Raise();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onPlayerExitedResZoneEvent.Raise();
    }
}
