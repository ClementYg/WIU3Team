using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class UIManager : Singleton<UIManager>
{
    [Header("Dialogue Box")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choice Buttons")]
    [SerializeField] private Button[] choiceButtons;
    [SerializeField] private TextMeshProUGUI[] choiceTexts;

    [Header("Event Channels")]
    [SerializeField] private EventVoid onDialogueStartedEvent;
    [SerializeField] private EventStringColor onLineStartedEvent;
    [SerializeField] private EventString onTextUpdatedEvent;
    [SerializeField] private EventVoid onLineFinishedEvent;
    [SerializeField] private EventStringArray onChoicesAvailableEvent;
    [SerializeField] private EventVoid onDialogueEndedEvent;

    protected override void Awake()
    {
        base.Awake();
        HideAllChoices();
        dialogueBox.SetActive(false);
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void OnDialogueStarted()
    {
        dialogueBox.SetActive(true);
        HideAllChoices();
    }

    private void OnLineStarted(string speakerName, Color color)
    {
        speakerNameText.text = speakerName;
        dialogueText.color = color;
        HideAllChoices();
    }

    private void OnTextUpdated(string revealedText)
    {
        dialogueText.text = revealedText;
    }

    private void OnLineFinished()
    {
        // Reserved for code like blinking arrow to indicate a "continue"
    }

    private void OnChoicesAvailable(string[] choices)
    {
        for (int i = 0; i < choices.Length; i++)
        {
            bool hasChoice = i < choices.Length;
            choiceButtons[i].gameObject.SetActive(hasChoice);

            if (hasChoice) choiceTexts[i].text = choices[i];
        }
    }

    private void OnDialogueEnded()
    {
        dialogueBox.SetActive(false);
        HideAllChoices();
    }

    private void HideAllChoices()
    {
        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void OnChoiceButtonClicked(int choiceIndex)
    {
        DialogueManager.Instance.SelectChoice(choiceIndex);
    }
}
