using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : Singleton<DialogueManager>
{
    [Header("Runtime State")]
    private DialogueNode currentNode;
    private DialogueConversation currentConversation;
    private bool isLineScrolling;
    private bool isSkippingAll;
    private string currentRevealedText;
    private Coroutine typewriterRoutine;

    [Header("Typewriter Settings")]
    [SerializeField] private float secondsPerCharacter = 0.02f;

    [Header("Event Channels")]
    [SerializeField] private EventVoid onDialogueStartedEvent;
    [SerializeField] private EventAudioClipFloat onDialogueSFXRequestEvent;
    [SerializeField] private EventStringColor onLineStartedEvent;
    [SerializeField] private EventString onTextUpdatedEvent;
    [SerializeField] private EventVoid onLineFinishedEvent;
    [SerializeField] private EventStringArray onChoicesAvailableEvent;
    [SerializeField] private EventVoid onDialogueEndedEvent;

    public bool IsDialogueActive { get; private set; }

    public void StartDialogue(DialogueConversation conversation)
    {
        if (conversation == null || conversation.GetStartNode() == null)
        {
            Debug.LogError("[DialogueManager] Tried to start dialogue with a null conversation or missing start node.");
            return;
        }

        currentConversation = conversation;
        IsDialogueActive = true;
        onDialogueStartedEvent.Raise();
        DisplayNode(conversation.GetStartNode());
    }

    private void Update()
    {
        if (!IsDialogueActive) return;

        if (InputSystem.actions["Advance"].WasPressedThisFrame())
        {
            Advance();
        }
    }

    private void Advance()
    {
        if (isLineScrolling)
        {
            SkipLine();
            return;
        }

        bool isChoiceNode = currentNode.choices != null && currentNode.choices.Length > 0;
        if (isChoiceNode) return;

        if (currentNode.nextNode != null)
        {
            DisplayNode(currentNode.nextNode);
        }
        else
        {
            EndDialogue();
        }
    }

    private void DisplayNode(DialogueNode node)
    {
        currentNode = node;

        Color speakerColor = node.speaker != null ? node.speaker.textColor : Color.white;
        string speakerName = node.speaker != null ? node.speaker.characterName : "";
        onLineStartedEvent.Raise(speakerName, speakerColor);

        if (typewriterRoutine != null)
        {
            StopCoroutine(typewriterRoutine);
        }

        typewriterRoutine = StartCoroutine(TypewriterRoutine(node.text));
    }

    private IEnumerator TypewriterRoutine(string text)
    {
        isLineScrolling = true;
        currentRevealedText = "";

        int blipEvery = currentNode.speaker != null ? currentNode.speaker.blipEveryNCharacters : 1;
        int charCount = 0;

        foreach (char c in text)
        {
            currentRevealedText += c;
            charCount++;

            if (!char.IsWhiteSpace(c) && charCount % blipEvery == 0)
            {
                PlayBlip();
            }

            onTextUpdatedEvent.Raise(currentRevealedText);

            yield return new WaitForSeconds(secondsPerCharacter);
        }

        FinishLine();
    }

    private void SkipLine()
    {
        if (typewriterRoutine == null) return;

        StopCoroutine(typewriterRoutine);
        typewriterRoutine = null;
        currentRevealedText = currentNode.text;
        onTextUpdatedEvent.Raise(currentRevealedText);

        FinishLine();
    }

    private void FinishLine()
    {
        isLineScrolling = false;
        typewriterRoutine = null;
        onLineFinishedEvent.Raise();

        if (currentNode.choices != null && currentNode.choices.Length > 0)
        {
            string[] choiceTexts = new string[currentNode.choices.Length];
            for (int i = 0; i < choiceTexts.Length; i++)
            {
                choiceTexts[i] = currentNode.choices[i].choiceText;
            }

            onChoicesAvailableEvent.Raise(choiceTexts);
        }
    }

    private void EndDialogue()
    {
        currentConversation.MarkAsSeen();
        IsDialogueActive = false;
        currentNode = null;
        currentConversation = null;
        onDialogueEndedEvent.Raise();
    }

    public void SelectChoice(int choiceIndex)
    {
        // This function is called from Unity's UI
        if (currentNode.choices == null || choiceIndex < 0 || choiceIndex >= currentNode.choices.Length)
        {
            Debug.LogError($"[DialogueManager] Invalid choice index {choiceIndex}.");
            return;
        }

        DisplayNode(currentNode.choices[choiceIndex].nextNode);
    }

    private void PlayBlip()
    {
        if (currentNode.speaker == null || currentNode.speaker.blipSound == null) return;
        onDialogueSFXRequestEvent.Raise(currentNode.speaker.blipSound, Random.Range(currentNode.speaker.pitchRange.x, currentNode.speaker.pitchRange.y));
    }
}
