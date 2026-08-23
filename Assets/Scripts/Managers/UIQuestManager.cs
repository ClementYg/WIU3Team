using UnityEngine;

public class UIQuestManager : PersistentSingleton<UIQuestManager>
{
    [Header("UI Faders")]
    [SerializeField] UIFader canvasFader;
    [SerializeField] UIFader questLogFader;

    [Header("Event Channels")]
    [SerializeField] EventVoid onToggledQuestLogEvent;
    [SerializeField] EventVoid onDialogueStartedEvent;
    [SerializeField] EventVoid onDialogueEndedEvent;

    bool isQuestLogEnabled = false;

    private void OnEnable()
    {
        onToggledQuestLogEvent.Subscribe(OnToggledQuestLog);
        onDialogueStartedEvent.Subscribe(OnDialogueStarted);
        onDialogueEndedEvent.Subscribe(OnDialogueEnded);
    }

    private void OnDisable()
    {
        onToggledQuestLogEvent.Unsubscribe(OnToggledQuestLog);
        onDialogueStartedEvent.Unsubscribe(OnDialogueStarted);
        onDialogueEndedEvent.Unsubscribe(OnDialogueEnded);
    }

    public void OnToggledQuestLog()
    {
        isQuestLogEnabled = !isQuestLogEnabled;
        if (isQuestLogEnabled)
        {
            questLogFader.FadeIn();
        }
        else
        {
            questLogFader.FadeOut();
        }
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

        Transform questLogTransform = transform.Find("Quest Log");
        if (questLogTransform.TryGetComponent<UIFader>(out UIFader qstLgFader))
        {
            questLogFader = qstLgFader;
        }
    }
#endif
}
