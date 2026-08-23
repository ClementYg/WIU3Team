using UnityEngine;
using UnityEngine.EventSystems;

public class QuestLogEntry : MonoBehaviour, IPointerClickHandler
{
    [Header("Event Channels")]
    [SerializeField] EventQuestLogEntry onLogEntryClickEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        onLogEntryClickEvent.Raise(this);
    }
}
