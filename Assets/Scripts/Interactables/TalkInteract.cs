using UnityEngine;

public class TalkInteract : Interactable
{
    [Header("Dialogue Data")]
    [SerializeField] private DialogueConversation conversation;

    protected override void Start()
    {
        if (useDefaultValues)
        {
            fadeSpeed = 8f;
            moveSpeed = 2f;
            maxDividerDistance = 0.15f;
            textContent = "Talk";
            fontSize = 1f;
            initialDistanceFromCenter = 0f;
        }

        base.Start();
    }

    public override void Interact()
    {
        if (conversation == null)
        {
            Debug.LogError($"[TalkInteract] {name} has no conversation assigned.", this);
            return;
        }

        DialogueManager.Instance.StartDialogue(conversation);
    }
}
