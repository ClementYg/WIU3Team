using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [Header("Quest UI")]
    [SerializeField] ButtonColorFlash bttnColorFlash;

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
        // Do color flash
        bttnColorFlash.FlashForDuration();
    }
}
