using UnityEngine;

[System.Serializable]
public class TeleportNPC
{
    [Header("Teleport NPC")]
    public Vector3 newPosition;

    [Header("Event Channels")]
    // This event channel will act as the trigger for NPC to teleport to the next location.
    // It is designed to receive an event from the tutorial system, but it can also be
    // adopted for use outside of the system.
    public EventVoid onTeleportRequestedEvent;
}
