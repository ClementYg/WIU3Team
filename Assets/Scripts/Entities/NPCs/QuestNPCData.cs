using UnityEngine;

[CreateAssetMenu(fileName = "QuestNPCData", menuName = "ScriptableObjects/NPCs/QuestNPCData")]
public class QuestNPCData : NPCData
{
    [Header("Quest Item")]
    public ItemQuestEffect effect;

    [Header("Dialogue")]
    public DialogueConversation questAssignmentConvo;
    public DialogueConversation questCompletionConvo;

    public int AssignQuest(QuestInstance toAssign)
    {
        return QuestSystem.Instance.AssignQuest(toAssign);
    }

    public bool CompleteQuest(int questID)
    {
        return QuestSystem.Instance.CompleteQuest(questID);
    }

    public void SubscribeToConvo(System.Action function)
    {
        // Validate quest convo
        if (questAssignmentConvo != null && questAssignmentConvo.onConvoEndedEvent != null)
        {
            questAssignmentConvo.onConvoEndedEvent.Subscribe(function);
        }
        else
        {
            Debug.LogWarning("QuestNPC: Missing quest conversation references.");
        }
    }

    public void UnsubscribeToConvo(System.Action function)
    {
        // Validate quest convo
        if (questAssignmentConvo != null && questAssignmentConvo.onConvoEndedEvent != null)
        {
            questAssignmentConvo.onConvoEndedEvent.Unsubscribe(function);
        }
    }
}
