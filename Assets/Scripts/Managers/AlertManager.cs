using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]

public struct AlertMessage
{
    public AlertType type;
    public string template;  //we can use {#} if we wanna do like +50G or +10 Strength etc
}

public class AlertManager : PersistentSingleton<AlertManager>
{
    [Header("Event")]
    [SerializeField] EventAlertFloat onRequestAlert;

    [Header("Message")]
    [SerializeField] List<AlertMessage> messages = new();

    [Header("UI")]
    [SerializeField] GameObject alertUIPrefab;
    UIFader alertFader;
    TMP_Text alertText;

    [Header("timing")]
    [SerializeField] float textDuration = 2f;  //how long it stays on screen

    Dictionary<AlertType, string> messageTypes;
    Coroutine holdText;



    protected override void Awake()
    {
        base.Awake();
        if (_instance != this) return;
        if (alertUIPrefab != null)
        {
            GameObject uiInstance = Instantiate(alertUIPrefab, transform);
            alertFader = uiInstance.GetComponentInChildren<UIFader>();
            alertText = uiInstance.GetComponentInChildren<TMP_Text>();
        }

        messageTypes = new Dictionary<AlertType, string>();
        foreach(AlertMessage msg in messages)
        {
            messageTypes[msg.type] = msg.template; //initialise all the msgs and types into the dictionary
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

    private void ShowAlert(AlertType type, float value = 0f)
    {
        string template = messageTypes.TryGetValue(type, out string found) 
            ? found : type.ToString();
        
        alertText.text = template.Contains("{0}") ? string.Format(template, value) : template;

        if (holdText != null) StopCoroutine(holdText);
        alertFader.FadeIn(() => holdText = StartCoroutine(Fade()));
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(textDuration);
        alertFader.FadeOut();
    }
}
