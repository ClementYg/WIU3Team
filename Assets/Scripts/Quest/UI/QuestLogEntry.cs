using UnityEngine;
using UnityEngine.EventSystems;

public class QuestLogEntry : MonoBehaviour, IPointerClickHandler
{
    [System.NonSerialized] public QuestInstance quest = null;

    [Header("Event Channels")]
    [SerializeField] EventQuestLogEntry onLogEntryClickEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        onLogEntryClickEvent.Raise(this);
    }
}
