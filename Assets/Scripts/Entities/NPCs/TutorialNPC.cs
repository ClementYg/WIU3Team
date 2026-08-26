using UnityEngine;

public class TutorialNPC : MonoBehaviour
{
    [Header("Tutorial NPC")]
    [SerializeField] NPCData npcData;

    [Header("Event Channels")]
    // This event channel will act as the trigger for NPC to teleport to the next location.
    // It is designed to receive an event from the tutorial system, but it can also be
    // adopted for use outside of the system.
    [SerializeField] EventVoid onStepCompletedEvent;
}
