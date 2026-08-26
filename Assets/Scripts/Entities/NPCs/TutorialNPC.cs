using UnityEngine;
using System.Collections.Generic;

public class TutorialNPC : MonoBehaviour
{
    [Header("Tutorial NPC")]
    [SerializeField] NPCData npcData;
    [SerializeField] Transform npcTransform;
    [SerializeField] List<TeleportNPC> teleports;

    TeleportNPC currentTeleport;
    int currentTeleportIndex = 0;

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
            return;
        }

        currentTeleport = teleports[currentTeleportIndex++];
        npcTransform.position = currentTeleport.newPosition;
    }
}
