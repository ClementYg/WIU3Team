using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [Header("Event Channels")]
    [SerializeField] EventVoid onQuestsUpdatedEvent;

    private void OnEnable()
    {
        onQuestsUpdatedEvent.Subscribe(UpdateUI);
    }

    private void OnDisable()
    {
        onQuestsUpdatedEvent.Unsubscribe(UpdateUI);
    }

    private void UpdateUI()
    {

    }
}
