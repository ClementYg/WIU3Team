using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]

public struct AlertMessage
{
    public AlertType type;
    public AlertStyle style;
    public string template;  //we can use {#} if we wanna do like +50G or +10 Strength etc
}

public enum AlertStyle
{
    Impact, //"PUZZLE COMPLETE" etc stuff
    EnterArea
}

[System.Serializable]
public struct AlertStylePrefab
{
    public AlertStyle style;
    public GameObject prefab;
}


public class AlertManager : PersistentSingleton<AlertManager>
{
    [Header("Event")]
    [SerializeField] EventAlertFloat onRequestAlert;

    [Header("Message")]
    [SerializeField] List<AlertMessage> messages = new();

    [Header("UI")]
    [SerializeField] List<AlertStylePrefab> stylePrefabs = new();

    [Header("timing")]
    [SerializeField] float textDuration = 2f;  //how long it stays on screen

    Dictionary<AlertType, AlertMessage> messageTypes;
    Dictionary<AlertStyle, (UIFader fader, TMP_Text text)> styles;
    Dictionary<AlertStyle, Coroutine> holdTexts;


    protected override void Awake()
    {
        base.Awake();
        if (_instance != this) return;

        messageTypes = new Dictionary<AlertType, AlertMessage>();
        foreach (AlertMessage msg in messages) messageTypes[msg.type] = msg;

        styles = new Dictionary<AlertStyle, (UIFader, TMP_Text)>();
        holdTexts = new Dictionary<AlertStyle, Coroutine>();

        foreach(AlertStylePrefab entry in stylePrefabs)
        {
            if (entry.prefab == null) continue;

            GameObject instance = Instantiate(entry.prefab, transform);
            UIFader fader = instance.GetComponentInChildren<UIFader>(true);
            TMP_Text text = instance.GetComponentInChildren<TMP_Text>(true);

            if (fader == null || text == null)
            {
                Debug.LogError($"(AM):{entry.style} prefab is missing UIFader or Text component");
                continue;
            }
            styles[entry.style] = (fader,text);
        }
    }

    private void OnEnable()
    {
        if (onRequestAlert != null) onRequestAlert.Subscribe(ShowAlert);
    }

    private void OnDisable()
    {
        if (onRequestAlert != null) onRequestAlert.Unsubscribe(ShowAlert);
    }

    public void ShowAlert(AlertType type, float value = 0f)
    {
        if (!messageTypes.TryGetValue(type, out AlertMessage msg))
        {
            Debug.LogWarning($"AlertManager: no message registered for {type}.");
            return;
        }

        string text = msg.template.Contains("{0}") ? string.Format(msg.template, value) : msg.template;
        PlayAlertStyle(msg.style, text);
    }
    public void ShowCustomAlert(string text, AlertStyle style)
    {
        PlayAlertStyle(style, text);
    }

    void PlayAlertStyle(AlertStyle style, string text)
    {
        if (!styles.TryGetValue(style, out var instance))
        {
            Debug.LogWarning($"AlertManager: no prefab registered for style {style}.");
            return;
        }

        instance.text.text = text;

        if (holdTexts.TryGetValue(style, out Coroutine existing) && existing != null)
            StopCoroutine(existing);

        instance.fader.FadeIn(() =>
        {
            holdTexts[style] = StartCoroutine(Fade(instance.fader, style));
        });
    }

    IEnumerator Fade(UIFader fader, AlertStyle style)
    {
        yield return new WaitForSeconds(textDuration);
        fader.FadeOut();
        holdTexts[style] = null;
    }

}
