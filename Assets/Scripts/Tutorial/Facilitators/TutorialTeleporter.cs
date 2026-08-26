using UnityEngine;
using System.Collections.Generic;

public class TutorialTeleporter : MonoBehaviour
{
    [Header("Tutorial Teleporter")]
    [SerializeField] Transform teleporterTransform;
    [SerializeField] SpriteRenderer teleporterRenderer;
    [SerializeField] List<TeleportData> teleports;

    TeleportData currentTeleport;
    int currentTeleportIndex = 0;

    public bool HasDoneLastTeleport => (currentTeleportIndex > teleports.Count - 1);

    private void Awake()
    {
        InitTeleport();
    }

    private void TeleportToNextPos()
    {
        ++currentTeleportIndex;
        if (HasDoneLastTeleport) return;

        // Unsubscribe from the current event first, go to the next one and then subscribe to it
        currentTeleport.onTeleportRequestedEvent.Unsubscribe(TeleportToNextPos);

        InitTeleport();
    }

    private void InitTeleport()
    {
        currentTeleport = teleports[currentTeleportIndex];
        currentTeleport.onTeleportRequestedEvent.Subscribe(TeleportToNextPos);

        // Set the transform
        teleporterTransform.position = currentTeleport.newPosition;
        teleporterTransform.localScale = currentTeleport.newScale;

        // Set the color
        teleporterRenderer.color = currentTeleport.newColor;
    }
}
