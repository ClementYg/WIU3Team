using UnityEngine;
using System.Collections.Generic;

public class TutorialTeleporter : MonoBehaviour
{
    [Header("Tutorial Teleporter")]
    [SerializeField] Transform teleporterTransform;
    [SerializeField] List<TeleportData> teleports;

    TeleportData currentTeleport;
    int currentTeleportIndex = 0;

    bool hasDoneLastTeleport = false;
    public bool HasDoneLastTeleport => hasDoneLastTeleport;

    private void Awake()
    {
        currentTeleport = teleports[currentTeleportIndex];
        currentTeleport.onTeleportRequestedEvent.Subscribe(TeleportToNextPos);
    }

    private void TeleportToNextPos()
    {
        if (currentTeleportIndex > teleports.Count - 1)
        {
            // This is the last position
            currentTeleport.onTeleportRequestedEvent.Unsubscribe(TeleportToNextPos);
            hasDoneLastTeleport = true;
            return;
        }

        currentTeleport = teleports[currentTeleportIndex++];
        teleporterTransform.position = currentTeleport.newPosition;
    }
}
