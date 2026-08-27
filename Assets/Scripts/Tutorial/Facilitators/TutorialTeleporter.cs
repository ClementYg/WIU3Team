using UnityEngine;
using System.Collections.Generic;

public class TutorialTeleporter : MonoBehaviour
{
    [Header("Tutorial Teleporter")]
    [SerializeField] SpriteRenderer teleporterRenderer;
    [SerializeField] List<TeleportData> teleports;

    [Header("Testing")]
    [SerializeField] int startAtTeleport = 0;

    TeleportData currentTeleport;
    int currentTeleportIndex = 0;

    public bool HasDoneLastTeleport => (currentTeleportIndex > teleports.Count - 1);

    private void Awake()
    {
        currentTeleportIndex = startAtTeleport;
        InitTeleport();
    }

    private void TeleportToNextPos()
    {
        ++currentTeleportIndex;
        if (HasDoneLastTeleport) return;

        // Unsubscribe from the current event first, go to the next one and then subscribe to it
        currentTeleport.UnsubscribeFromTeleportRequest(TeleportToNextPos);

        InitTeleport();
    }

    private void InitTeleport()
    {
        // Enter the teleport that we want to start at
        currentTeleport = teleports[currentTeleportIndex];
        currentTeleport.SubscribeToTeleportRequest(TeleportToNextPos);

        // Set the transform
        transform.position = currentTeleport.newPosition;
        transform.localScale = currentTeleport.newScale;

        // Set the color
        teleporterRenderer.color = currentTeleport.newColor;
    }
}
