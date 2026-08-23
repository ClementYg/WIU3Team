using UnityEngine;

public class UIQuestManager : PersistentSingleton<UIQuestManager>
{
    [Header("UI Faders")]
    [SerializeField] UIFader canvasFader;
    [SerializeField] UIFader questLogFader;
    [SerializeField] UIFader questJournalFader;

    [Header("Event Channels")]
    [SerializeField] EventVoid onToggledQuestUIEvent;
    [SerializeField] EventQuestLogEntry onLogEntryClickedEvent;
    [SerializeField] EventVoid onDialogueStartedEvent;
    [SerializeField] EventVoid onDialogueEndedEvent;

    bool isQuestLogEnabled = true;
    bool isQuestUIEnabled = false;

    private void OnEnable()
    {
        onToggledQuestUIEvent.Subscribe(OnToggledQuestUI);
        onLogEntryClickedEvent.Subscribe(OnLogEntryClicked);
        onDialogueStartedEvent.Subscribe(OnDialogueStarted);
        onDialogueEndedEvent.Subscribe(OnDialogueEnded);
    }

    private void OnDisable()
    {
        onToggledQuestUIEvent.Unsubscribe(OnToggledQuestUI);
        onLogEntryClickedEvent.Unsubscribe(OnLogEntryClicked);
        onDialogueStartedEvent.Unsubscribe(OnDialogueStarted);
        onDialogueEndedEvent.Unsubscribe(OnDialogueEnded);
    }

    public void OnToggledQuestUI()
    {
        isQuestUIEnabled = !isQuestUIEnabled;
        if (isQuestUIEnabled)
        {
            if (isQuestLogEnabled)
            {
                questLogFader.FadeIn();
            }
            else
            {
                questJournalFader.FadeIn();
            }
        }
        else
        {
            if (isQuestLogEnabled)
            {
                questLogFader.FadeOut();
            }
            else
            {
                questJournalFader.FadeOut();
            }
        }
    }

    private void OnLogEntryClicked(QuestLogEntry entry)
    {
        questLogFader.FadeOut();
        questJournalFader.FadeIn();
        isQuestLogEnabled = false;
    }

    private void OnDialogueStarted()
    {
        canvasFader.FadeOut();
    }

    private void OnDialogueEnded()
    {
        canvasFader.FadeIn();
    }

#if UNITY_EDITOR
    [ContextMenu("Find All References")]
    private void FindAllReferences()
    {
        if (transform.TryGetComponent<UIFader>(out UIFader cnvsFader))
        {
            canvasFader = cnvsFader;
        }
    }
#endif
}
