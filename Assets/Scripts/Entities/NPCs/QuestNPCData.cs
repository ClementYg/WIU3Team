using UnityEngine;

[CreateAssetMenu(fileName = "QuestNPCData", menuName = "ScriptableObjects/NPCs/QuestNPCData")]
public class QuestNPCData : NPCData
{
    [Header("Quest Item")]
    public ItemQuestEffect effect;

    [Header("Quest Dialogue")]
    public DialogueConversation questConvo;

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
        if (questConvo != null && questConvo.onConvoEndedEvent != null)
        {
            questConvo.onConvoEndedEvent.Subscribe(function);
        }
        else
        {
            Debug.LogWarning("QuestNPC: Missing quest conversation references.");
        }
    }

    public void UnsubscribeToConvo(System.Action function)
    {
        // Validate quest convo
        if (questConvo != null && questConvo.onConvoEndedEvent != null)
        {
            questConvo.onConvoEndedEvent.Unsubscribe(function);
        }
    }
}
