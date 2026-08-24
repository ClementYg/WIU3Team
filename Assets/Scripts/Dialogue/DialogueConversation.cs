using UnityEngine;

[CreateAssetMenu(fileName = "DialogueConversation", menuName = "ScriptableObjects/Dialogue/DialogueConversation")]
public class DialogueConversation : ScriptableObject
{
    [Header("Starting Points")]
    public DialogueNode firstTimeNode;
    public DialogueNode repeatNode;

    [Header("State")]
    public bool hasBeenSeen;

    [Header("Event Channels")]
    public EventVoid onConvoEndedEvent;

    public DialogueNode GetStartNode()
    {
        if (hasBeenSeen && repeatNode != null)
        {
            return repeatNode;
        }

        return firstTimeNode;
    }

    public void MarkAsSeen()
    {
        hasBeenSeen = true;
    }
}
